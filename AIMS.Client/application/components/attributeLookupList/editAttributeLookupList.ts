﻿import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
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
import {ILogService} from '../../services/logService';
import {CRUDController} from '../../core/crudController';

@Component({    
    templateUrl: './application/components/attributeLookupList/editAttributeLookupList.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, FormInput, NgClass, EntitySummary, EntityDropdown, TAB_DIRECTIVES],
    pipes: [JsonPipe]
})
export class EditAttributeLookupList extends CRUDController {

    constructor(params: RouteParams, router: Router, location: Location, dataService: IDataService, shellService: IShellService, authService: IAuthService, fb: FormBuilder, logService: ILogService) {
        super(params, router, location, dataService, shellService, authService, fb, logService);
        this.title = "Attribute Lookup List";
    }

    protected typeName(): string {
        return "AttributeLookupList";
    }

    protected setName(): string {
        return "AttributeLookupLists";
    }

    protected expandFields(): string[] {
        var result = super.expandFields();
        result.push("Items");
        return result;
    }


    protected afterSave(sender: EditAttributeLookupList, data: any) {
        super.afterSave(sender, data);
        sender.router.navigate(["Home"]);
    }

    protected addItem() {
        var item = this.dataService.createEntity("AttributeLookupItem", {});
        this.entity.Items.push(item);
    }

    protected removeItem(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }

   
}