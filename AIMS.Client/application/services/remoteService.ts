import {Inject, Injectable, Injector, Component, provide} from 'angular2/core';
import {BaseRequestOptions, Http, Headers, Response} from 'angular2/http';
import {Observable, Subscription} from 'rxjs/Rx';
import {IConfigService, ConfigService} from './configService';

import {Q} from './q';

breeze.config.setQ(Q);

//var OData;


export abstract class IRemoteService {
    abstract post(sender: any, url: string, data: any, callback: (sender: any, data: any, status: number) => any): Subscription<any>;
    abstract put(sender: any, url: string, data: any, callback: (sender: any, data: any, status: number) => any): Subscription<any>;
    abstract get(sender: any, url: string, callback: (sender: any, data: any, status: number) => any): Subscription<any>;
    abstract fetchBlob(sender: any, url: string, data: any, callback: (sender: any, data: any, status: number) => any);

    abstract postODataChangeRequest(requests: any[]);

}

@Injectable()
export class RemoteService implements IRemoteService {

    private http: Http;
    private apiPath: string;
    //private configService: IConfigService;

    constructor(
        @Inject(Http) http: Http,
        @Inject(IConfigService) configService: IConfigService) {
        
        this.http = http;        
        //this.apiPath = config.api;  //"../api/";
        this.apiPath = configService.getSettings().api;
    }

    post(sender: any, url: string, data: any, callback: (sender: any, data: any, status: number) => any): Subscription<any> {
        var self = this;
        var headers = new Headers();
        headers.append('Accept', 'application/json, text/plain, */*');
        headers.append('Content-Type', 'application/json');                

        var observable = self.http.post(self.apiPath + url, JSON.stringify(data), { headers: headers });
        var subscription = observable.subscribe(
            response => {                
                var responseData: any;
                try {
                    if (response.status != 204)
                        responseData = self.decodeResponse(response);
                }
                catch (ex) {
                    //alert(ex);
                    console.error(ex);
                    //logging
                }
                
                callback(sender, responseData, response.status);
                //subscription.unsubscribe();
            }
        );
        return subscription;
    }

    put(sender: any, url: string, data: any, callback: (sender: any, data: any, status: number) => any): Subscription<any> {
        var self = this;
        var headers = new Headers();
        headers.append('Accept', 'application/json, text/plain, */*');
        headers.append('Content-Type', 'application/json');

        if (data["odata.etag"])
            headers.append("If-Match", data["odata.etag"]);

        var observable = self.http.put(self.apiPath + url, JSON.stringify(data), { headers: headers });        
        var subscription = observable.subscribe(
            response => {
                var responseData: any;
                try {
                    if (response.status != 204)
                        responseData = self.decodeResponse(response);
                }
                catch (ex) {
                    //logging
                    console.error(ex);
                }
                callback(sender, responseData, response.status);
                //subscription.unsubscribe();
            }
        );
        return subscription;
    }

    get(sender: any, url: string, callback: (sender: any, data: any, status: number) => any): Subscription<any> {
        var self = this;
        var headers = new Headers();
        headers.append('Accept', 'application/json, text/plain, */*');
        headers.append('Content-Type', 'application/json');
        var observable = self.http.get(self.apiPath + url, { headers: headers });
        var subscription = observable.subscribe(
            response => {
                var responseData: any;
                try {
                    if (response.status != 204)
                        responseData = self.decodeResponse(response);
                }
                catch (ex) {
                    //logging
                    console.error(ex);
                }
                callback(sender, responseData, response.status);
                //subscription.unsubscribe();
            }
        );
        return subscription;
    }


    fetchBlob(sender: any, url: string, data: any, callback: (sender: any, data: any, status: number) => any) {
        var xhr = new XMLHttpRequest();
        xhr.open('POST', this.apiPath + url, true);
        xhr.responseType = 'blob';
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onload = function (e) {
            if (this.status == 200) {
                callback(sender, this.response, this.status);
            }
        };
        xhr.send(JSON.stringify(data));
    }


    postODataChangeRequest(requests: any[]) {
        //window.OData.
                
        //OData.request({
        //    requestUri: this.apiPath + "Data.svc/$batch",
        //    method: "POST",
        //    data: {
        //        __batchRequests: [
        //            {
        //                __changeRequests: requests
        //            }
        //        ]
        //    }
        //}, function (data, response) {
        //    //success handler

        //    alert(JSON.stringify(data));
        //    alert(JSON.stringify(response));

        //    }, undefined, OData.batchHandler);

    }

    test() {

        
        breeze.config.initializeAdapterInstance('dataService', 'webApiOData', true);

        var serviceName = this.apiPath + "Data.svc"; // route to the Web Api controller
        var manager = new breeze.EntityManager(serviceName);

        //return manager.createEntity('TodoList', initialValues);
        //todoItem.entityAspect.setDeleted();

        //return datacontext.getTodoLists(forceRefresh).then(function (data) {
        //    return vm.todoLists = data;
        //});

    }



    private decodeResponse(response: Response) {
        var contentType = response.headers.get('Content-Type');

        if (contentType.startsWith("application/json")) {
            return response.json();
        }

        if (contentType.startsWith("application/pdf")) {
            return response.text;
        }

        response.blob
    }
}
