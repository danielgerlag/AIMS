import {Component, OnInit, EventEmitter, View, provide, ElementRef, Injector, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {LookupText} from '../../directives/input/lookupText';
import {FormInput} from '../../directives/input/formInput';
import {NumericInput} from '../../directives/input/numericInput';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {EditContextParameterValue} from './editcontextParameterValue'

import {SubViewList} from '../../core/subViewList'

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';

@Component({
    selector: 'contextParameterValues',
    providers: [Modal],
    inputs: ['value', 'dataService', 'childType'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/components/contextParameter/contextParameterValues.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, LookupText, NumericInput]
})
export class ContextParameterValues extends SubViewList {

    childType: string;

    constructor(modal: Modal, elementRef: ElementRef, injector: Injector, _renderer: Renderer) {
        super(modal, elementRef, injector, _renderer);
    }


    protected getNewSubView(): any {
        return EditContextParameterValue;
    }

    protected getEditSubView(): any {
        return EditContextParameterValue;
    }

    protected createChildEntity(type): any {
        var item = this.dataService.createEntity(this.childType, {});
        item.ContextParameterValue = this.dataService.createEntity("ContextParameterValue", {});
        item.ContextParameterValue.EffectiveDate = moment().startOf('day').toDate();

        return item;
    }

    protected onAdd(item) {
        this.entity['ContextParameterValues'].push(item);
        //this.onEntityChanged();
    }

    protected onEntityChanged() {
        super.onEntityChanged();
        this.loadNavigationGraph("ContextParameterValues", "ContextParameterValue");
    }


}

