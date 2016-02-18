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
    selector: 'operatorAttributes',    
    providers: [Modal],
    inputs: ['value', 'dataService', 'operatorType'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/components/operator/operatorAttributes.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ACCORDION_DIRECTIVES, EntityDropdown, FormInput, LookupText]
})
export class OperatorAttributes implements OnInit {

    private dataService: IDataService;
    private entity: breeze.Entity;
    private operatorType: breeze.Entity;
    public valueChange: EventEmitter<any> = new EventEmitter();        
    
    
    constructor() {        
    }

    ngOnInit() {
        this.entity.entityAspect.loadNavigationProperty("Attributes");
        this.dataService.getEntity(this, "OperatorTypes", this.operatorType['ID'], "Groups, Groups.Attributes, Groups.Attributes.AttributeDataType", false, this.onTypeDataRecieved, this.onTypeDataFailed);        
    }

    onInit() {
        this.ngOnInit();
    }

    onTypeDataRecieved(sender: OperatorAttributes, data: breeze.QueryResult) {
        sender.operatorType = data.results[0];
        //sender.initAttributes();
    }

    onTypeDataFailed(sender: OperatorAttributes, reason: any) {
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
        
    findAttribute(operatorTypeAttributeID) {
        var operator: any = this.entity;
        for (let attr of operator.Attributes) {
            if (attr.OperatorTypeAttributeID == operatorTypeAttributeID) {
                return attr;
            }
        }
        var newAttr = this.dataService.createEntity("OperatorAttribute", {});
        newAttr.OperatorTypeAttributeID = operatorTypeAttributeID;
        operator.Attributes.push(newAttr);
        return newAttr;
    }

    //initAttributes() {
    //    var operator: any = this.entity;
    //    var operatorType: any = this.operatorType;

    //    for (let group of operatorType.Groups) {
    //        for (let attrType of group.Attributes) {

    //        }
    //    }


    //    for (let attr of operator.Attributes) {
            
    //    }
        
    //}
        
    
}

