import {Component, OnInit, EventEmitter, Input, View, provide, ElementRef, Injector, IterableDiffers, KeyValueDiffers, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {Typeahead} from '../../../node_modules/ng2-bootstrap/ng2-bootstrap';
import {IRemoteService} from '../../services/remoteService';
import {ISearchService} from '../../services/searchService';
import {IDataService} from '../../services/dataService';
import {ODataWrapper, ODataCollectionWrapper} from '../../core/interfaces';
import {dto} from '../../core/dto';

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal, ICustomModalComponent} from 'angular2-modal/angular2-modal';


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
        if (this.entity) {
            var e: any = this.entity;
            this.lookupText = e.fullName();            
        }
        else {
            this.lookupText = "";
        }    
    }

    constructor(
        remoteService: IRemoteService,
        
        private modal: Modal, private elementRef: ElementRef,
        private injector: Injector, private _renderer: Renderer) {
        this.remoteService = remoteService;        
        this.path = "Publics";
        this.columns = [{ Title: 'Name', Name: 'Name' }, { Title: 'First Name', Name: 'FirstName' }];
    }


    onEntityChanged() {
        this.valueChange.emit(this.entity);
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
                if (result) {
                    self.value = result;
                    self.onEntityChanged();
                }            
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

    openSearch() {
        let dialog: Promise<ModalDialogInstance>;
        let component = SearchPopup;
        var self = this;        

        let bindings = Injector.resolve([
            provide(ICustomModal, { useValue: this }),
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
                if (result) {
                    self.value = result;
                    self.onEntityChanged();
                }
            }, () => {
                //
            });
        });
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


@Component({
    selector: 'modal-content',
    template: `
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" (click)="cancel()">&times;</button>
                <h4 class="modal-title">Search</h4>
            </div>
            <div class="modal-body">
                <form class="form-horizontal" role="form">

                    <div class="input-group">
                        <input type="text" placeholder="Search"
                               [(ngModel)]="searchQuery"
                               [typeahead]="suggest()"
                               [typeaheadWaitMs]="300"
                               class="form-control">
                        <div class="input-group-btn">
                            <div class="btn-group" role="group">
                                <button type="submit" class="btn btn-default" (click)="search()">
                                    <i class="fa fa-search"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </form>
                <table class="table table-hover">
                    <tr>
                        <th *ngFor="#col of columns">{{ col.Title }}</th>
                    </tr>
                    <tbody>
                        <tr *ngFor="#item of searchResult" (click)="select(item.ID)">
                            <td *ngFor="#col of columns">{{ drillObject(item, col.Name) }}</td>
                        </tr>
                    </tbody>
                </table>

            </div>
        </div>
`,
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, Typeahead]
})
class SearchPopup implements ICustomModalComponent {

    searchQuery: string;
    searchResult: any[];
    searchService: ISearchService;
    dialog: ModalDialogInstance;
    //context: breeze.Entity;
    caller: PublicSelector;
    public columns: any;


    constructor(dialog: ModalDialogInstance, data: ICustomModal, searchService: ISearchService) {
        this.searchService = searchService;
        this.dialog = dialog;
        this.caller = <PublicSelector>data;
        this.columns = this.caller.columns;
    }


    beforeDismiss(): boolean {
        return true;
    }

    beforeClose(): boolean {
        return false;
    }        

    protected cancel() {
        this.dialog.close(null);
    }

    search() {
        var self = this;        
        self.caller.remoteService.get(self, 'Data.svc/Search' + self.caller.path + "?query='" + self.searchQuery + "'", self.onSearchResponse);
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

    protected onSearchResponse(sender: SearchPopup, data: ODataCollectionWrapper<any>, status: number): any {        
        sender.searchResult = data.value;
    }

    select(id: number) {
        this.caller.dataService.getEntity(this, this.caller.path, id, "", false, this.onAttachResponse, this.onAttachFailure);
    }

    protected onAttachResponse(sender: SearchPopup, data: breeze.QueryResult) {
        sender.dialog.close(data.results[0]);
    }

    protected onAttachFailure(sender: SearchPopup, data: any) {
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