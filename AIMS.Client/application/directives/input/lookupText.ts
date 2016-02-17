import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass} from 'angular2/common';
import {IRemoteService} from '../../services/remoteService';
import {ODataWrapper, ODataCollectionWrapper} from '../../core/interfaces';

@Component({
    selector: 'lookupText',
    inputs: ['key', 'set']    
})
@View({
    template: '{{ lookupText }}',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass]
})
export class LookupText implements OnInit {
        
    set: string;
    id: number;
    lookupText: string;   

    remoteService: IRemoteService;          

    constructor(remoteService: IRemoteService) {
        this.remoteService = remoteService;
    }

    public get key() {
        return this.id;
    }
    public set key(value) {
        this.id = value;
        this.doLookup();
    }

    doLookup() {
        if ((this.id > 0) && (this.set)) {
            this.remoteService.get(this, "Data.svc/GetLookupText?set='" + this.set + "'&id=" + this.id, (sender: LookupText, data: ODataWrapper<string>, status: number) => {
                sender.lookupText = data.value;
            });
        }
        else {
            this.lookupText = "";
        }
    }
    
    onInit() {
        this.ngOnInit();
    }

    ngOnInit() {
        this.doLookup();
    }
        
}
