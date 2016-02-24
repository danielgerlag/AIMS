import {Component, OnInit, EventEmitter, View, provide, ElementRef, Injector, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {LookupText} from '../../directives/input/lookupText';
import {FormInput} from '../../directives/input/formInput';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {NewTransactionTrigger} from './newTransactionTrigger'
import {EditTransactionTrigger} from './editTransactionTrigger'

import {SubViewList} from '../../core/subViewList'

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';

@Component({
    selector: 'transactionTriggers',    
    providers: [Modal],
    inputs: ['value', 'dataService', 'type'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/components/transactionTrigger/transactionTriggers.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, LookupText]
})
export class TransactionTriggers extends SubViewList {      
    
    type: string;
    
    constructor(modal: Modal, elementRef: ElementRef, injector: Injector, _renderer: Renderer) {
        super(modal, elementRef, injector, _renderer);
    }    
     
    
    protected getNewSubView(): any {
        return NewTransactionTrigger;
    }
      
    protected getEditSubView(): any {
        return EditTransactionTrigger;
    }

    protected createChildEntity(type): any {
        var item = this.dataService.createEntity(this.type, {});  
        item.TransactionTrigger = this.dataService.createEntity("TransactionTrigger", {});  
        return item;
    }

    protected onAdd(item) {
        this.entity['TransactionTriggers'].push(item);
        //this.onEntityChanged();
    }

    
     
}

