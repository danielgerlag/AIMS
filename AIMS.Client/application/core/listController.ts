import {Component, View, provide} from 'angular2/core';
import {FormBuilder, Validators, ControlGroup, Control, NgClass} from 'angular2/common';
import {IShellService} from '../services/shellService';
import {IRemoteService} from '../services/remoteService';
import {IValidatable} from './interfaces';
import {ModelErrorContainer} from './interfaces';
import {BaseController} from './baseController'


export abstract class ListController<T> extends BaseController {
        
    protected remoteService: IRemoteService;
    
    public data: any;

    constructor(shellService: IShellService, remoteService: IRemoteService) {
        super(shellService);
        this.remoteService = remoteService;
        this.data = [];

        this.loadData();
    }
        
    protected abstract getPath(): string;

    protected expandFields(): string[] {
        return [];
    }

    protected loadData() {
        var oDataPath = this.getPath();
        var expandList = this.expandFields();
        var expandQuery = "";

        if (expandList.length > 0) {
            expandQuery = "?$expand=";
            var first = true;
            expandList.forEach(function (navPath) {
                if (!first)
                    expandQuery = expandQuery + ",";
                expandQuery = expandQuery + navPath;
                first = false;
            });
        }
        this.remoteService.get(this, oDataPath + expandQuery, this.onLoadResponse);
    }

    protected onLoadResponse(sender: ListController<T>, data: any, status: number): any {
                
        if (status = 200) {
            sender.data = data.value;
        }
    }

}