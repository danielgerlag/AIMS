import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {PublicSelector} from '../../directives/public/publicSelector';
//import {IRemoteService} from '../../services/remoteService';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {Modal} from 'angular2-modal/angular2-modal';
import {ModalDialogInstance} from 'angular2-modal/angular2-modal';
import {ICustomModal, ICustomModalComponent} from 'angular2-modal/angular2-modal';



@Component({
    selector: 'editPolicyTypeTransitionJournalTemplate',
})
@View({
    templateUrl: './application/components/policyType/editPolicyTypeTransitionJournalTemplate.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, PublicSelector]
})
export class EditPolicyTypeTransitionJournalTemplate implements OnInit, ICustomModalComponent {
        
    dataService: IDataService;
    entity: any;
    dialog: ModalDialogInstance;    
    journalTemplates: any = [];

    ngOnInit() {
    }

    onInit() {
        this.ngOnInit();
    }

    constructor(dialog: ModalDialogInstance, data: ICustomModal, dataService: IDataService) {
        this.dialog = dialog;
        this.entity = data;
        this.dataService = dataService;

        this.entityReqChanged();
    }
    

    beforeDismiss(): boolean {
        return true;
    }

    beforeClose(): boolean {
        return false;
    }

    protected save() {
        this.dialog.close(this.entity);
    }

    protected cancel() {
        this.dialog.close(null);
    }

    protected addInput() {
        var item = this.dataService.createEntity("PolicyTypeTransitionJournalTemplateInput", {});        
        this.entity.Inputs.push(item);
    }    

    protected removeInput(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }


    protected entityReqChanged() {
        var entityRequirementID = this.entity.EntityRequirementID;
        if (entityRequirementID > 0) {
            var er = _.findWhere(this.entity.PolicyTypeTransition.PolicyType.EntityRequirements, { ID: entityRequirementID });
            var profileID = er['ReportingEntityProfileID'];
            var query = breeze.EntityQuery.from("JournalTemplates")
                .where(breeze.Predicate
                    .create('ReportingEntityProfileID', breeze.FilterQueryOp.Equals, profileID)
                    .and('TransactionOrigin', breeze.FilterQueryOp.Equals, 'P'));

            this.dataService.query(this, query, (sender: EditPolicyTypeTransitionJournalTemplate, data) => {
                sender.journalTemplates = data.results;
            }, (sender, data) => {
                //fail
                //todo: log
            });
        }
    }
    
}

