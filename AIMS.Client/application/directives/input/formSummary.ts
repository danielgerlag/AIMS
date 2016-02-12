import {Component, View, OnInit} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {Alert} from '../../../node_modules/ng2-bootstrap/ng2-bootstrap';
import {ModelErrorContainer} from '../../core/interfaces';
//import {TranslateService, TranslatePipe} from '../../../node_modules/ng2-translate/ng2-translate';
@Component({
    selector: 'form-summary',
    properties: ['errors', 'form']
})
@View({
    template: `    
    <div *ngIf="errors.length > 0">
        <alert *ngFor="#error of errors" type="danger" dismissible="true">
            <i class="fa fa-exclamation-triangle"></i>&nbsp; {{ error.message }}
        </alert>
    </div>
  `,
    directives: [CORE_DIRECTIVES, NgClass, Alert]
})
export class FormSummary implements OnInit {
    errors: ModelErrorContainer[];
    form: ControlGroup;
    
    //private translateService: TranslateService;
    constructor() {
        //this.translateService = translateService;
    }
    onInit() {
        this.ngOnInit();
    }
    ngOnInit() {
    }
}