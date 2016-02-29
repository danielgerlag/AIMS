import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {FormBuilder, Validators, ControlGroup, Control, NgClass, FORM_BINDINGS, CORE_DIRECTIVES, FORM_DIRECTIVES, JsonPipe} from 'angular2/common';
import {Component, Input, View, provide, ElementRef, Injector, IterableDiffers, KeyValueDiffers, Renderer} from 'angular2/core';

//import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
//import {EntityDropdown} from '../../directives/input/entityDropdown';
//import {FormInput} from '../../directives/input/formInput';
//import {EntitySummary} from '../../directives/input/entitySummary';
import {PublicWizard} from './publicWizard';
//import {IShellService} from '../../services/shellService';
//import {IAuthService} from '../../services/authService';
import {IDataService} from '../../services/dataService';


import {Modal} from 'angular2-modal/angular2-modal';
import {ModalDialogInstance} from 'angular2-modal/angular2-modal';
import {ICustomModal, ICustomModalComponent} from 'angular2-modal/angular2-modal';



@Component({    
    selector: 'modal-content',
    template: `

<div class="modal-content">
    <div class="modal-header">
    <button type="button" class="close" (click)="cancel()">&times;</button>
    <h4 class="modal-title">New Public</h4>
    </div>
    <div class="modal-body">
        <publicWizard [(value)]="context" [dataService]="dataService" (onFinish)="save()" (onCancel)="cancel()"></publicWizard>
    </div>
</div>
`,
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, PublicWizard],
    pipes: [JsonPipe]
})
export class NewPublicWizard implements ICustomModalComponent {

    dialog: ModalDialogInstance;
    context: breeze.Entity;
    dataService: IDataService;

    constructor(dialog: ModalDialogInstance, data: ICustomModal, dataService: IDataService) {
        this.dialog = dialog;
        this.context = <breeze.Entity>data;
        this.dataService = dataService;
    }
        

    beforeDismiss(): boolean {
        return true;
    }

    beforeClose(): boolean {
        return false;
    }

    protected save() {
        this.dialog.close(this.context);
    }

    protected cancel() {
        this.dialog.close(null);
    }

   
}