import {Component, OnInit, EventEmitter, View, provide, ElementRef, Injector, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {LookupText} from '../../directives/input/lookupText';
import {FormInput} from '../../directives/input/formInput';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {EditPolicyTypeTransition} from './editPolicyTypeTransition'

import {SubViewList} from '../../core/subViewList'

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';

@Component({
    selector: 'policyTypeTransitions',    
    providers: [Modal],
    inputs: ['value', 'dataService'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/components/policyType/policyTypeTransitions.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, LookupText]
})
export class PolicyTypeTransitions extends SubViewList {      
        
    constructor(modal: Modal, elementRef: ElementRef, injector: Injector, _renderer: Renderer) {
        super(modal, elementRef, injector, _renderer);
    }    
     
    
    protected getNewSubView(): any {
        return EditPolicyTypeTransition;
    }
      
    protected getEditSubView(): any {
        return EditPolicyTypeTransition;
    }

    protected createChildEntity(type): any {
        var item = this.dataService.createEntity("PolicyTypeTransition", {});
        item.PolicyType = this.value;
        return item;
    }

    protected onAdd(item) {
        this.entity['Transitions'].push(item);
    }

    protected edit(item: breeze.Entity) {
        //item['PolicyType'] = this.value;
        super.edit(item);
    }

    
     
}

