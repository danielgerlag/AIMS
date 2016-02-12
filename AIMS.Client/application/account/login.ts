import {Component, View, provide} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';
import {FormBuilder, Validators, ControlGroup, Control, NgClass} from 'angular2/common';
import {FormInput} from '../directives/input/formInput';
import {FormSummary} from '../directives/input/formSummary';

import {IShellService} from '../services/shellService';
import {IAuthService} from '../services/authService';
import {dto} from '../core/dto';
import {FormController} from '../core/baseController';
import {TranslateService, TranslatePipe} from 'ng2-translate/ng2-translate';
import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';

import {Account} from '../core/models';


@Component({
    selector: 'login',
    templateUrl: './application/account/login.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, FormInput, FormSummary],
    pipes: [TranslatePipe]
})
export class LoginController extends FormController<Account.LoginRequest> {
    
    router: Router;
    authService: IAuthService;    

    constructor(router: Router, shellService: IShellService, authService: IAuthService, fb: FormBuilder) {
        super(shellService, fb);                
        this.router = router;        
        this.authService = authService;        
    }

    protected createModel(): Account.LoginRequest {
        return new Account.LoginRequest();
    }

    protected doSubmit(data: any) {
        //this.shellService.showLoader("Logging in...");
        this.authService.login(this.model, this, this.onResponse);        
    }        

    protected onResponse(sender: LoginController, data: any, status: number): any {
        super.onResponse(sender, data, status);

        if (status == 200) {            
            sender.router.navigate(["Home"]);
            sender.shellService.toastSuccess("LOGIN_SUCCESS_TITLE", "LOGIN_SUCCESS_TEXT");
        }
        else {
            sender.shellService.toastError("LOGIN_FAIL_TITLE", "LOGIN_FAIL_TEXT");
        }
       
    }        

}


