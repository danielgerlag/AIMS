/// <reference path="../../node_modules/reflect-metadata/reflect-metadata.d.ts" />

import {FormBuilder, Validators, ControlGroup, Control, NgClass} from 'angular2/common';
import {IValidatable} from '../core/interfaces';
import {CustomValidators, Validate, ServerValidate, ChildForm, AddToForm} from '../core/validation';
//import {Reflect} from 'reflect-metadata'



abstract class DecoratedModel implements IValidatable {
    buildForm(fb: FormBuilder, serverState: any, prefix: string): ControlGroup {
        var self = this;
        var result = fb.group({});
        for (var p in self) {
            var add = Reflect.getMetadata("custom:addToForm", self, p);
            if (add) {
                var validators: Function[] = [];
                var sv = Reflect.getMetadata("custom:serverValidate", self, p);
                if (sv) 
                    validators.push(CustomValidators.serverValidate(serverState, prefix + p));
                var cv = Reflect.getMetadata("custom:validator", self, p);
                if (cv)
                    validators = validators.concat(cv);
                result.addControl(p, new Control(self[p], Validators.compose(validators)));
            }
            var child = Reflect.getMetadata("custom:childForm", self, p);
            if (child) {
                var childForm = self[p].buildForm(fb, serverState, prefix + p + ".");
                result.addControl(p, childForm);
            }
        }
        return result;
    }
}

export namespace Account {

    export class LoginRequest extends DecoratedModel {

        @ServerValidate
        @Validate(Validators.required)
        Username: string;

        @Validate(Validators.required)
        Password: string;                
    }

    export class LoginResponse {
        Name: string;
        IsAdmin: boolean;
    }
        
}

export namespace DocumentArchiverModels {

    export class DocumentArchiveRequest extends DecoratedModel {
    }
}

export namespace DocumentBuilderModels {


    export class DocumentFragment extends DecoratedModel {     
        ContentFragmentID: number;
        Order: number;
        Custom: boolean;
        CustomContent: string;
    }

    export class AddressData extends DecoratedModel {

        @ServerValidate
        public Name: string;

        @ServerValidate
        public AddressLine1: string;

        @ServerValidate
        public AddressLine2: string;

        @ServerValidate
        public City: string;

        @ServerValidate
        public Province: string;

        @ServerValidate
        public Country: string;

        @ServerValidate
        public PostalCode: string;
    }

    export class DocumentBuildRequest extends DecoratedModel {

        @ServerValidate
        public DocumentDefinitionID: number;

        @AddToForm
        public MergeData: string;

        //@ChildForm
        public Address: AddressData = new AddressData();

        public HeaderFragments: DocumentFragment[] = [];
        public FooterFragments: DocumentFragment[] = [];
        public BodyFragments: DocumentFragment[] = [];

    }
}

export namespace EntityExts {

    export class TransactionTrigger {

        _statusLoading: boolean = false;

        init() {
            var self: any = this;
            var entity: breeze.Entity = self;
            entity.entityAspect.propertyChanged.subscribe(
                function (propertyChangedArgs) {
                    if (propertyChangedArgs.propertyName == "TransactionTriggerStatusID") {
                        self._statusLoading = true;
                        entity.entityAspect.loadNavigationProperty("Status");
                    }
                });
        }

        statusText() {
            var self: any = this;
            var entity: breeze.Entity = self;

            if (self["Status"]) {
                self._statusLoading = false;
                return self["Status"].Name;
            }
            else {
                if (!self._statusLoading) {
                    self._statusLoading = true;
                    entity.entityAspect.loadNavigationProperty("Status");
                }
            }

        }
    }
}