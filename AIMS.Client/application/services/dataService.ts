﻿import {Inject, Injectable, Injector, Component, provide} from 'angular2/core';
import {IConfigService, ConfigService} from './configService';
import {IRemoteService, RemoteService} from './remoteService';
import {Q} from './q';

breeze.config.setQ(Q);
breeze.config.initializeAdapterInstance("modelLibrary", "backingStore", true);
breeze.config.initializeAdapterInstance('dataService', 'OData', true);
//breeze.config.
var OData;
OData = breeze.core.requireLib("OData", "Needed to support remote OData services");

var oldClient = OData.defaultHttpClient;
OData.defaultHttpClient = {
    request: function (request, success, error) {
        //console.log(JSON.stringify(request));
        var uri: string = request.requestUri;
        if (uri.endsWith("$metadata"))
            request.headers = { "Accept": "application/xml" };
        return oldClient.request(request, success, error);
    }
};


export abstract class IDataService {

    abstract getEntity(sender: any, setName: string, id: number, expand: string, clearCache: boolean, success: (sender: any, data: breeze.QueryResult) => any, failure: (sender: any, data: any) => any);
    abstract createEntity(entityType, initialValues): any;
    abstract saveChanges(sender: any, success: (sender: any, data: breeze.SaveResult) => any, failure: (sender: any, data: any) => any);
    abstract detachEntity(entity);
    abstract query(sender: any, query: breeze.EntityQuery, success: (sender: any, data: breeze.QueryResult) => any, failure: (sender: any, data: any) => any);
}

@Injectable()
export class DataService implements IDataService {

        
    private servicePath: string;
    private manager: breeze.EntityManager;

    constructor(        
        @Inject(IConfigService) configService: IConfigService,
        @Inject(IRemoteService) remoteService: IRemoteService
    ) {
                
        //this.apiPath = config.api;  //"../api/";
        //this.apiPath = configService.getSettings().api;

        this.servicePath = configService.getSettings().api + "Data.svc"; 
        this.manager = new breeze.EntityManager(this.servicePath);
        this.manager.fetchMetadata();
                
    }

    


    getEntity(sender: any, setName: string, id: number, expand: string, clearCache: boolean, success: (sender: any, data: breeze.QueryResult) => any, failure: (sender: any, data: any) => any) {
        var query = breeze.EntityQuery.from(setName)
            .where('ID', 'eq', id);

        if (expand)
            query = query.expand(expand);

        if (clearCache)
            this.manager.clear();

        query.using(this.manager).execute()
            .then(function (data) {
                success(sender, data);
            })
            .catch((reason) => {
                failure(sender, reason);
            });
    }


    createEntity(entityType, initialValues) {
        return this.manager.createEntity(entityType, initialValues);
    }

    detachEntity(entity) {
        this.manager.detachEntity(entity);
    }

    query(sender: any, query: breeze.EntityQuery, success: (sender: any, data: breeze.QueryResult) => any, failure: (sender: any, data: any) => any) {
        query.noTracking().using(this.manager).execute()
            .then(function (data) {
                success(sender, data);
            })
            .catch((reason) => {
                failure(sender, reason);
            });
    }

    saveChanges(sender: any, success: (sender: any, data: breeze.SaveResult) => any, failure: (sender: any, data: any) => any) {
        this.manager.saveChanges()
            .then(function (data) {                
                success(sender, data);
            })
            .catch((reason) => {                
                failure(sender, reason);
            });
    }
    
   
}
