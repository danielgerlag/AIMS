import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {DateInput} from '../../directives/input/dateInput';
import {PublicSelector} from '../../directives/public/publicSelector';
//import {IRemoteService} from '../../services/remoteService';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {TransactionTriggerInputs} from './transactionTriggerInputs'

import {Modal} from 'angular2-modal/angular2-modal';
import {ModalDialogInstance} from 'angular2-modal/angular2-modal';
import {ICustomModal, ICustomModalComponent} from 'angular2-modal/angular2-modal';


@Component({
    selector: 'transactionTrigger',
})
@View({
    templateUrl: './application/components/transactionTrigger/editTransactionTrigger.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, DateInput, PublicSelector, TransactionTriggerInputs]
})
export class EditTransactionTrigger implements OnInit, ICustomModalComponent {
        
    dataService: IDataService;
    entity: any;
    trigger: any;
    dialog: ModalDialogInstance;    
    origin: string;

    ngOnInit() {
    }

    onInit() {
        this.ngOnInit();
    }

    constructor(dialog: ModalDialogInstance, data: ICustomModal, dataService: IDataService) {
        this.dialog = dialog;
        this.entity = data;
        this.trigger = this.entity.TransactionTrigger;
        this.dataService = dataService;

        var e: breeze.Entity = this.entity;
        if (e.entityType.shortName == "PolicyTransactionTrigger") {
            this.origin = 'P';
        }

        if (e.entityType.shortName == "ReportingEntityTransactionTrigger") {
            this.origin = 'G';
        }
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
        
    
}

