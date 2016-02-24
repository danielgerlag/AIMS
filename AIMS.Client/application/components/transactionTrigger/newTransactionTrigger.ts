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
    templateUrl: './application/components/transactionTrigger/newTransactionTrigger.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, DateInput, PublicSelector, TransactionTriggerInputs]
})
export class NewTransactionTrigger implements OnInit, ICustomModalComponent {
        
    dataService: IDataService;
    entity: any;
    dialog: ModalDialogInstance;
    origin: string;
    templateInputsLoaded: boolean;

    availableEntities: any = [];
    availableTemplates: any = [];

    protected step: number;

    onDateChange(e) {
        this.entity.EffectiveFrom = e.sender.value();
    }

    ngOnInit() {

    }

    onInit() {
        this.ngOnInit();
    }

    constructor(dialog: ModalDialogInstance, data: ICustomModal, dataService: IDataService) {
        this.dialog = dialog;
        this.entity = data;
        this.dataService = dataService;
        this.step = 1;
        this.origin = 'P';
        this.templateInputsLoaded = false;
    }


    beforeDismiss(): boolean {
        return true;
    }

    beforeClose(): boolean {
        return false;
    }

    protected finish() {
        this.dialog.close(this.entity);
    }

    protected cancel() {
        this.dialog.close(null);
    }

    protected nextStep() {
        if (this.step == 2)
            this.initTxnTrigger(this.entity.JournalTemplateID);
        this.step++;
    }

    protected prevStep() {
        this.step--;
    }
    

    protected initTxnTrigger(journalTemplateID: number) {

        this.dataService.getEntity(this, "JournalTemplates", journalTemplateID, "Inputs, Inputs.AttributeDataType", false, this.onLoadConfig, this.onConfigFailure);
    }

    protected onLoadConfig(sender: NewTransactionTrigger, data: breeze.QueryResult): any {
        sender.entity.JournalTemplate = data.results[0];
        sender.templateInputsLoaded = true;
        //sender.entity.TransactionOrigin = sender.entity.JournalTemplate.TransactionOrigin;
    }

    protected onConfigFailure(sender: NewTransactionTrigger, data: any): any {
        alert(data);
    }
}

