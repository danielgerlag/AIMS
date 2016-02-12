import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {IRemoteService} from '../../services/remoteService';
import {ODataWrapper} from '../../core/interfaces'


@Component({
    selector: 'entity-dropdown',    
    inputs: ['value', 'name', 'query', 'keyField', 'displayField'],
    outputs: ['valueChange']
})
@View({
    template: `    
    <select type="text" class="form-control"  [ngModel]="value" (change)="changeValue($event.target.value)" >        
        <option *ngFor="#o of selectList" [value]="o.key">{{o.name}}</option>
    </select>
  `,
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass]
})
export class EntityDropdown implements OnInit {

    private remoteService: IRemoteService;

    private entityId: number;
    private name: string;
    private query: string;
    private keyField: string;
    private displayField: string;
    public valueChange: EventEmitter<any> = new EventEmitter();

    private selectList: Array<any> = [];
    
    constructor(remoteService: IRemoteService) {     
        this.remoteService = remoteService;   
    }

    ngOnInit() {
        this.remoteService.get(this, 'Data.svc/' + this.query, this.onLoadResponse);
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

    public changeValue(value) {
        this.value = value;
    }

    onEntityChanged() {
        //console.log('firing valueChange: ' + this.entityId);
        this.valueChange.next(this.entityId);
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
                //firefox select bug workaround
                if (sender.selectList.length > 0)
                    sender.value = sender.selectList[0].key;
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

