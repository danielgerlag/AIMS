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
    templateUrl: './application/components/policyType/editPolicyType.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, FormInput, NgClass, EntitySummary, EntityDropdown, TAB_DIRECTIVES],
    pipes: [JsonPipe]
})
export class EditPolicyType extends CRUDController {

    constructor(params: RouteParams, router: Router, location: Location, dataService: IDataService, shellService: IShellService, authService: IAuthService, fb: FormBuilder) {
        super(params, router, location, dataService, shellService, authService, fb);
        this.title = "Policy Type";
    }

    protected typeName(): string {
        return "PolicyType";
    }

    protected setName(): string {
        return "PolicyTypes";
    }


    protected afterSave(sender: EditPolicyType, data: any) {
        super.afterSave(sender, data);
        sender.router.navigate(["Home"]);
    }

    protected expandFields(): string[] {
        var result = super.expandFields();
        result.push("EntityRequirements");
        result.push("AgentRequirements");
        result.push("ServiceProviders");
        result.push("PolicySubTypes");
        result.push("ItemClasses");
        
        return result;
    }

    protected addEntityRequirement() {
        var item = this.dataService.createEntity("PolicyTypeEntityRequirement", {});
        this.entity.EntityRequirements.push(item);
    }

    protected addAgentRequirement() {
        var item = this.dataService.createEntity("PolicyTypeAgentRequirement", {});
        this.entity.AgentRequirements.push(item);
    }

    protected addServiceProvider() {
        var item = this.dataService.createEntity("PolicyTypeServiceProvider", {});
        this.entity.ServiceProviders.push(item);
    }

    protected addPolicySubType() {
        var item = this.dataService.createEntity("PolicySubType", {});
        this.entity.PolicySubTypes.push(item);
    }

    protected addItemClass() {
        var item = this.dataService.createEntity("PolicyTypeItemClass", {});
        this.entity.ItemClasses.push(item);
    }    

    protected removeEntityRequirement(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }

    protected removeAgentRequirement(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }

    protected removeServiceProvider(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }

    protected removeItemClass(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }
}