import {Component, OnInit, EventEmitter, Input, View, provide, ElementRef, Injector, IterableDiffers, KeyValueDiffers, Renderer} from 'angular2/core';
import {IDataService} from '../services/dataService';
import {ODataWrapper} from '../core/interfaces'

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';

export abstract class SubViewList implements OnInit {

    protected isInit: boolean;
    protected dataService: IDataService;
    public entity: breeze.Entity;    
    public valueChange: EventEmitter<any> = new EventEmitter();


    constructor(private modal: Modal, private elementRef: ElementRef, private injector: Injector, private _renderer: Renderer) {
        this.isInit = false;
    }

    protected abstract getNewSubView(): any;
    protected abstract getEditSubView(): any;
    protected abstract createChildEntity(type): any;
    protected abstract onAdd(item);

    ngOnInit() {
        this.isInit = true;
        this.onEntityChanged();
    }

    onInit() {
        this.ngOnInit();
    }

    public get value() {
        return this.entity;
    }
    public set value(value) {
        this.entity = value;
        if (this.isInit)
            this.onEntityChanged();
    }


    protected onEntityChanged() {
        
    }


    protected add(type) {
        var item = this.createChildEntity(type);        
        let dialog: Promise<ModalDialogInstance>;
        let component = this.getNewSubView();
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

    protected loadNavigationGraph(navProperty, navExpand) {
        if (this.entity.entityAspect.entityState.isUnchangedOrModified()) {
            if (!this.entity.entityAspect.isNavigationPropertyLoaded(navProperty)) {
                var np = this.entity.entityType.getNavigationProperty(navProperty);
                this.dataService.loadNavigationGraph(this, this.entity, np, navExpand, this.navFail);
            }
        }
    }

    protected navFail(sender, data) {
        alert(data);
    }

    protected edit(item: breeze.Entity) {
        let dialog: Promise<ModalDialogInstance>;
        let component = this.getEditSubView();
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

