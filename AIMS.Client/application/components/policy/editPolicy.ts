import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {FormBuilder, Validators, ControlGroup, Control, NgClass, FORM_BINDINGS, CORE_DIRECTIVES, FORM_DIRECTIVES, JsonPipe} from 'angular2/common';
import {RouteParams} from 'angular2/router';
import {TAB_DIRECTIVES, ACCORDION_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {Component, View} from 'angular2/core';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {EntitySummary} from '../../directives/input/entitySummary';
import {DateInput} from '../../directives/input/dateInput';

import {ContextParameterValues} from '../../components/contextParameter/contextParameterValues';

import {PolicyReportingEntities} from '../../directives/policy/policyReportingEntities';
import {PolicyServiceProviders} from '../../directives/policy/policyServiceProviders';
import {PolicyRiskLocations} from '../../directives/policy/policyRiskLocations';
import {PolicyOperators} from '../../directives/policy/policyOperators';
import {PolicyInsurableItems} from '../../directives/policy/policyInsurableItems';
import {PolicyHolders} from '../../directives/policy/policyHolders';

import {TransactionTriggers} from '../../components/transactionTrigger/transactionTriggers';
import {JournalExplorer} from '../../components/journal/journalExplorer';
import {LedgerBalances} from '../../components/journal/ledgerBalances';


import {IShellService} from '../../services/shellService';
import {IAuthService} from '../../services/authService';
import {IDataService} from '../../services/dataService';
import {ILogService} from '../../services/logService';
import {CRUDController} from '../../core/crudController';

@Component({    
    templateUrl: './application/components/policy/editPolicy.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, FormInput, NgClass, EntitySummary, EntityDropdown, TAB_DIRECTIVES, ACCORDION_DIRECTIVES, PolicyReportingEntities, PolicyServiceProviders, PolicyRiskLocations, PolicyOperators, PolicyInsurableItems, PolicyHolders, TransactionTriggers, JournalExplorer, LedgerBalances, DateInput, ContextParameterValues],
    pipes: [JsonPipe]
})
export class EditPolicy extends CRUDController {

    private ready: boolean;
    ledgerBalanceDate: Date;

    constructor(params: RouteParams, router: Router, location: Location, dataService: IDataService, shellService: IShellService, authService: IAuthService, fb: FormBuilder, logService: ILogService) {
        super(params, router, location, dataService, shellService, authService, fb, logService);
        this.title = "Policy";
        this.ready = false;
        this.ledgerBalanceDate = moment().toDate();
    }

    protected typeName(): string {
        return "Policy";
    }

    protected setName(): string {
        return "Policies";
    }


    protected afterSave(sender: EditPolicy, data: any) {
        super.afterSave(sender, data);
        sender.router.navigate(["Home"]);
    }

    protected expandFields(): string[] {
        var result = super.expandFields();
        result.push("PolicyHolders");
        result.push("PolicyHolders.Public");
        result.push("Coverages");
        result.push("RiskLocations");
        result.push("InsurableItems");
        result.push("InsurableItems.Attributes");
        result.push("InsurableItems.Operators");
        result.push("InsurableItems.Operators.Operator");
        result.push("InsurableItems.Operators.Operator.OperatorPublic");
        result.push("InsurableItems.PolicyCoverages");
        result.push("InsurableItems.PolicyRiskLocation");
        result.push("Agents");
        result.push("Operators");
        result.push("ReportingEntities");
        result.push("ServiceProviders");

        result.push("TransactionTriggers");
        result.push("TransactionTriggers.TransactionTrigger");
        result.push("TransactionTriggers.TransactionTrigger.JournalTemplate");
        result.push("TransactionTriggers.TransactionTrigger.Inputs");        

        result.push("ContextParameterValues");
        result.push("ContextParameterValues.ContextParameterValue");
        
        result.push("PolicySubType");

        result.push("PolicySubType.PolicyType");
        result.push("PolicySubType.PolicyType.EntityRequirements");
        result.push("PolicySubType.PolicyType.AgentRequirements");
        result.push("PolicySubType.PolicyType.ServiceProviders");
        result.push("PolicySubType.PolicyType.ItemClasses");
        result.push("PolicySubType.PolicyType.ItemClasses.InsurableItemClass");
        result.push("PolicySubType.PolicyType.ItemClasses.InsurableItemClass.OperatorTypes");
        result.push("PolicySubType.PolicyType.ItemClasses.InsurableItemClass.OperatorTypes.OperatorType");
        result.push("PolicySubType.PolicyType.ItemClasses.InsurableItemClass.Groups");
        result.push("PolicySubType.PolicyType.ItemClasses.InsurableItemClass.Groups.Attributes");
        result.push("PolicySubType.Coverages");
        result.push("PolicySubType.Coverages.CoverageType");        
        
        return result;
    }

    protected onLoadSuccess(sender: EditPolicy, data: breeze.QueryResult): any {
        super.onLoadSuccess(sender, data)
        sender.ready = true;
    }
    
}