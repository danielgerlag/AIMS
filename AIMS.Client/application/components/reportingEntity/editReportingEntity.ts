import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {FormBuilder, Validators, ControlGroup, Control, NgClass, FORM_BINDINGS, CORE_DIRECTIVES, FORM_DIRECTIVES, JsonPipe} from 'angular2/common';
import {RouteParams} from 'angular2/router';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {Component, View} from 'angular2/core';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {PublicSelector} from '../../directives/public/publicSelector';
import {FormInput} from '../../directives/input/formInput';
import {EntitySummary} from '../../directives/input/entitySummary';

import {DateInput} from '../../directives/input/dateInput';

import {TransactionTriggers} from '../../components/transactionTrigger/transactionTriggers';
import {JournalExplorer} from '../../components/journal/journalExplorer';
import {LedgerBalances} from '../../components/journal/ledgerBalances';
import {DebtorCreditorBalances} from '../../components/journal/debtorCreditorBalances';

import {IShellService} from '../../services/shellService';
import {IAuthService} from '../../services/authService';
import {IDataService} from '../../services/dataService';
import {ILogService} from '../../services/logService';
import {CRUDController} from '../../core/crudController';

@Component({    
    templateUrl: './application/components/reportingEntity/editReportingEntity.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, FormInput, NgClass, EntitySummary, EntityDropdown, TAB_DIRECTIVES, PublicSelector, TransactionTriggers, JournalExplorer, LedgerBalances, DateInput, DebtorCreditorBalances],
    pipes: [JsonPipe]
})
export class EditReportingEntity extends CRUDController {

    ledgerBalanceDate: Date;

    constructor(params: RouteParams, router: Router, location: Location, dataService: IDataService, shellService: IShellService, authService: IAuthService, fb: FormBuilder, logService: ILogService) {
        super(params, router, location, dataService, shellService, authService, fb, logService);
        this.title = "Reporting Entity";
        this.ledgerBalanceDate = moment().toDate();
    }

    protected typeName(): string {
        return "ReportingEntity";
    }

    protected setName(): string {
        return "ReportingEntities";
    }

    protected expandFields(): string[] {
        var result = super.expandFields();
        result.push("Public");

        result.push("TransactionTriggers");
        result.push("TransactionTriggers.TransactionTrigger");        
        result.push("TransactionTriggers.TransactionTrigger.JournalTemplate");
        result.push("TransactionTriggers.TransactionTrigger.JournalTemplate.ServiceProviderType");
        result.push("TransactionTriggers.TransactionTrigger.JournalTemplate.AgentType");
        result.push("TransactionTriggers.TransactionTrigger.JournalTemplate.PublicRequirement");
        result.push("TransactionTriggers.TransactionTrigger.Inputs");
        
        return result;
    }


    protected afterSave(sender: EditReportingEntity, data: any) {
        super.afterSave(sender, data);
        sender.router.navigate(["Home"]);
    }

   
}