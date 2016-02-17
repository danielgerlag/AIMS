import {Component, OnInit, EventEmitter, Input, View, provide, ElementRef, Injector, IterableDiffers, KeyValueDiffers, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {Typeahead} from '../../../node_modules/ng2-bootstrap/ng2-bootstrap';
import {IRemoteService} from '../../services/remoteService';
import {ISearchService} from '../../services/searchService';
import {IDataService} from '../../services/dataService';
import {ODataWrapper, ODataCollectionWrapper} from '../../core/interfaces';
import {dto} from '../../core/dto';

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';
import {NewPublicWizard} from './newPublicWizard';



@Component({
    selector: 'publicSelector',
    providers: [Modal],
    inputs: ['value', 'dataService'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/directives/public/publicSelector.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, Typeahead]
})
export class PublicSelector implements OnInit {

    dataService: IDataService;
    entity: breeze.Entity;    
    searchQuery: string;
    searchResult: any[];
    lookupText: string;

    valueChange: EventEmitter<any> = new EventEmitter();

    public columns: any;
    public path: string;

    remoteService: IRemoteService;
    searchService: ISearchService;

    get value() {
        return this.entity;
    }
    set value(value) {
        this.entity = value;
        this.onEntityChanged();
    }        

    constructor(
        remoteService: IRemoteService,
        searchService: ISearchService,
        private modal: Modal, private elementRef: ElementRef,
        private injector: Injector, private _renderer: Renderer) {

        this.remoteService = remoteService;
        this.searchService = searchService;
        this.path = "Publics";
        this.columns = [{ Title: 'Name', Name: 'Name' }, { Title: 'First Name', Name: 'FirstName' }];
    }
        

    onEntityChanged() {
        if (this.entity) {

            this.lookupText = this.entity['Name'];
            //
        }
        else {
            this.lookupText = "";
        }
        this.valueChange.next(this.entity);
    }

    search() {
        var self = this;
        self.remoteService.get(self, 'Data.svc/Search' + self.path + "?query='" + self.searchQuery +"'", self.onSearchResponse);
    }

    suggest() {
        var self = this;
        let f: Function = function (): Promise<string[]> {
            let p: Promise<string[]> = new Promise<string[]>((resolve: Function) => {
                self.searchService.suggest(self.searchQuery, self, (sender: any, data: dto.SuggestionResponse) => {
                    resolve(data.Suggestions);
                });
            });
            return p;
        };
        return f;
    }

    protected onSearchResponse(sender: PublicSelector, data: ODataCollectionWrapper<any>, status: number): any {
        sender.searchResult = data.value;
    }

    select(id: number) {
        this.dataService.getEntity(this, this.path, id, "", false, this.onAttachResponse, this.onAttachFailure);
    }

    protected onAttachResponse(sender: PublicSelector, data: breeze.QueryResult) {
        sender.value = data.results[0];
    }

    protected onAttachFailure(sender: PublicSelector, data: any) {
    }

    protected new() {
        let dialog: Promise<ModalDialogInstance>;
        let component = NewPublicWizard;        
        var self = this;
        var newItem: breeze.Entity = self.dataService.createEntity("Public", {});

        let bindings = Injector.resolve([
            provide(ICustomModal, { useValue: newItem }),
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
                    self.value = result;
                else
                    newItem.entityAspect.setDetached();
            }, () => {                
                newItem.entityAspect.setDetached();
            });
        });
    }

    onInit() {
        this.ngOnInit();
    }

    ngOnInit() {
    }


    private drillObject(data: any, path: string): any {
        var delimeter = path.indexOf(".");
        if (delimeter == -1) {
            return data[path];
        }
        else {
            var newPath = path.slice(delimeter + 1);
            var newData = data[path.slice(0, delimeter)];
            return this.drillObject(newData, newPath);
        }
    }
}
