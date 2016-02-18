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
    selector: 'insurableItemAttributes',    
    providers: [Modal],
    inputs: ['value', 'dataService', 'insurableItemClass'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/components/insurableItem/insurableItemAttributes.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ACCORDION_DIRECTIVES, EntityDropdown, FormInput, LookupText]
})
export class InsurableItemAttributes implements OnInit {

    private dataService: IDataService;
    private entity: breeze.Entity;
    private insurableItemClass: breeze.Entity;
    public valueChange: EventEmitter<any> = new EventEmitter();
    public ready: boolean;
    
    constructor() {
        this.ready = false;
    }

    ngOnInit() {
        this.entity.entityAspect.loadNavigationProperty("Attributes");
        this.dataService.getEntity(this, "InsurableItemClasses", this.insurableItemClass['ID'], "Groups, Groups.Attributes, Groups.Attributes.AttributeDataType", false, this.onTypeDataRecieved, this.onTypeDataFailed);        
    }

    onInit() {
        this.ngOnInit();
    }

    onTypeDataRecieved(sender: InsurableItemAttributes, data: breeze.QueryResult) {
        sender.insurableItemClass = data.results[0];
        sender.ready = true;
        
    }

    onTypeDataFailed(sender: InsurableItemAttributes, reason: any) {
        alert(reason);
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
        
    findAttribute(insurableItemClassAttributeID) {
        var insurableItem: any = this.entity;
        for (let attr of insurableItem.Attributes) {
            if (attr.InsurableItemClassAttributeID == insurableItemClassAttributeID) {
                return attr;
            }
        }
        var newAttr = this.dataService.createEntity("InsurableItemAttribute", {});
        newAttr.InsurableItemClassAttributeID = insurableItemClassAttributeID;
        insurableItem.Attributes.push(newAttr);
        return newAttr;
    }                
    
}

