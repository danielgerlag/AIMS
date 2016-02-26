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
    }
    //query="JournalTemplates?$filter=ReportingEntityProfileID eq {{entity.EntityRequirement.ReportingEntityProfileID}}"

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

        breeze.EntityQuery.from("JournalTemplates")
            .where

        this.dataService.query(this, 
    }
    
}

