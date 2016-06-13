import {LocalStorage, SessionStorage} from 'angular2-localstorage/WebStorage';
import {Injectable} from '@angular/core';
import CONFIG from '../../shared/config';

@Injectable()
export class GameSettingsService {
    @LocalStorage() private gameSettings:Object;

    constructor( private config: CONFIG) {
        this.gameSettings = config.DEFAULT_GAME_SETTINGS;
    }

    getGameSettings() {
        return this.gameSettings;
    }

    setGameSettings(gameSettings) {
        this.gameSettings = gameSettings;
    }
}