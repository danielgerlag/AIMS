import {Component, OnInit, EventEmitter, View, provide, ElementRef, Injector, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {LookupText} from '../../directives/input/lookupText';
import {FormInput} from '../../directives/input/formInput';
import {EditJournalTemplate} from '../../components/journalTemplate/editJournalTemplate';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {SubViewList} from '../../core/subViewList'

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';

@Component({
    selector: 'journalTemplates',    
    providers: [Modal],
    inputs: ['value', 'dataService'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/components/journalTemplate/journalTemplates.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, LookupText]
})
export class JournalTemplates extends SubViewList {      
    
    
    constructor(modal: Modal, elementRef: ElementRef, injector: Injector, _renderer: Renderer) {
        super(modal, elementRef, injector, _renderer);
    }    
     
    protected getNewSubView(): any {
        return EditJournalTemplate;
    }
      
    protected getEditSubView(): any {
        return EditJournalTemplate;
    }

    protected createChildEntity(type): any {
        var item = this.dataService.createEntity("JournalTemplate", {});
        item.ReportingEntityProfile = this.entity;
        return item;
    }

    protected onAdd(item) {
        this.entity['JournalTemplates'].push(item);
    }
    
    
}

