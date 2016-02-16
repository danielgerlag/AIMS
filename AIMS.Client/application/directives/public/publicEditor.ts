import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {IRemoteService} from '../../services/remoteService';
import {ODataWrapper} from '../../core/interfaces'


@Component({
    selector: 'public-editor',    
    inputs: ['value'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/directives/public/publicEditor.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput]
})
export class PublicEditor implements OnInit {

    private remoteService: IRemoteService;
    private entity: any;
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
        
    

    
}

