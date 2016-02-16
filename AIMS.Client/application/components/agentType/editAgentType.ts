import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {FormBuilder, Validators, ControlGroup, Control, NgClass, FORM_BINDINGS, CORE_DIRECTIVES, FORM_DIRECTIVES, JsonPipe} from 'angular2/common';
import {RouteParams} from 'angular2/router';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {Component, View} from 'angular2/core';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {EntitySummary} from '../../directives/input/entitySummary';

import {IShellService} from '../../services/shellService';
import {IAuthService} from '../../services/authService';
import {IDataService} from '../../services/dataService';
import {CRUDController} from '../../core/crudController';

@Component({    
    templateUrl: './application/components/agentType/editAgentType.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, FormInput, NgClass, EntitySummary, EntityDropdown, TAB_DIRECTIVES],
    pipes: [JsonPipe]
})
export class EditAgentType extends CRUDController {

    constructor(params: RouteParams, router: Router, location: Location, dataService: IDataService, shellService: IShellService, authService: IAuthService, fb: FormBuilder) {
        super(params, router, location, dataService, shellService, authService, fb);
        this.title = "Agent Type";
    }

    protected typeName(): string {
        return "AgentType";
    }

    protected setName(): string {
        return "AgentTypes";
    }


    protected afterSave(sender: EditAgentType, data: any) {
        super.afterSave(sender, data);
        sender.router.navigate(["Home"]);
    }

   
}