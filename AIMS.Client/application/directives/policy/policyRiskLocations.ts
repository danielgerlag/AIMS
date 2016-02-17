import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {LookupText} from '../../directives/input/lookupText';
import {FormInput} from '../../directives/input/formInput';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'


@Component({
    selector: 'policyRiskLocations',    
    inputs: ['value', 'dataService'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/directives/policy/policyRiskLocations.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, LookupText]
})
export class PolicyRiskLocations implements OnInit {

    private dataService: IDataService;
    private policy: any;
    public valueChange: EventEmitter<any> = new EventEmitter();        
    
    
    constructor() {        
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
        
    
    protected add() {
        var item = this.dataService.createEntity("PolicyRiskLocation", {});
        this.policy.RiskLocations.push(item);
    }
    
}

