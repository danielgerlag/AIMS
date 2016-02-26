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

export namespace OperationModels {

    export class PolicyTransitionRequest {
        PolicyTypeTransitionID: number;
        Inputs: PolicyTransitionRequestInput[] = [];
    }

    export class PolicyTransitionRequestInput {
        PolicyTypeTransitionInputID: number;
        Value: string;
    }
}

export namespace EntityExts {

    export class TransactionTrigger {

        _statusLoading: boolean = false;

        init() {
            var self: any = this;
            var entity: breeze.Entity = self;

            //entity.entityType.defaultResourceName

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


    export class ContextParameterValue {

        _contextParameterLoading: boolean = false;

        init() {
            var self: any = this;
            var entity: breeze.Entity = self;
            entity.entityAspect.propertyChanged.subscribe(
                function (propertyChangedArgs) {
                    if (propertyChangedArgs.propertyName == "ContextParameterID") {
                        self._contextParameterLoading = true;
                        entity.entityAspect.loadNavigationProperty("ContextParameter");
                    }
                });
        }

        contextParameterName() {
            var self: any = this;
            var entity: breeze.Entity = self;

            if (self["ContextParameter"]) {
                self._contextParameterLoading = false;
                return self["ContextParameter"].Name;
            }
            else {
                if (!self._contextParameterLoading) {
                    self._contextParameterLoading = true;
                    entity.entityAspect.loadNavigationProperty("ContextParameter");
                }
            }

        }
    }

    export class Public {
                
        init() {
            
        }

        fullName() {
            var self: any = this;
            if (self.PartyType == 'I')
                return self.Name + ", " + self.FirstName;

            return self.Name;

        }
    }
}