import {Component, View, provide} from 'angular2/core';
import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';
import {IShellService} from '../services/shellService';
import {IDataService} from '../services/dataService';
import {IValidatable} from './interfaces';
import {ModelErrorContainer} from './interfaces';
import {BaseController} from './baseController'

export abstract class ListController extends BaseController {
        
    protected dataService: IDataService;
    protected router: Router;

    public data: breeze.Entity[];
    public columns: any;

    constructor(shellService: IShellService, dataService: IDataService, router: Router) {
        super(shellService);
        this.dataService = dataService;
        this.router = router;
        this.data = [];
        this.columns = this.getColumns();
        this.loadData();
    }
        
    protected abstract setName(): string;
    protected abstract getColumns(): any;
    protected abstract itemRoute(): string;

    protected expandFields(): string[] {
        return [];
    }

    protected loadData() {

        var query = breeze.EntityQuery.from(this.setName());        
        var expandList = this.expandFields();        

        if (expandList.length > 0)
            query = query.expand(expandList);

        this.dataService.query(this, query, this.onLoadSuccess, this.onLoadFailure);
    }
        
    protected onLoadSuccess(sender: ListController, data: breeze.QueryResult): any {
        sender.shellService.hideLoader();
        sender.data = data.results;
    }

    protected onLoadFailure(sender: ListController, data: any): any {
        sender.shellService.hideLoader();
        //
    }

    protected selectItem(id) {
        this.router.navigateByUrl(this.itemRoute() + "/" + id);
    }

    protected addItem() {
        this.router.navigateByUrl(this.itemRoute());
    }

}