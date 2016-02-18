﻿import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {IRemoteService} from '../../services/remoteService';
import {ODataWrapper} from '../../core/interfaces'


@Component({
    selector: 'entity-dropdown',    
    inputs: ['value', 'name', 'query', 'boundList', 'keyField', 'displayField', 'nullable'],
    outputs: ['valueChange']
})
@View({
    template: `    
    <select type="text" class="form-control"  [ngModel]="value" (change)="changeValue($event.target.value)" >
        <option *ngIf="nullable" value="">(None)</option>
        <option *ngFor="#o of selectList" [value]="o.key">{{o.name}}</option>
    </select>
  `,
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass]
})
export class EntityDropdown implements OnInit {

    private remoteService: IRemoteService;
    private nullValue: any;
    private entityId: number;
    private name: string;
    private query: string;
    private keyField: string;
    private displayField: string;
    private nullable: boolean;
    public valueChange: EventEmitter<any> = new EventEmitter();
        
    private _boundList: Array<any> = [];
    private selectList: Array<any> = [];
    
    constructor(remoteService: IRemoteService) {     
        this.remoteService = remoteService;   
    }

    ngOnInit() {
        if (this.query) {
            this.remoteService.get(this, 'Data.svc/' + this.query, this.onLoadResponse);
        } else {
            this.loadBoundList();
        }
    }

    onInit() {
        this.ngOnInit();
    }

    public get value() {
        return this.entityId;
    }
    public set value(value) {
        //console.log('firing setValue: ' + value);
        this.entityId = value;
        this.onEntityChanged();
    }

    public get boundList() {
        return this._boundList;
    }
    public set boundList(value) {                
        this._boundList = value;
        this.loadBoundList();
    }

    public changeValue(value) {
        this.value = value;
    }

    onEntityChanged() {
        //console.log('firing valueChange: ' + this.entityId);
        this.valueChange.next(this.entityId);
    }        

    protected loadBoundList() {
        var self = this;
        if ((!self.query) && (self._boundList) && (self.keyField) && (self.displayField)) {
            self.selectList = self._boundList.map(function (x) {
                return {
                    key: self.drillObject(x, self.keyField), name: self.drillObject(x, self.displayField)
                    //key: x[self.keyField], name: x[self.displayField]
                }
            });
        }
    }

    protected onLoadResponse(sender: EntityDropdown, data: ODataWrapper<Array<any>>, status: number): any {
        //sender.shellService.hideLoader();

        if (status == 200) {
            sender.selectList = data.value.map(function (x) {                
                return {
                    key: sender.drillObject(x, sender.keyField), name: sender.drillObject(x, sender.displayField)
                }
            });
            if (!sender.value) {
                if (!sender.nullable) {
                    //firefox select bug workaround
                    if (sender.selectList.length > 0)
                        sender.value = sender.selectList[0].key;
                }
            }
        }
    }

    private drillObject(data: any, path: string): any {
        var delimeter = path.indexOf(".");
        if (delimeter == -1) {
            return data[path];
        }
        else {
            var newPath = path.slice(delimeter + 1);
            var newData = data[path.slice(0, delimeter)];
            return this.drillObject(newData, newPath);
        }
    }
}

