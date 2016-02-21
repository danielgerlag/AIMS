import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {FormBuilder, Validators, ControlGroup, Control, NgClass, FORM_BINDINGS, CORE_DIRECTIVES, FORM_DIRECTIVES, JsonPipe} from 'angular2/common';
import {RouteParams} from 'angular2/router';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {Component, View} from 'angular2/core';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {EntitySelector} from '../../directives/input/entitySelector';
import {FormInput} from '../../directives/input/formInput';
import {EntitySummary} from '../../directives/input/entitySummary';

import {IShellService} from '../../services/shellService';
import {IAuthService} from '../../services/authService';
import {IDataService} from '../../services/dataService';
import {ILogService} from '../../services/logService';
import {CRUDController} from '../../core/crudController';

@Component({    
    templateUrl: './application/components/serviceProvider/editServiceProvider.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, FormInput, NgClass, EntitySummary, EntityDropdown, TAB_DIRECTIVES, EntitySelector],
    pipes: [JsonPipe]
})
export class EditServiceProvider extends CRUDController {

    constructor(params: RouteParams, router: Router, location: Location, dataService: IDataService, shellService: IShellService, authService: IAuthService, fb: FormBuilder, logService: ILogService) {
        super(params, router, location, dataService, shellService, authService, fb, logService);
        this.title = "Service Provider";
    }

    protected typeName(): string {
        return "ServiceProvider";
    }

    protected setName(): string {
        return "ServiceProviders";
    }


    protected afterSave(sender: EditServiceProvider, data: any) {
        super.afterSave(sender, data);
        sender.router.navigate(["Home"]);
    }

   
}