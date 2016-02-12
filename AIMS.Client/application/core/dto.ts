import {FormBuilder, Validators, ControlGroup, Control} from 'angular2/common';
import {IValidatable} from './interfaces';
import {CustomValidators} from './validation';

export namespace dto {    

    export class ChangeRequest {
        requestUri: string;
        method: string;
        data: any;
        headers: any;
    }

    export class SearchRequest {
        SearchQuery: string;
        SearchType: string;
    }

    export class SuggestionResponse {
        Suggestions: string[];
    }

    export class SearchResponse {
        Results: SearchResponseLine[] = [];
    }

    export class SearchResponseLine {
        ID: number;
        EntityType: string;
        Reference: string;
        Name: string;
        Number: string;
        Summary: string;

    }

    export interface IAppConfig {
        api: string;
    }
}