import {Component, OnInit, EventEmitter, Input, View, provide, ElementRef, Injector, IterableDiffers, KeyValueDiffers, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {TAB_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {LookupText} from '../../directives/input/lookupText';
import {FormInput} from '../../directives/input/formInput';
import {EditOperator} from '../../components/operator/editOperator';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';

@Component({
    selector: 'policyOperators',    
    providers: [Modal],
    inputs: ['value', 'dataService', 'policySubType'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/directives/policy/policyOperators.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, TAB_DIRECTIVES, EntityDropdown, FormInput, LookupText]
})
export class PolicyOperators implements OnInit {

    private dataService: IDataService;
    private policy: breeze.Entity;
    private policySubType: breeze.Entity;
    public valueChange: EventEmitter<any> = new EventEmitter();        
    
    
    constructor(private modal: Modal, private elementRef: ElementRef,
        private injector: Injector, private _renderer: Renderer) {        
    }

    ngOnInit() {
        //this.dataService.cacheEntities(this, "OperatorTypes", "", (sender, data) => { }, (sender, data) => { });
        //this.policySubType.entityAspect.loadNavigationProperty("PolicySubType.PolicyType.ItemClasses");
        //for (let itemClass of this.policySubType['PolicyType'].ItemClasses) {
        //    itemClass.entityAspect.loadNavigationProperty("InsurableItemClass.OperatorTypes");
        //}
        
    }

    onInit() {
        this.ngOnInit();
    }

    public get value() {
        return this.policy;
    }
    public set value(value) {        
        this.policy = value;
        this.onEntityChanged();
    }       
    

    onEntityChanged() {        
        this.valueChange.next(this.policy);
    }        
        
    
    protected add(opType) {
        var item = this.dataService.createEntity("Operator", {});
        item.OperatorType = opType;
        let dialog: Promise<ModalDialogInstance>;
        let component = EditOperator;
        var self = this;
        

        let bindings = Injector.resolve([
            provide(IDataService, { useValue: this.dataService }),
            provide(ICustomModal, { useValue: item }),
            provide(IterableDiffers, { useValue: this.injector.get(IterableDiffers) }),
            provide(KeyValueDiffers, { useValue: this.injector.get(KeyValueDiffers) }),
            provide(Renderer, { useValue: this._renderer })
        ]);

        dialog = this.modal.open(
            <any>component,
            bindings,
            new ModalConfig("lg", false, 27));


        dialog.then((resultPromise) => {
            return resultPromise.result.then((result) => {
                if (result)
                    this.policy['Operators'].push(result);
                else
                    item.entityAspect.setDetached();
            }, () => {
                item.entityAspect.setDetached();
            });
        });
        
    }

    protected edit(item: breeze.Entity) {        
        let dialog: Promise<ModalDialogInstance>;
        let component = EditOperator;
        var self = this;


        let bindings = Injector.resolve([
            provide(IDataService, { useValue: this.dataService }),
            provide(ICustomModal, { useValue: item }),
            provide(IterableDiffers, { useValue: this.injector.get(IterableDiffers) }),
            provide(KeyValueDiffers, { useValue: this.injector.get(KeyValueDiffers) }),
            provide(Renderer, { useValue: this._renderer })
        ]);

        dialog = this.modal.open(
            <any>component,
            bindings,
            new ModalConfig("lg", false, 27));


        dialog.then((resultPromise) => {
            return resultPromise.result.then((result) => {
                // ?
            }, () => {
                //
            });
        });
    }

    protected remove(item: breeze.Entity) {
        item.entityAspect.setDeleted();
    }
    
}

