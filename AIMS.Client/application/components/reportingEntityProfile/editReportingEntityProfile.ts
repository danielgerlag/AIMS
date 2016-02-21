import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {FormBuilder, Validators, ControlGroup, Control, NgClass, FORM_BINDINGS, CORE_DIRECTIVES, FORM_DIRECTIVES, JsonPipe} from 'angular2/common';
import {RouteParams} from 'angular2/router';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {Component, View} from 'angular2/core';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {EntitySummary} from '../../directives/input/entitySummary';
import {JournalTemplates} from '../journalTemplate/journalTemplates';

import {IShellService} from '../../services/shellService';
import {IAuthService} from '../../services/authService';
import {IDataService} from '../../services/dataService';
import {ILogService} from '../../services/logService';
import {CRUDController} from '../../core/crudController';

@Component({    
    templateUrl: './application/components/reportingEntityProfile/editReportingEntityProfile.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, FormInput, NgClass, EntitySummary, EntityDropdown, TAB_DIRECTIVES, JournalTemplates],
    pipes: [JsonPipe]
})
export class EditReportingEntityProfile extends CRUDController {

    constructor(params: RouteParams, router: Router, location: Location, dataService: IDataService, shellService: IShellService, authService: IAuthService, fb: FormBuilder, logService: ILogService) {
        super(params, router, location, dataService, shellService, authService, fb, logService);
        this.title = "Reporting Entity Profile";
    }

    protected typeName(): string {
        return "ReportingEntityProfile";
    }

    protected setName(): string {
        return "ReportingEntityProfiles";
    }


    protected afterSave(sender: EditReportingEntityProfile, data: any) {
        super.afterSave(sender, data);
        sender.router.navigate(["Home"]);
    }

    protected expandFields(): string[] {
        var result = super.expandFields();
        result.push("LedgerAccounts");
        result.push("CoverageProfiles");
        result.push("JournalTemplates");
        result.push("JournalTemplates.Inputs");
        result.push("JournalTemplates.JournalTemplateTxns");
        result.push("JournalTemplates.JournalTemplateTxns.Postings");
        return result;
    }

    protected addLedgerAccount() {
        var item = this.dataService.createEntity("LedgerAccount", {});
        this.entity.LedgerAccounts.push(item);
    }

    protected addCoverageProfile() {
        var item = this.dataService.createEntity("CoverageProfile", {});
        this.entity.CoverageProfiles.push(item);
    }

    protected addJournalTemplate() {
        var item = this.dataService.createEntity("JournalTemplate", {});
        this.entity.JournalTemplates.push(item);
    }

    protected addJournalTemplateInput(journalTemplate) {
        var item = this.dataService.createEntity("JournalTemplateInput", {});
        journalTemplate.Inputs.push(item);
    }

    protected addJournalTemplateTxn(journalTemplate) {
        var item = this.dataService.createEntity("JournalTemplateTxn", {});
        journalTemplate.JournalTemplateTxns.push(item);
    }

    protected addJournalTemplateTxnPosting(journalTemplateTxn) {
        var item = this.dataService.createEntity("JournalTemplateTxnPosting", {});
        journalTemplateTxn.Postings.push(item);
    }
    
    

    protected removeLedgerAccount(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }

    protected removeCoverageProfile(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }

    protected removeJournalTemplate(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }
}