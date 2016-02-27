import {Component, OnInit, EventEmitter, View, provide, ElementRef, Injector, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {TAB_DIRECTIVES, ACCORDION_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
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
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, ACCORDION_DIRECTIVES, EntityDropdown, FormInput, LookupText]
})
export class PolicyInsurableItems extends SubViewList {
        
    private policySubType: breeze.Entity;    
    
    constructor(modal: Modal, elementRef: ElementRef, injector: Injector, _renderer: Renderer) {
        super(modal, elementRef, injector, _renderer);
    }    
       
    protected getNewSubView(): any {
        return EditInsurableItem;
    }

    protected getEditSubView(): any {
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

    public displayColumnsByClass(insurableItemClass) {
        //insurableItemClass.entityAspect.loadNavigationProperty("Attributes", () => { });
        let result = [];
        for (let group of insurableItemClass.Groups) {            
            let attrs = _.where(group.Attributes, { ShowInList: true });
            for (let attr of attrs) {
                result.push(attr);
            }
        }
        return result;
    }

    public itemsByClass(insurableItemClass) {
        return _.where(this.entity['InsurableItems'], { InsurableItemClass: insurableItemClass });
    }
    
    public getAttributeValue(item, attr) {
        let value: any = _.findWhere(item.Attributes, { InsurableItemClassAttributeID: attr.ID });
        if (value)
            return value.Value;
        return null;
    }
        

    protected onEntityChanged() {
        super.onEntityChanged();
        this.loadNavigationGraph("InsurableItems", "Attributes, Operators, Operators.Operator, Operators.Operator.OperatorPublic, PolicyCoverages, PolicyRiskLocation");
    }

    
}

