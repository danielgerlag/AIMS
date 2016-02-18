import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {PublicSelector} from '../../directives/public/publicSelector';
//import {IRemoteService} from '../../services/remoteService';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'
import {JournalTemplateTxns} from './journalTemplateTxns'

import {Modal} from 'angular2-modal/angular2-modal';
import {ModalDialogInstance} from 'angular2-modal/angular2-modal';
import {ICustomModal, ICustomModalComponent} from 'angular2-modal/angular2-modal';



@Component({
    selector: 'editInsurableItem',
})
@View({
    templateUrl: './application/components/journalTemplate/editJournalTemplate.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, PublicSelector, JournalTemplateTxns]
})
export class EditJournalTemplate implements OnInit, ICustomModalComponent {
        
    dataService: IDataService;
    entity: any;
    dialog: ModalDialogInstance;    

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
        var item = this.dataService.createEntity("JournalTemplateInput", {});        
        //item.JournalTemplate = this.entity;
        this.entity.Inputs.push(item);
    }

    protected addJournalTemplateTxn() {
        var item = this.dataService.createEntity("JournalTemplateTxn", {});        
        this.entity.JournalTemplateTxns.push(item);
    }

    protected removeInput(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }

    protected removeJournalTemplateTxn(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }
}

