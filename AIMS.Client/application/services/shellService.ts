import {Inject, Injectable, Component} from 'angular2/core';
import {Http, Headers} from 'angular2/http'
import {RouteParams, ROUTER_DIRECTIVES, Location} from 'angular2/router';

import {IRemoteService} from '../services/remoteService';
import {dto} from '../core/dto';


export class ShellInfo {
    isLoading: boolean;
    loadingMessage: string;

    constructor() {
        this.isLoading = false;
        this.loadingMessage = "";
    }

}

export abstract class IShellService {
    info: ShellInfo;
    abstract showLoader(message);
    abstract hideLoader();
    abstract toastSuccess(title, text);
    abstract toastError(title, text);
    abstract toastInfo(title, text);
    abstract toastWarning(title, text);
}

@Injectable()
export class ShellService implements IShellService {

    info: ShellInfo;
    toastSvc: Toastr;
    //translate: TranslateService;
    toastLocation = 'toast-top-center';

    constructor( @Inject(Location) location: Location) {
        this.info = new ShellInfo();
        this.toastSvc = toastr;
    }


    showLoader(message) {
        this.info.isLoading = true;
        this.info.loadingMessage = message;
    }

    hideLoader() {
        this.info.isLoading = false;
        this.info.loadingMessage = "";
    }

    toastSuccess(title, text) {
        var self = this;
        self.toastSvc.success(text, title, {
            closeButton: true,
            timeOut: 3000,
            positionClass: self.toastLocation
        });
    }

    toastError(title, text) {
        var self = this;
        self.toastSvc.error(text, title, {
            closeButton: true,
            timeOut: 3000,
            positionClass: self.toastLocation
        });
    }

    toastInfo(title, text) {
        var self = this;
        self.toastSvc.info(text, title, {
            closeButton: true,
            timeOut: 3000,
            positionClass: self.toastLocation
        });
    }

    toastWarning(title, text) {
        var self = this;
        self.toastSvc.warning(text, title, {
            closeButton: true,
            timeOut: 3000,
            positionClass: self.toastLocation
        });
    }

}
