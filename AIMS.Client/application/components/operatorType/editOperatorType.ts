﻿import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {FormBuilder, Validators, ControlGroup, Control, NgClass, FORM_BINDINGS, CORE_DIRECTIVES, FORM_DIRECTIVES, JsonPipe} from 'angular2/common';
import {RouteParams} from 'angular2/router';
import {TAB_DIRECTIVES, ACCORDION_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {Component, View} from 'angular2/core';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {EntitySummary} from '../../directives/input/entitySummary';

import {IShellService} from '../../services/shellService';
import {IAuthService} from '../../services/authService';
import {IDataService} from '../../services/dataService';
import {CRUDController} from '../../core/crudController';

@Component({    
    templateUrl: './application/components/operatorType/editOperatorType.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, FormInput, NgClass, EntitySummary, EntityDropdown, TAB_DIRECTIVES, ACCORDION_DIRECTIVES],
    pipes: [JsonPipe]
})
export class EditOperatorType extends CRUDController {

    constructor(params: RouteParams, router: Router, location: Location, dataService: IDataService, shellService: IShellService, authService: IAuthService, fb: FormBuilder) {
        super(params, router, location, dataService, shellService, authService, fb);
        this.title = "Operator Type";
    }

    protected typeName(): string {
        return "OperatorType";
    }

    protected setName(): string {
        return "OperatorTypes";
    }


    protected afterSave(sender: EditOperatorType, data: any) {
        super.afterSave(sender, data);
        sender.router.navigate(["Home"]);
    }

    protected expandFields(): string[] {
        var result = super.expandFields();
        result.push("Groups");
        result.push("Groups.Attributes");
        return result;
    }

    protected addGroup() {
        var item = this.dataService.createEntity("OperatorTypeAttributeGroup", {});
        item.Name = "New Group";
        this.entity.Groups.push(item);
    }

    protected addAttribute(group) {
        var item = this.dataService.createEntity("OperatorTypeAttribute", {});
        group.Attributes.push(item);
    }

    protected removeAttribute(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }
}