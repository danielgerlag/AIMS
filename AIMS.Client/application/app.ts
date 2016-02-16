import {bootstrap} from 'angular2/platform/browser'
import {Inject, Component, View, provide} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';
import {HTTP_PROVIDERS} from './core/xhr_backend';
import {Home} from './home/home';
//import {LoginController} from './account/login'
import {ROUTER_DIRECTIVES, RouteConfig, Location,ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {FormBuilder, Validators, ControlGroup } from 'angular2/common';
import {Typeahead} from '../node_modules/ng2-bootstrap/ng2-bootstrap';
import {IConfigService, ConfigService} from './services/configService';
import {IRemoteService, RemoteService} from './services/remoteService';
import {IAuthService, AuthService, TransientInfo} from './services/authService';
import {IShellService, ShellService, ShellInfo} from './services/shellService';
import {ISearchService, SearchService} from './services/searchService';

import {IDataService, DataService} from './services/dataService';

import {dto} from './core/dto';

import {Components} from './components';

import {enableProdMode} from "angular2/core";

enableProdMode();

declare var System:any;

@Component(
{
    selector: 'aims-app',
    templateUrl: './application/app.html',  
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES],
    pipes: []
})
     
@RouteConfig([
        new Route({ path: '/', component: Home, name: 'Home' }),


        new Route({ path: '/EditPublic', component: Components.EditPublic, name: 'EditPublic' }),
        new Route({ path: '/EditPublic/:Id', component: Components.EditPublic, name: 'EditPublic' }),

        new Route({ path: '/ListReportingEntityProfile', component: Components.ListReportingEntityProfile, name: 'ListReportingEntityProfile' }),
        new Route({ path: '/EditReportingEntityProfile', component: Components.EditReportingEntityProfile, name: 'EditReportingEntityProfile' }),
        new Route({ path: '/EditReportingEntityProfile/:Id', component: Components.EditReportingEntityProfile, name: 'EditReportingEntityProfile' }),

        new Route({ path: '/ListPolicyType', component: Components.ListPolicyType, name: 'ListPolicyType' }),
        new Route({ path: '/EditPolicyType', component: Components.EditPolicyType, name: 'EditPolicyType' }),
        new Route({ path: '/EditPolicyType/:Id', component: Components.EditPolicyType, name: 'EditPolicyType' }),

        new Route({ path: '/ListOperatorType', component: Components.ListOperatorType, name: 'ListOperatorType' }),
        new Route({ path: '/EditOperatorType', component: Components.EditOperatorType, name: 'EditOperatorType' }),
        new Route({ path: '/EditOperatorType/:Id', component: Components.EditOperatorType, name: 'EditOperatorType' }),

        new Route({ path: '/ListInsurableItemClass', component: Components.ListInsurableItemClass, name: 'ListInsurableItemClass' }),
        new Route({ path: '/EditInsurableItemClass', component: Components.EditInsurableItemClass, name: 'EditInsurableItemClass' }),    
        new Route({ path: '/EditInsurableItemClass/:Id', component: Components.EditInsurableItemClass, name: 'EditInsurableItemClass' })
                    
        
    //new Route({ path: '/Login', component: LoginController, name: 'Login' })
])
 
class AIMSApp {

    router: Router;
    location: Location;
    
    userInfo: TransientInfo;
    shellInfo: ShellInfo;
        
    authService: IAuthService;
    shellService: IShellService;
    
    uibModal: any;

    constructor(
        @Inject(Router) router: Router,
        @Inject(Location) location: Location,        
        @Inject(IAuthService) authService: IAuthService,
        @Inject(IShellService) shellService: IShellService,
        @Inject(IDataService) dataService: IDataService
        
    ){

        this.router = router;
        this.location = location;
        this.authService = authService;
        this.shellService = shellService;
        
        this.userInfo = authService.userInfo;
        this.shellInfo = shellService.info;
                
    }
        

    private onLogoutResponse(sender: AIMSApp, data: any, status: number): any {
        sender.router.navigate(["Home"]);
        sender.shellService.toastInfo("LOGOUT_TITLE", "LOGOUT_TEXT");
    }

    public getLinkStyle(path) {
        if (path === this.location.path()) {
            return true;
        }
        else if (path.length > 0) {
            return this.location.path().indexOf(path) > -1;
        }
    }

    public signOut() {        
        this.authService.logout(this, this.onLogoutResponse);
    }

    
}

class ComponentHelper{

    static LoadComponentAsync(name,path){
        return System.import(path).then(c => c[name]);
    }
}

bootstrap(AIMSApp, [
    HTTP_PROVIDERS,
    ROUTER_PROVIDERS,    
    FormBuilder,    
    provide(IConfigService, { useClass: ConfigService }),
    provide(IRemoteService, { useClass: RemoteService }),
    provide(IDataService, { useClass: DataService }),
    provide(IAuthService, { useClass: AuthService }),
    provide(IShellService, { useClass: ShellService }),
    provide(ISearchService, { useClass: SearchService }),
    provide(LocationStrategy, { useClass: HashLocationStrategy })
]);
