import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {IRemoteService} from '../../services/remoteService';
import {ODataWrapper} from '../../core/interfaces'


@Component({
    selector: 'policyReportingEntities',    
    inputs: ['value', 'dataService'],
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
    public dataService: any;        
    
    
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
        
    }        

    //protected loadNavigationGraph(navProperty, navExpand) {
    //    if (this.value.entityAspect.entityState.isUnchangedOrModified()) {
    //        if (!this.value.entityAspect.isNavigationPropertyLoaded(navProperty)) {
    //            var np = this.value.entityType.getNavigationProperty(navProperty);
    //            this.dataService.loadNavigationGraph(this, this.entity, np, navExpand, this.navFail);
    //        }
    //    }
    //}
        
    isReady() {
        return true;
    }
    

    
}

