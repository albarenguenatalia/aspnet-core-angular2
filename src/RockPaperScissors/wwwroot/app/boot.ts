import { bootstrap } from '@angular/platform-browser-dynamic';
import { enableProdMode } from '@angular/core';
import {LocalStorageService, LocalStorageSubscriber} from 'angular2-localstorage/LocalStorageEmitter';

import { AppComponent } from './app.component';

//enableProdMode();

var appPromise = bootstrap(AppComponent, [LocalStorageService]);

// register LocalStorage
LocalStorageSubscriber(appPromise);