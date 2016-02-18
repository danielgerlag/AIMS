import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {PublicSelector} from '../../directives/public/publicSelector';
//import {IRemoteService} from '../../services/remoteService';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'
import {InsurableItemAttributes} from './insurableItemAttributes';

import {Modal} from 'angular2-modal/angular2-modal';
import {ModalDialogInstance} from 'angular2-modal/angular2-modal';
import {ICustomModal, ICustomModalComponent} from 'angular2-modal/angular2-modal';



@Component({
    selector: 'editInsurableItem',
})
@View({
    templateUrl: './application/components/insurableItem/editInsurableItem.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, PublicSelector, InsurableItemAttributes]
})
export class EditInsurableItem implements OnInit, ICustomModalComponent {
        
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

    protected addCoverage() {
        var item = this.dataService.createEntity("PolicyCoverage", {});
        item.Policy = this.entity.Policy;
        item.InsurableItem = this.entity;
        this.entity.PolicyCoverages.push(item);
    }

    protected addOperator() {
        var item = this.dataService.createEntity("InsurableItemOperator", {});
        item.InsurableItem = this.entity;
        this.entity.Operators.push(item);
    }

    protected removeCoverage(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }

    protected removeOperator(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }
}

