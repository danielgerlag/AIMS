import {FormBuilder, Validators, ControlGroup, Control, NgClass, FORM_BINDINGS, CORE_DIRECTIVES, FORM_DIRECTIVES, JsonPipe} from 'angular2/common';
import {Router} from 'angular2/router';

import {Component, View} from 'angular2/core';

import {IShellService} from '../../services/shellService';
import {IAuthService} from '../../services/authService';
import {IDataService} from '../../services/dataService';
import {ListController} from '../../core/listController';

@Component({
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass],
    templateUrl: './application/core/listController.html'
})
export class ListReportingEntity extends ListController {

    constructor(shellService: IShellService, dataService: IDataService, router: Router) {
        super(shellService, dataService, router);        
    }
    
    protected setName(): string {
        return "ReportingEntities";
    }

    protected itemRoute(): string {
        return "EditReportingEntity";
    }

    protected expandFields(): string[] {
        var result = super.expandFields();
        result.push("Public");
        return result;
    }

    protected getColumns(): any {
        return [
            { Title: 'Name', Name: 'Public.Name' }
        ];
    }
    
}