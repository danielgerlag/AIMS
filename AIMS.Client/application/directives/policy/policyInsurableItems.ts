import {Component, OnInit, EventEmitter, View, provide, ElementRef, Injector, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {LookupText} from '../../directives/input/lookupText';
import {FormInput} from '../../directives/input/formInput';
import {EditInsurableItem} from '../../components/insurableItem/editInsurableItem';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {SubViewList} from '../../core/subViewList'

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';

@Component({
    selector: 'policyInsurableItems',    
    providers: [Modal],
    inputs: ['value', 'dataService', 'policySubType'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/directives/policy/policyInsurableItems.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, LookupText]
})
export class PolicyInsurableItems extends SubViewList {
        
    private policySubType: breeze.Entity;    
    
    constructor(modal: Modal, elementRef: ElementRef, injector: Injector, _renderer: Renderer) {
        super(modal, elementRef, injector, _renderer);
    }    
       
    protected getSubView(): any {
        return EditInsurableItem;
    }

    protected createChildEntity(type): any {
        var item = this.dataService.createEntity("InsurableItem", {});
        item.InsurableItemClass = type;
        item.Policy = this.entity;
        return item;
    }

    protected onAdd(item) {
        this.entity['InsurableItems'].push(item);
    }

    //flattenItems() {
    //    var result = [];
    //    for (let riskLocation of this.entity['RiskLocations']) {
    //        for (let item of riskLocation.InsurableItems) {
    //            result.push(item);
    //        }            
    //    }
    //    return result;
    //}
    
    
}

