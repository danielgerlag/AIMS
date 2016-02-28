﻿import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {FormBuilder, Validators, ControlGroup, Control, NgClass, FORM_BINDINGS, CORE_DIRECTIVES, FORM_DIRECTIVES, JsonPipe} from 'angular2/common';
import {RouteParams} from 'angular2/router';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {Component, View} from 'angular2/core';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {PublicSelector} from '../../directives/public/publicSelector';
import {FormInput} from '../../directives/input/formInput';
import {DateInput} from '../../directives/input/dateInput';
import {EntitySummary} from '../../directives/input/entitySummary';
import {TransactionTriggers} from '../../components/transactionTrigger/transactionTriggers';
import {JournalExplorer} from '../../components/journal/journalExplorer';
import {LedgerBalances} from '../../components/journal/ledgerBalances';


import {IShellService} from '../../services/shellService';
import {IAuthService} from '../../services/authService';
import {IDataService} from '../../services/dataService';
import {ILogService} from '../../services/logService';
import {CRUDController} from '../../core/crudController';

@Component({    
    templateUrl: './application/components/agent/editAgent.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, FormInput, NgClass, EntitySummary, EntityDropdown, TAB_DIRECTIVES, PublicSelector, TransactionTriggers, JournalExplorer, LedgerBalances, DateInput],
    pipes: [JsonPipe]
})
export class EditAgent extends CRUDController {

    ledgerBalanceDate: Date;

    constructor(params: RouteParams, router: Router, location: Location, dataService: IDataService, shellService: IShellService, authService: IAuthService, fb: FormBuilder, logService: ILogService) {
        super(params, router, location, dataService, shellService, authService, fb, logService);
        this.title = "Agent";
        this.ledgerBalanceDate = moment().toDate();
    }

    protected typeName(): string {
        return "Agent";
    }

    protected setName(): string {
        return "Agents";
    }


    protected expandFields(): string[] {
        var result = super.expandFields();
        result.push("Public");        

        return result;
    }

    protected afterSave(sender: EditAgent, data: any) {
        super.afterSave(sender, data);
        sender.router.navigate(["Home"]);
    }

   
}