var gulp = require('gulp'),
    ts = require('gulp-typescript'),
    fs = require("fs"),
    path = require('path');

eval("var project = " + fs.readFileSync("./project.json"));
var lib = "./" + project.webroot + "/lib/";

var paths = {
    tsSrc: './wwwroot/app/**/*.ts',
    tsDest: lib + 'appSrc/',
    jsLibsDest: lib + 'js',
    cssDest: lib + 'css',
    fontsDest: lib + 'fonts'
};


var tsProject = ts.createProject('./wwwroot/tsconfig.json');

gulp.task("setup-libs", () => {
    gulp.src([
            'es6-shim/es6-shim.min.js',
            'systemjs/dist/system-polyfills.min.js',
            'systemjs/dist/system.src.js',
            'reflect-metadata/Reflect.js',
            'rxjs/**',
            'zone.js/dist/**',
            '@angular/**',
            'angular2-in-memory-web-api',
            'angular2-localstorage',
            'materialize-css/dist/js/materialize.js',
    ], {
        cwd: "node_modules/**"
    })
    .pipe(gulp.dest(paths.jsLibsDest));
 
    gulp.src([
        'node_modules/materialize-css/dist/css/materialize.css',
        'bower_components/components-font-awesome/css/font-awesome.css'
    ]).pipe(gulp.dest(paths.cssDest));

    gulp.src([
      'node_modules/materialize-css/dist/fonts/roboto/*.*',
    ]).pipe(gulp.dest(paths.fontsDest + '/roboto'));

    gulp.src([
      'bower_components/components-font-awesome/fonts/FontAwesome.otf',
      'bower_components/components-font-awesome/fonts/fontawesome-webfont.eot',
      'bower_components/components-font-awesome/fonts/fontawesome-webfont.svg',
      'bower_components/components-font-awesome/fonts/fontawesome-webfont.ttf',
      'bower_components/components-font-awesome/fonts/fontawesome-webfont.woff',
      'bower_components/components-font-awesome/fonts/fontawesome-webfont.woff2',
    ]).pipe(gulp.dest(paths.fontsDest));
});

gulp.task('ts', function (done) {
    var tsResult = gulp.src([
       "wwwroot/app/**/*.ts"
    ])
     .pipe(ts(tsProject), undefined, ts.reporter.fullReporter());
    return tsResult.js.pipe(gulp.dest(paths.tsDest));
});

gulp.task('watch.ts', ['ts'], function () {
    return gulp.watch('wwwroot/app/**/*.ts', ['ts']);
});

gulp.task('watch', ['watch.ts']);

gulp.task('clean', function () {
    return gulp.src(lib)
        .pipe(clean());
});

gulp.task('default', ['setup-libs', 'watch']);