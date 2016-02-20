import {Component, OnInit, EventEmitter, Input, View, provide, ElementRef, Injector, IterableDiffers, KeyValueDiffers, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import { ACCORDION_DIRECTIVES, TAB_DIRECTIVES } from 'ng2-bootstrap/ng2-bootstrap';


import {EntityDropdown} from '../../directives/input/entityDropdown';
import {LookupText} from '../../directives/input/lookupText';
import {FormInput} from '../../directives/input/formInput';

import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';

@Component({
    selector: 'transactionTriggerInputs',    
    providers: [Modal],
    inputs: ['value', 'dataService', 'journalTemplate'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/components/transactionTrigger/transactionTriggerInputs.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ACCORDION_DIRECTIVES, EntityDropdown, FormInput, LookupText]
})
export class TransactionTriggerInputs implements OnInit {

    private dataService: IDataService;
    private entity: breeze.Entity;
    private journalTemplate: breeze.Entity;
    public valueChange: EventEmitter<any> = new EventEmitter();        
    
    
    constructor() {        
    }

    ngOnInit() {
        //this.entity.entityAspect.loadNavigationProperty("Inputs");
        this.dataService.getEntity(this, "JournalTemplates", this.journalTemplate['ID'], "Inputs, Inputs.AttributeDataType", false, this.onTypeDataRecieved, this.onTypeDataFailed);        
    }

    onInit() {
        this.ngOnInit();
    }

    onTypeDataRecieved(sender: TransactionTriggerInputs, data: breeze.QueryResult) {
        sender.journalTemplate = data.results[0];        
    }

    onTypeDataFailed(sender: TransactionTriggerInputs, reason: any) {
    }

    public get value() {
        return this.entity;
    }
    public set value(value) {        
        this.entity = value;
        this.onEntityChanged();
    }    

    onEntityChanged() {        
        this.valueChange.next(this.entity);
    }        
        
    findInput(journalTemplateInputID) {
        var txnTrigger: any = this.entity;
        for (let inp of txnTrigger.Inputs) {
            if (inp.JournalTemplateInputID == journalTemplateInputID) {
                return inp;
            }
        }
        var newInp = this.dataService.createEntity("TransactionTriggerInput", {});
        newInp.JournalTemplateInputID = journalTemplateInputID;
        txnTrigger.Inputs.push(newInp);
        return newInp;
    }    
    
}

