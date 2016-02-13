import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {FormBuilder, Validators, ControlGroup, Control, NgClass, FORM_BINDINGS, CORE_DIRECTIVES, FORM_DIRECTIVES, JsonPipe} from 'angular2/common';
import {RouteParams} from 'angular2/router';

import {Component, View} from 'angular2/core';

import {FormInput} from '../../directives/input/formInput';
import {EntitySummary} from '../../directives/input/entitySummary';

import {IShellService} from '../../services/shellService';
import {IAuthService} from '../../services/authService';
import {IDataService} from '../../services/dataService';
import {ListController} from '../../core/listController'; 

@Component({
    //selector: 'list',
    templateUrl: './application/forms/documentCategory/documentCategory.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, FormInput, NgClass, EntitySummary],
    pipes: [JsonPipe]
})
export class DocumentCategoryController extends ListController {

    constructor(params: RouteParams, router: Router, location: Location, dataService: IDataService, shellService: IShellService, authService: IAuthService, fb: FormBuilder) {
        super(params, router, location, dataService, shellService, authService, fb);
    }

    protected typeName(): string {
        return "DocumentCategory";
    }

    protected setName(): string {
        return "DocumentCategories";
    }


    protected afterSave(sender: DocumentCategoryController, data: any) {
        super.afterSave(sender, data);
        sender.router.navigate(["Home"]);
    }

}