import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {PublicSelector} from '../../directives/public/publicSelector';
import {PolicyTypeTransitionJournalTemplates} from './policyTypeTransitionJournalTemplates';


import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {Modal} from 'angular2-modal/angular2-modal';
import {ModalDialogInstance} from 'angular2-modal/angular2-modal';
import {ICustomModal, ICustomModalComponent} from 'angular2-modal/angular2-modal';



@Component({
    selector: 'editPolicyTypeTransition',
})
@View({
    templateUrl: './application/components/policyType/editPolicyTypeTransition.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, PublicSelector, PolicyTypeTransitionJournalTemplates]
})
export class EditPolicyTypeTransition implements OnInit, ICustomModalComponent {
        
    dataService: IDataService;
    entity: any;
    dialog: ModalDialogInstance;    

    ngOnInit() {
    }

    onInit() {
        this.ngOnInit();
    }

    constructor(dialog: ModalDialogInstance, data: ICustomModal, dataService: IDataService) {
        this.dialog = dialog;
        this.entity = data;
        this.dataService = dataService;
    }


    beforeDismiss(): boolean {
        return true;
    }

    beforeClose(): boolean {
        return false;
    }

    protected save() {
        this.dialog.close(this.entity);
    }

    protected cancel() {
        this.dialog.close(null);
    }

    protected addInput() {
        var item = this.dataService.createEntity("PolicyTypeTransitionInput", {});        
        this.entity.Inputs.push(item);
    }    

    protected removeInput(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }

    
}

