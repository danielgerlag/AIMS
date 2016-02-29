import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {IRemoteService} from '../../services/remoteService';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'
import {PublicGeneral} from '../../directives/public/publicGeneral';
import {PublicContactDetails} from '../../directives/public/publicContactDetails';
import {PublicBankAccounts} from '../../directives/public/publicBankAccounts';

@Component({
    selector: 'publicWizard',
    inputs: ['value', 'dataService'],
    outputs: ['valueChange', 'onFinish', 'onCancel']
})
@View({
    templateUrl: './application/directives/public/publicWizard.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, PublicGeneral, PublicContactDetails, PublicBankAccounts]
})
export class PublicWizard implements OnInit {

    private remoteService: IRemoteService;
    private dataService: IDataService;
    private entity: any;
    public valueChange: EventEmitter<any> = new EventEmitter();
    public onFinish: EventEmitter<any> = new EventEmitter();
    public onCancel: EventEmitter<any> = new EventEmitter();

    protected step: number;

    constructor(remoteService: IRemoteService) {
        this.remoteService = remoteService;
        this.step = 1;
    }

    ngOnInit() {
    }

    onInit() {
        this.ngOnInit();
    }

    public get value() {
        return this.entity;
    }
    public set value(value) {
        this.entity = value;
        this.onEntityChanged();
    }

    public changeValue(value) {
        this.value = value;
    }

    onEntityChanged() {
        this.valueChange.next(this.entity);
    }

    protected nextStep() {
        this.step++;
    }

    protected prevStep() {
        this.step--;
    }

    protected finish() {
        this.onFinish.next(this.entity);
    }

    protected cancel() {
        this.onCancel.next(null);
    }

}

