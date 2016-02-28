import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {NumericInput} from '../../directives/input/numericInput';
import {LookupText} from '../../directives/input/lookupText';
import {FormInput} from '../../directives/input/formInput';
import {IRemoteService} from '../../services/remoteService';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'


@Component({
    selector: 'policyAgents',    
    inputs: ['value', 'dataService'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/directives/policy/policyAgents.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, LookupText, NumericInput]
})
export class PolicyAgents implements OnInit {

    private dataService: IDataService;
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
        //this.valueChange.next(this.policy);
    }        
        
    protected add() {
        var item = this.dataService.createEntity("PolicyAgent", {});
        this.policy.Agents.push(item);
    }

    protected remove(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }
    
}

