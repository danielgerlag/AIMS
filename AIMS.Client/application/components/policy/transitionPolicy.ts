import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {FormInput} from '../../directives/input/formInput';
import {DateInput} from '../../directives/input/dateInput';

import {PublicSelector} from '../../directives/public/publicSelector';
import {IRemoteService} from '../../services/remoteService';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {OperationModels} from '../../core/models'


import {Modal} from 'angular2-modal/angular2-modal';
import {ModalDialogInstance} from 'angular2-modal/angular2-modal';
import {ICustomModal, ICustomModalComponent} from 'angular2-modal/angular2-modal';


@Component({
    selector: 'transitionPolicy',
})
@View({
    templateUrl: './application/components/policy/transitionPolicy.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, DateInput, PublicSelector]
})
export class TransitionPolicyWizard implements OnInit, ICustomModalComponent {
        
    dataService: IDataService;
    remoteService: IRemoteService;
    policyID: number;
    transitionID: number;
    transitionConfig: any;

    request: OperationModels.PolicyTransitionRequest;

    dialog: ModalDialogInstance;    
    configLoaded: boolean;
    
    protected step: number;

    
    ngOnInit() {

    }

    onInit() {
        this.ngOnInit();
    }

    constructor(dialog: ModalDialogInstance, data: ICustomModal, dataService: IDataService, remoteService: IRemoteService) {
        this.dialog = dialog;
        this.remoteService = remoteService;
        this.policyID = data['policyID'];
        this.transitionID = data['transitionID'];
        this.dataService = dataService;
        this.configLoaded = false;
        this.loadConfig();
        this.step = 1;
    }


    beforeDismiss(): boolean {
        return true;
    }

    beforeClose(): boolean {
        return false;
    }

    protected finish() {

        //this.shellService.showLoader("Transitioning...");        

        this.remoteService.post(this, "Data.svc/Policies(" + this.policyID + ")/Transition", this.request, (sender: TransitionPolicyWizard, data: any, status) => {
            //this.shellService.hideLoader();
            if (data.Success) {
                //sender.shellService.toastSuccess("Transition", "Transition Successful");
                this.dialog.close(true);
            }
            else {
                alert(data.Message);
                //sender.shellService.toastError("Transition", data.value.Message);
            }
        });
                
    }

    protected cancel() {
        this.dialog.close(null);
    }

    protected nextStep() {
        this.step++;
    }

    protected prevStep() {
        this.step--;
    }
    

    protected loadConfig() {
        this.dataService.getEntity(this, "PolicyTypeTransitions", this.transitionID, "Inputs, Inputs.AttributeDataType", false, this.onLoadConfig, this.onConfigFailure);
    }

    protected onLoadConfig(sender: TransitionPolicyWizard, data: breeze.QueryResult): any {
        sender.transitionConfig = data.results[0];
        sender.request = new OperationModels.PolicyTransitionRequest();
        sender.request.PolicyTypeTransitionID = sender.transitionID;

        for (let srcInput of sender.transitionConfig.Inputs) {
            let destInput = new OperationModels.PolicyTransitionRequestInput();
            destInput.PolicyTypeTransitionInputID = srcInput.ID;
            sender.request.Inputs.push(destInput);
        }

        sender.configLoaded = true;
    }

    protected onConfigFailure(sender: TransitionPolicyWizard, data: any): any {
        alert(data);
    }

    findInput(policyTypeTransitionInputID) {

        for (let inp of this.request.Inputs) {
            if (inp.PolicyTypeTransitionInputID == policyTypeTransitionInputID) {
                return inp;
            }
        }
        var newInp = new OperationModels.PolicyTransitionRequestInput();
        newInp.PolicyTypeTransitionInputID = policyTypeTransitionInputID;
        this.request.Inputs.push(newInp);
        return newInp;
    }    
}

