import {Component, OnInit, EventEmitter, View, provide, ElementRef, Injector, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {LookupText} from '../../directives/input/lookupText';
import {FormInput} from '../../directives/input/formInput';
import {EditJournalTemplateTxn} from '../../components/journalTemplate/editJournalTemplateTxn';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {SubViewList} from '../../core/subViewList'

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';

@Component({
    selector: 'journalTemplateTxns',    
    providers: [Modal],
    inputs: ['value', 'dataService'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/components/journalTemplate/journalTemplateTxns.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, LookupText]
})
export class JournalTemplateTxns extends SubViewList {      
    
    
    constructor(modal: Modal, elementRef: ElementRef, injector: Injector, _renderer: Renderer) {
        super(modal, elementRef, injector, _renderer);
    }    
       
    protected getSubView(): any {
        return EditJournalTemplateTxn;
    }

    protected createChildEntity(type): any {
        var item = this.dataService.createEntity("JournalTemplateTxn", {});
        item.JournalTemplate = this.entity;     
        return item;
    }

    protected onAdd(item) {
        this.entity['JournalTemplateTxns'].push(item);
    }
    
    
}

