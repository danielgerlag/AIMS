import {Inject, Injectable} from 'angular2/core';
import {ROUTER_DIRECTIVES, RouteConfig, Location, ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, Route, AsyncRoute, Router} from 'angular2/router';

import {Subscription} from 'rxjs/Rx';
import {IShellService} from './shellService';
import {IRemoteService} from './remoteService';
import {dto} from '../core/dto';


export abstract class ISearchService {
    abstract search(query, searchType, sender: any, callback: (sender: any, data: dto.SearchResponse) => any): void;
    abstract suggest(query, sender: any, callback: (sender: any, data: dto.SuggestionResponse) => any): void;
    abstract selectResult(item: dto.SearchResponseLine);
}

@Injectable()
export class SearchService implements ISearchService {

    private remoteService: IRemoteService;
    private shellService: IShellService;
    private router: Router;

    constructor( @Inject(Router) router: Router, @Inject(IRemoteService) remoteService: IRemoteService, @Inject(IShellService) shellService: IShellService) {
        this.remoteService = remoteService;
        this.shellService = shellService;
        this.router = router;
    }

    public search(query, searchType, sender: any, callback: (sender: any, data: dto.SearchResponse) => any): void {        
        var request: dto.SearchRequest = new dto.SearchRequest();
        request.SearchQuery = query;
        request.SearchType = searchType;
        this.shellService.showLoader("Searching...");

        this.remoteService.post(this, 'Search.svc/DoSearch', request, (sender1: SearchService, data1: dto.SearchResponse, status: number) => {
            sender1.shellService.hideLoader();
            callback(sender, data1);
        });
    }

    public suggest(query, sender: any, callback: (sender: any, data: dto.SuggestionResponse) => any): void {
        var request: dto.SearchRequest = new dto.SearchRequest();
        request.SearchQuery = query;
        this.shellService.showLoader("Searching...");

        this.remoteService.post(this, 'Search.svc/Suggest', request, (sender1: SearchService, data1: dto.SuggestionResponse, status: number) => {
            sender1.shellService.hideLoader();
            callback(sender, data1);
        });
    }

    public selectResult(item: dto.SearchResponseLine) {
        switch (item.EntityType) {
            case "Public":
                this.router.navigateByUrl("EditPublic/" + item.ID);
                break;
            case "Policy":
                this.router.navigateByUrl("EditPolicy/" + item.ID);
                break;
        }
    }



}
