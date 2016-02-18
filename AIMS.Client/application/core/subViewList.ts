import {Component, OnInit, EventEmitter, Input, View, provide, ElementRef, Injector, IterableDiffers, KeyValueDiffers, Renderer} from 'angular2/core';
import {IDataService} from '../services/dataService';
import {ODataWrapper} from '../core/interfaces'

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';

export abstract class SubViewList implements OnInit {

    protected dataService: IDataService;
    public entity: breeze.Entity;    
    public valueChange: EventEmitter<any> = new EventEmitter();


    constructor(private modal: Modal, private elementRef: ElementRef,
        private injector: Injector, private _renderer: Renderer) {
    }

    protected abstract getSubView(): any;
    protected abstract createChildEntity(type): any;
    protected abstract onAdd(item);

    ngOnInit() {
    }

    onInit() {
        this.ngOnInit();
    }

    public get value() {
        return this.entity;
    }
    public set value(value) {
        this.entity = value;
        this.onEntityChanged();
    }


    onEntityChanged() {
        this.valueChange.next(this.entity);
    }


    protected add(type) {
        var item = this.createChildEntity(type);        
        let dialog: Promise<ModalDialogInstance>;
        let component = this.getSubView();
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
                    self.onAdd(result);
                else
                    item.entityAspect.setDetached();
            }, () => {
                item.entityAspect.setDetached();
            });
        });

    }

    protected edit(item: breeze.Entity) {
        let dialog: Promise<ModalDialogInstance>;
        let component = this.getSubView();
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

