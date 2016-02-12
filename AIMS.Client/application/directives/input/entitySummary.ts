import {Component, View, OnInit} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {Alert} from '../../../node_modules/ng2-bootstrap/ng2-bootstrap';
import {ModelErrorContainer} from '../../core/interfaces';
//import {TranslateService, TranslatePipe} from '../../../node_modules/ng2-translate/ng2-translate';


@Component({
    selector: 'entity-summary',
    properties: ['entity', 'canShow']
})
@View({
    template: `    
    <div *ngIf="canShow">
        <alert *ngFor="#error of entity.entityAspect.getValidationErrors()" type="danger" dismissible="true">
            <i class="fa fa-exclamation-triangle"></i>&nbsp; {{ error.errorMessage }}
        </alert>
    </div>
  `,
    directives: [CORE_DIRECTIVES, NgClass, Alert]
})
export class EntitySummary implements OnInit {

    canShow: boolean;
    entity: breeze.Entity;
    
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

