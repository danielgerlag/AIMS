import {bootstrap} from 'angular2/platform/browser'
import {Inject, Component, View, provide} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';
import {HTTP_PROVIDERS} from './core/xhr_backend';
import {Home} from './home/home';
import {LoginController} from './account/login'
import {DocumentCategoryController, DocumentTemplateController, DocumentDefinitionController, ContentFragmentController, DocumentBuilder, DocumentArchiver} from './forms'
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

import {enableProdMode} from "angular2/core";

enableProdMode();

declare var System:any;

@Component(
{
    selector: 'westdoc-app',
    templateUrl: './application/app.html',  
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES],
    pipes: []
})
     
@RouteConfig([
    new Route({ path: '/', component: Home, name: 'Home' }),    

    new Route({ path: '/DocumentCategory', component: DocumentCategoryController, name: 'DocumentCategory' }),    
    new Route({ path: '/DocumentCategory/:Id', component: DocumentCategoryController, name: 'DocumentCategory' }),    

    new Route({ path: '/DocumentTemplate', component: DocumentTemplateController, name: 'DocumentTemplate' }),
    new Route({ path: '/DocumentTemplate/:Id', component: DocumentTemplateController, name: 'DocumentTemplate' }),    

    new Route({ path: '/DocumentDefinition', component: DocumentDefinitionController, name: 'DocumentDefinition' }),
    new Route({ path: '/DocumentDefinition/:Id', component: DocumentDefinitionController, name: 'DocumentDefinition' }),    

    new Route({ path: '/ContentFragment', component: ContentFragmentController, name: 'ContentFragment' }),
    new Route({ path: '/ContentFragment/:Id', component: ContentFragmentController, name: 'ContentFragment' }),    

    new Route({ path: '/DocumentBuilder', component: DocumentBuilder, name: 'DocumentBuilder' }),
    new Route({ path: '/DocumentArchiver', component: DocumentArchiver, name: 'DocumentArchiver' }),       
            
        
    new Route({ path: '/Login', component: LoginController, name: 'Login' })
])
 
class WestDocApp {

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
        

    private onLogoutResponse(sender: WestDocApp, data: any, status: number): any {
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

bootstrap(WestDocApp, [
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
