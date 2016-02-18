import {Component, View, OnInit} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

//import {TranslateService, TranslatePipe} from '../../../node_modules/ng2-translate/ng2-translate';


@Component({
    selector: 'form-input',
    properties: ['title', 'name', 'entity', 'labelClass']
})
@View({
    template: `    
    <div class="form-group" [class.has-danger]="hasError()">
        <label class="control-label {{labelClass}}" >{{ title }}</label>
        <ng-content></ng-content>
    </div>
  `,    
    directives: [CORE_DIRECTIVES, NgClass]
})
export class FormInput implements OnInit {

    private title: string;
    private name: string;
    private labelClass: string;
    private entity: breeze.Entity;

    //private translateService: TranslateService;

    constructor() {
        //this.translateService = translateService;
    }

    ngOnInit() {
        //this.type = this.type !== 'undefined' ? this.type : 'tabs';
    }

    onInit() {
        this.ngOnInit();
    }

    hasError() {
        if (this.entity) {
            if (this.entity.entityAspect) {
                var errors = this.entity.entityAspect.getValidationErrors(this.name);
                return (errors.length > 0);
            }
        }
        return false;        
        //return !this.form.find(this.name).valid;// && this.form.find(this.name).touched;
    }

}

