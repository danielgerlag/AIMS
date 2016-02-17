import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {IRemoteService} from '../../services/remoteService';
import {ODataWrapper} from '../../core/interfaces'


@Component({
    selector: 'policyReportingEntities',    
    inputs: ['value'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/directives/policy/policyReportingEntities.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput]
})
export class PolicyReportingEntities implements OnInit {

    private remoteService: IRemoteService;
    private policy: any;
    public valueChange: EventEmitter<any> = new EventEmitter();        
    
    
    constructor(remoteService: IRemoteService) {     
        this.remoteService = remoteService;   
    }

    ngOnInit() {        
    }

    onInit() {
        this.ngOnInit();
    }

    public get value() {
        return this.policy;
    }
    public set value(value) {        
        this.policy = value;
        this.onEntityChanged();
    }       
    

    onEntityChanged() {        
        this.valueChange.next(this.policy);
    }        
        
    

    
}

