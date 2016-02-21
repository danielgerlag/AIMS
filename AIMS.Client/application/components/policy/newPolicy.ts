import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {FormBuilder, Validators, ControlGroup, Control, NgClass, FORM_BINDINGS, CORE_DIRECTIVES, FORM_DIRECTIVES, JsonPipe} from 'angular2/common';
import {RouteParams} from 'angular2/router';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {Component, View} from 'angular2/core';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {EntitySummary} from '../../directives/input/entitySummary';

import {PolicyReportingEntities} from '../../directives/policy/policyReportingEntities';
import {PolicyServiceProviders} from '../../directives/policy/policyServiceProviders';
import {PolicyRiskLocations} from '../../directives/policy/policyRiskLocations';
import {PolicyOperators} from '../../directives/policy/policyOperators';
import {PolicyInsurableItems} from '../../directives/policy/policyInsurableItems';
import {PolicyHolders} from '../../directives/policy/policyHolders';

import {IShellService} from '../../services/shellService';
import {IAuthService} from '../../services/authService';
import {IDataService} from '../../services/dataService';
import {ILogService} from '../../services/logService';
import {CRUDController} from '../../core/crudController';

@Component({
    templateUrl: './application/components/policy/newPolicy.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, FormInput, NgClass, EntitySummary, EntityDropdown, TAB_DIRECTIVES, PolicyReportingEntities, PolicyServiceProviders, PolicyRiskLocations, PolicyOperators, PolicyInsurableItems, PolicyHolders],
    pipes: [JsonPipe]
})
export class NewPolicy extends CRUDController {

    protected policySubType: any;
    protected policyType: any;
    protected step: number;

    constructor(params: RouteParams, router: Router, location: Location, dataService: IDataService, shellService: IShellService, authService: IAuthService, fb: FormBuilder, logService: ILogService) {
        super(params, router, location, dataService, shellService, authService, fb, logService);
        this.title = "Policy";
        this.step = 1;
    }

    protected typeName(): string {
        return "Policy";
    }

    protected setName(): string {
        return "Policies";
    }


    protected nextStep() {
        if (this.step == 1)
            this.initPolicy(this.entity.PolicySubTypeID);
        this.step++;
    }

    protected prevStep() {
        this.step--;
    }

    protected afterSave(sender: NewPolicy, data: any) {
        super.afterSave(sender, data);
        sender.router.navigate(["Home"]);
    }
    

    protected initPolicy(policySubTypeID: number) {

        this.dataService.getEntity(this, "PolicySubTypes", policySubTypeID, "PolicyType, PolicyType.EntityRequirements, PolicyType.AgentRequirements, PolicyType.ServiceProviders, PolicyType.ItemClasses, PolicyType.ItemClasses.InsurableItemClass, PolicyType.ItemClasses.InsurableItemClass.OperatorTypes, PolicyType.ItemClasses.InsurableItemClass.OperatorTypes.OperatorType, PolicyType.ItemClasses.InsurableItemClass.Groups, PolicyType.ItemClasses.InsurableItemClass.Groups.Attributes, Coverages, Coverages.CoverageType", false, this.onLoadConfig, this.onConfigFailure);
    }

    protected onLoadConfig(sender: NewPolicy, data: breeze.QueryResult): any {
        sender.shellService.hideLoader();

        sender.policySubType = data.results[0];
        sender.policyType = data.results[0]['PolicyType'];

        for (let entityReq of sender.policyType.EntityRequirements) {
            var item = sender.dataService.createEntity("PolicyReportingEntity", {});
            //item.PolicyTypeEntityRequirementID = entityReq.PolicyTypeEntityRequirementID;
            item.PolicyTypeEntityRequirement = entityReq;
            item.ReportingEntityID = entityReq.DefaultReportingEntityID;
            sender.entity.ReportingEntities.push(item);
        }

        for (let sp of sender.policyType.ServiceProviders) {
            var item = sender.dataService.createEntity("PolicyServiceProvider", {});            
            item.ServiceProviderTypeID = sp.ServiceProviderTypeID;
            item.ServiceProviderID = sp.DefaultServiceProviderID;
            item.Percentage = 1;
            sender.entity.ServiceProviders.push(item);
        }

        

        //sender.entity.ReportingEntities
        //sender.entity.ServiceProviders
        //sender.entity.Agents

    }

    protected onConfigFailure(sender: NewPolicy, data: any): any {
        sender.shellService.hideLoader();
        alert(data);
    }

        
}