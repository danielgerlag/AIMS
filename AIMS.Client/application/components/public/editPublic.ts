import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {FormBuilder, Validators, ControlGroup, Control, NgClass, FORM_BINDINGS, CORE_DIRECTIVES, FORM_DIRECTIVES, JsonPipe} from 'angular2/common';
import {RouteParams} from 'angular2/router';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {Component, View} from 'angular2/core';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {EntitySummary} from '../../directives/input/entitySummary';
import {PublicGeneral} from '../../directives/public/publicGeneral';
import {PublicContactDetails} from '../../directives/public/publicContactDetails';
import {IShellService} from '../../services/shellService';
import {IAuthService} from '../../services/authService';
import {IDataService} from '../../services/dataService';
import {CRUDController} from '../../core/crudController';

@Component({    
    templateUrl: './application/components/public/editPublic.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, FormInput, NgClass, EntitySummary, EntityDropdown, TAB_DIRECTIVES, PublicGeneral, PublicContactDetails],
    pipes: [JsonPipe]
})
export class EditPublic extends CRUDController {

    constructor(params: RouteParams, router: Router, location: Location, dataService: IDataService, shellService: IShellService, authService: IAuthService, fb: FormBuilder) {
        super(params, router, location, dataService, shellService, authService, fb);
        this.title = "Public";
    }

    protected typeName(): string {
        return "Public";
    }

    protected setName(): string {
        return "Publics";
    }


    protected afterSave(sender: EditPublic, data: any) {
        super.afterSave(sender, data);
        sender.router.navigate(["Home"]);
    }

   
}