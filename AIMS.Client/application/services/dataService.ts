import {Inject, Injectable, Injector, Component, provide} from 'angular2/core';
import {IConfigService, ConfigService} from './configService';
import {IRemoteService, RemoteService} from './remoteService';
import {EntityExts} from '../core/models';
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
    abstract getDetached(sender: any, setName: string, id: number, expand: string, success: (sender: any, data: breeze.QueryResult) => any, failure: (sender: any, data: any) => any);
    abstract createEntity(entityType, initialValues): any;
    abstract saveChanges(sender: any, success: (sender: any, data: breeze.SaveResult) => any, failure: (sender: any, data: any) => any);
    abstract detachEntity(entity);
    abstract query(sender: any, query: breeze.EntityQuery, success: (sender: any, data: breeze.QueryResult) => any, failure: (sender: any, data: any) => any);
    abstract reset();
    abstract cacheEntities(sender: any, setName: string, expand: string, success: (sender: any, data: breeze.QueryResult) => any, failure: (sender: any, data: any) => any);
    abstract getCollection(sender: any, setName: string, id: number, expand: string, success: (sender: any, data: breeze.QueryResult) => any, failure: (sender: any, data: any) => any);
}

function initEntity(entity) {
    entity.init();
}

@Injectable()
export class DataService implements IDataService {

        
    private servicePath: string;
    private rootManager: breeze.EntityManager;
    private manager: breeze.EntityManager;

    constructor(        
        @Inject(IConfigService) configService: IConfigService,
        @Inject(IRemoteService) remoteService: IRemoteService
    ) {
        var self = this;
        this.servicePath = configService.getSettings().api + "Data.svc"; 
        this.rootManager = new breeze.EntityManager(this.servicePath);
        this.rootManager.metadataStore.registerEntityTypeCtor("Public", EntityExts.Public, initEntity);
        this.rootManager.metadataStore.registerEntityTypeCtor("PolicyTransactionTrigger", EntityExts.TransactionTrigger, initEntity);
        this.rootManager.metadataStore.registerEntityTypeCtor("ReportingEntityTransactionTrigger", EntityExts.TransactionTrigger, initEntity);
        this.rootManager.metadataStore.registerEntityTypeCtor("RegionContextParameterValue", EntityExts.ContextParameterValue, initEntity);
        this.rootManager.metadataStore.registerEntityTypeCtor("PolicyContextParameterValue", EntityExts.ContextParameterValue, initEntity);
        this.rootManager.metadataStore.registerEntityTypeCtor("PolicyTypeContextParameterValue", EntityExts.ContextParameterValue, initEntity);
        this.rootManager.metadataStore.registerEntityTypeCtor("PolicySubTypeContextParameterValue", EntityExts.ContextParameterValue, initEntity);

        this.rootManager.fetchMetadata().then((value) => {
            self.rootManager.metadataStore.setEntityTypeForResourceName("TransactionTriggers/AIMS.DomainModel.PolicyTransactionTrigger", "PolicyTransactionTrigger");
            self.rootManager.metadataStore.setEntityTypeForResourceName("TransactionTriggers/AIMS.DomainModel.ReportingEntityTransactionTrigger", "ReportingEntityTransactionTrigger");
            self.manager = self.rootManager.createEmptyCopy();
        }, (reason) => { });

        var valOpts = this.rootManager.validationOptions.using({ validateOnAttach: false, validateOnPropertyChange: false, validateOnSave: false });        
        this.rootManager.setProperties({ validationOptions: valOpts });

        
    }
    
    reset() {
        this.manager = this.rootManager.createEmptyCopy();
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

    getDetached(sender: any, setName: string, id: number, expand: string, success: (sender: any, data: breeze.QueryResult) => any, failure: (sender: any, data: any) => any) {
        var query = breeze.EntityQuery.from(setName)
            .where('ID', 'eq', id);

        if (expand)
            query = query.expand(expand);                

        query.noTracking().using(this.manager).execute()
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
    
    cacheEntities(sender: any, setName: string, expand: string, success: (sender: any, data: breeze.QueryResult) => any, failure: (sender: any, data: any) => any) {
        var query = breeze.EntityQuery.from(setName)            

        if (expand)
            query = query.expand(expand);

        query.using(this.manager).execute()
            .then(function (data) {
                success(sender, data);
            })
            .catch((reason) => {
                failure(sender, reason);
            });
    }

    getCollection(sender: any, setName: string, id: number, expand: string, success: (sender: any, data: breeze.QueryResult) => any, failure: (sender: any, data: any) => any) {
        var query = breeze.EntityQuery.from(setName);            

        if (expand)
            query = query.expand(expand);
        
        query.using(this.manager).execute()
            .then(function (data) {
                success(sender, data);
            })
            .catch((reason) => {
                failure(sender, reason);
            });
    }
   
}
