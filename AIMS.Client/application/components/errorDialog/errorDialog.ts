import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';

import {Modal} from 'angular2-modal/angular2-modal';
import {ModalDialogInstance} from 'angular2-modal/angular2-modal';
import {ICustomModal, ICustomModalComponent} from 'angular2-modal/angular2-modal';


@Component({
    selector: 'errorDialog',
})
@View({
    templateUrl: './application/components/errorDialog/errorDialog.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES]
})
export class ErrorDialog implements OnInit, ICustomModalComponent {
    context: any;
    dialog: ModalDialogInstance;    

    ngOnInit() {
    }

    onInit() {
        this.ngOnInit();
    }

    constructor(dialog: ModalDialogInstance, data: ICustomModal) {
        this.dialog = dialog;
        this.context = data;
    }


    beforeDismiss(): boolean {
        return true;
    }

    beforeClose(): boolean {
        return false;
    }

    protected ok() {
        this.dialog.close(this.context);
    }

    protected cancel() {
        this.dialog.close(null);
    }
        
    
}

