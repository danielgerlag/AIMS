import {Component, provide, ElementRef, Injector, IterableDiffers, KeyValueDiffers, Renderer} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';
import {Http, Headers} from 'angular2/http'
import {RouteParams} from 'angular2/router';
import {ROUTER_DIRECTIVES} from 'angular2/router';
import {Typeahead} from 'ng2-bootstrap/ng2-bootstrap';
import {IRemoteService} from '../services/remoteService';
//import {IAuthService, AuthService, TransientInfo} from '../services/authService';
import {IShellService, ShellService, ShellInfo} from '../services/shellService';
import {ISearchService, SearchService} from '../services/searchService';
import {dto} from '../core/dto';



import {oDataGrid} from '../directives/input/gridControl';

import {ICustomModal, ModalDialogInstance, ModalConfig, Modal} from 'angular2-modal/angular2-modal';
//import {YesNoModalContent, YesNoModal} from '../../../angular2-modal/commonModals/yesNoModal';
//import {OKOnlyContent, OKOnlyModal} from '../../../angular2-modal/commonModals/okOnlyModal';
import {AdditionCalculateWindowData, AdditionCalculateWindow} from '../directives/policy/customModal';




var config: any;

@Component({
    selector: 'home',
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, Typeahead, oDataGrid],
    providers: [Modal],
    templateUrl: './application/home/home.html'    
})
export class Home {

    
    private searchService: ISearchService;
    public searchStr: string;
    public searchType: string;
    public debugStr: string;

    public testDate: Date;

    //userInfo: TransientInfo;
    public shellInfo: ShellInfo;
    public searchResponse: dto.SearchResponse;
    

    constructor(shellService: IShellService, searchService: ISearchService,
        private modal: Modal, private elementRef: ElementRef,
        private injector: Injector, private _renderer: Renderer
    ) {
        
        this.searchService = searchService;
        //this.userInfo = authService.userInfo;
        this.shellInfo = shellService.info;
        this.searchResponse = new dto.SearchResponse();
        this.testDate = new Date(2001, 1, 1);
        this.searchType = "";
    }    

    public search() {
        this.searchService.search(this.searchStr, this.searchType, this, (sender: Home, data: dto.SearchResponse) => {
            sender.searchResponse = data;
        });
    }
    
    public suggest(): Function {
        var self = this;
        let f: Function = function (): Promise<string[]> {
            let p: Promise<string[]> = new Promise<string[]>((resolve: Function) => {          
                self.searchService.suggest(self.searchStr, self, (sender: Home, data: dto.SuggestionResponse) => {                    
                    resolve(data.Suggestions);                    
                });

            });
            return p; 
        };
        return f;
    }

    select(item: dto.SearchResponseLine) {
        this.searchService.selectResult(item);
    }

    mapDescription(source: string): string {
        //switch (source) {
        //    case "Party": return "PARTY";
        //    case "BrokerClient": return "CLIENT";
        //    case "BrokerClientQuote": return "QUOTE";
        //}
        return source;
    }

    private testVal: any;

    test() {
        this.testDate = new Date(2020, 5, 5);
    }
}
