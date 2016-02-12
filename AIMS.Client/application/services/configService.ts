import {Inject, Injectable, Component} from 'angular2/core';

export abstract class IConfigService {
    abstract getSettings(): any;
    
}

@Injectable()
export class ConfigService implements IConfigService {
    
    settings: any;    

    constructor() {
        this.settings = { api: "../api2/"};
    }


    getSettings(): any {
        return this.settings;
    }


}
