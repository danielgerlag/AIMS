import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';

import {Typeahead} from '../../../node_modules/ng2-bootstrap/ng2-bootstrap';
import {IRemoteService} from '../../services/remoteService';
import {ISearchService} from '../../services/searchService';
import {ODataWrapper, ODataCollectionWrapper} from '../../core/interfaces';
import {dto} from '../../core/dto';


@Component({
    selector: 'entity-selector',
    inputs: ['value', 'path', 'columns'],
    outputs: ['valueChange']
})
@View({
    templateUrl: './application/directives/input/entitySelector.html',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, Typeahead]
})
export class EntitySelector implements OnInit {

    private _entityId: number;    
    searchQuery: string;
    searchResult: any[];
    lookupText: string;

    private valueChange: EventEmitter<any> = new EventEmitter();

    public columns: any;
    public path: string;

    remoteService: IRemoteService;
    searchService: ISearchService;

    get value() {
        return this._entityId;
    }
    set value(value) {
        this._entityId = value;
        this.onEntityChanged();
    }        

    constructor(remoteService: IRemoteService, searchService: ISearchService) {
        this.remoteService = remoteService;
        this.searchService = searchService;        
    }
        

    onEntityChanged() {
        if (this._entityId > 0) {
            this.remoteService.get(this, "Data.svc/GetLookupText?set='" + this.path + "'&id=" + this._entityId, (sender: EntitySelector, data: ODataWrapper<string>, status: number) => {
                sender.lookupText = data.value;
            });
        }
        else {
            this.lookupText = "";
        }
        this.valueChange.next(this._entityId);
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

    protected onSearchResponse(sender: EntitySelector, data: ODataCollectionWrapper<any>, status: number): any {
        sender.searchResult = data.value;
    }

    select(id: number) {
        this.value = id;
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
