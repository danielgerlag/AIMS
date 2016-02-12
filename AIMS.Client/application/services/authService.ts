import {Inject, Injectable} from 'angular2/core';
import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {Subscription} from 'rxjs/Rx';
import {IRemoteService} from './remoteService';
import {Account} from '../core/models';

var config: any;

export class TransientInfo {
    isLoggedIn: boolean;
    fullName: string;
    isAdmin: boolean;

    constructor() {
        this.isLoggedIn = false;
        this.fullName = "";        
    }
}

export abstract class IAuthService {
    userInfo: TransientInfo;
    abstract login(model: Account.LoginRequest, sender: any, callback: (sender: any, data: any, status: number) => any): Subscription<any>;
    abstract logout(sender: any, callback: (sender: any, data: any, status: number) => any): Subscription<any>;
    abstract resume(sender: any, callback: (sender: any, data: any, status: number) => any): Subscription<any>;
}

@Injectable()
export class AuthService implements IAuthService {

    private remoteService: IRemoteService;
    public userInfo: TransientInfo;

    constructor(
        @Inject(Router) router: Router,
        @Inject(Location) location: Location,
        @Inject(IRemoteService) remoteService: IRemoteService) {

        this.remoteService = remoteService;
        this.userInfo = new TransientInfo();
        //this.resume(this, null);
    }


    public login(model: Account.LoginRequest, sender: any, callback: (sender: any, data: any, status: number) => any): Subscription<any> {
        var result = this.remoteService.post(this, 'Account/Login', model, (sender1: IAuthService, data: any, status: number) => {
            if (status == 200) {
                var response: Account.LoginResponse = data;
                sender1.userInfo.isLoggedIn = true;
                sender1.userInfo.fullName = response.Name;
                sender1.userInfo.isAdmin = response.IsAdmin;
            }
            if (callback)
                callback(sender, data, status);
        });
        return result;
    }

    public logout(sender: any, callback: (sender: any, data: any, status: number) => any): Subscription<any> {
        var result = this.remoteService.post(this, 'Account/Logout', null, (sender1: IAuthService, data: any, status: number) => {
            if (status == 200) {
                sender1.userInfo.isLoggedIn = false;
                sender1.userInfo.fullName = "";
                sender1.userInfo.isAdmin = false;
            }
            if (callback)
                callback(sender, data, status);
        });
        return result;
    }

    public resume(sender: any, callback: (sender: any, data: any, status: number) => any): Subscription<any> {
        var result = this.remoteService.post(this, 'Account/Resume', null, (sender1: IAuthService, data: any, status: number) => {
            if (status == 200) {
                var response: Account.LoginResponse = data;
                sender1.userInfo.isLoggedIn = true;
                sender1.userInfo.fullName = response.Name;
                sender1.userInfo.isAdmin = response.IsAdmin;
            }
            if (status == 401) {
                sender1.userInfo.isLoggedIn = false;
                sender1.userInfo.fullName = "";
                sender1.userInfo.isAdmin = false;
            }
            if (callback)
                callback(sender, data, status);
        });
        return result;
    }


}