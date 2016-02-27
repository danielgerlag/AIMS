import {Component, OnInit, EventEmitter, View, provide, ElementRef, Injector, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {LookupText} from '../../directives/input/lookupText';
import {FormInput} from '../../directives/input/formInput';
import {EditOperator} from '../../components/operator/editOperator';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {SubViewList} from '../../core/subViewList'

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';

@Component({
    selector: 'policyOperators',    
    providers: [Modal],
    inputs: ['value', 'dataService', 'policySubType'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/directives/policy/policyOperators.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, LookupText]
})
export class PolicyOperators extends SubViewList {
        
    private policySubType: breeze.Entity;    
    
    constructor(modal: Modal, elementRef: ElementRef, injector: Injector, _renderer: Renderer) {
        super(modal, elementRef, injector, _renderer);
    }    
     
    protected getNewSubView(): any {
        return EditOperator;
    }
      
    protected getEditSubView(): any {
        return EditOperator;
    }

    protected createChildEntity(type): any {
        var item = this.dataService.createEntity("Operator", {});
        item.OperatorType = type;
        return item;
    }

    protected onAdd(item) {
        this.entity['Operators'].push(item);
    }
    
    protected onEntityChanged() {
        super.onEntityChanged();
        this.loadNavigationGraph("Operators", "OperatorPublic");
    }
    
}

