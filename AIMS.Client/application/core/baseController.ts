import {Component, View, provide} from 'angular2/core';
import {FormBuilder, Validators, ControlGroup, Control, NgClass} from 'angular2/common';
import {IShellService} from '../services/shellService';
import {IRemoteService} from '../services/remoteService';
import {IValidatable} from './interfaces';
import {ModelErrorContainer} from './interfaces';


export abstract class BaseController {
    
    protected shellService: IShellService;

    constructor(shellService: IShellService) {
        this.shellService = shellService;          
    }

}

export abstract class FormController<T extends IValidatable> extends BaseController {

    protected model: T;
    protected serverState: any;
    protected form: ControlGroup;
    protected errorContainer: ModelErrorContainer[];    

    constructor(shellService: IShellService, fb: FormBuilder) {
        super(shellService);        
        this.errorContainer = [];
        this.serverState = { modelState: {} };
        this.model = this.createModel();
        this.form = this.model.buildForm(fb, this.serverState, "");

    }

    protected abstract createModel(): T;   
    //protected abstract getPath(): string;   

    protected prepareData(): any {
        return this.model;
    }

    protected beforeSubmit() {
    }

    protected abstract doSubmit(data: any)

    public submit() {
        this.beforeSubmit();
        var data = this.prepareData();
        this.doSubmit(data);
        //this.remoteService.post(this, this.getPath(), data, this.onResponse);
    } 

    protected onResponse(sender: FormController<T>, data: any, status: number): any {
                
        sender.errorContainer = [];
        sender.serverState.modelState = {};        

        if (status == 400) {
            for (var p in data) {
                for (var i in data[p]) {
                    sender.errorContainer.push({ errorKey: "server", fieldName: p, message: data[p][i] });
                }
            }
            sender.serverState.modelState = data;
            for (var x in sender.form.controls) {
                sender.form.controls[x].updateValueAndValidity();                
            }
        }
    }

}


