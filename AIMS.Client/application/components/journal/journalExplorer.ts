import {Directive, View, OnInit, EventEmitter, Input, Output, AfterContentInit, ElementRef} from 'angular2/core';
import {IConfigService, ConfigService} from '../../services/configService';

@Directive({
    selector: '[journalExplorer]',
    inputs: ['path']
})
export class JournalExplorer implements AfterContentInit {
    path: string;
    elementRef: ElementRef;
    configService: IConfigService;
    servicePath: string;
    queryOptions: string;

    private valueChange: EventEmitter<any> = new EventEmitter();

    constructor(elementRef: ElementRef, configService: IConfigService) {
        this.elementRef = elementRef;
        this.configService = configService;
        this.servicePath = configService.getSettings().api + "Data.svc";
        this.queryOptions = "?$expand=JournalType,ReportingEntity/Public&$select=*,ReportingEntity/Public/Name,JournalType/Name&$orderby=TxnDate desc";
    }

    ngAfterContentInit() {
        var self = this;
        var element = jQuery(this.elementRef.nativeElement);
        var txnDetailInit = this.buildInitJournalTxnDetail();

        element.kendoGrid({
            dataSource: {
                type: "odata",
                transport: {
                    read: {
                        url: self.servicePath + "/" + self.path + self.queryOptions,
                        dataType: "json"
                    }
                },

                schema: {
                    model: {
                        fields: {
                            'ReportingEntity.Public.Name': { type: "string" },
                            'JournalType.Name': { type: "string" },
                            'Reference': { type: "string" },
                            'TxnDate': { type: "date" },
                            'Amount': { type: "number" },
                            'Description': { type: "string" }
                        }
                    },
                    data: function (data) {
                        return data.value;
                    },
                    total: function (data) {
                        return data['odata.count'];
                    }
                },
                pageSize: 20,
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true
            },

            pageable: true,
            height: 400,            
            sortable: true,
            filterable: true,
            toolbar: ["excel"],
            groupable: true,
            scrollable: true,
            resizable: true,
            detailInit: txnDetailInit,
            columns: [
                { field: "ReportingEntity.Public.Name", title: "Reporting Entity" },
                { field: "TxnDate", title: "Date", format: "{0: yyyy-MM-dd}" },
                { field: "JournalType.Name", title: "Type" },
                { field: "Reference", title: "Reference" },
                { field: "Description", title: "Description" },
                { field: "Amount", title: "Amount" }
            ]
        });
    }


    buildInitJournalTxnDetail() {
        var self = this;
        var ledgerTxnDetail = this.buildInitLedgerTxnDetail();
        return function (e) {
            jQuery("<div/>").appendTo(e.detailCell).kendoGrid({

                dataSource: {
                    type: "odata",
                    transport: {
                        read: {
                            url: self.servicePath + "/Journals(" + e.data.ID + ")/JournalTxns",
                            dataType: "json"
                        }
                    },

                    schema: {
                        model: {
                            fields: {
                                'Amount': { type: "number" },
                                'Description': { type: "string" }
                            }
                        },
                        data: function (data) {
                            return data.value;
                        },
                        total: function (data) {
                            return data['odata.count'];
                        }
                    },
                    pageSize: 20,
                    serverPaging: true,
                    serverFiltering: true,
                    serverSorting: true
                },
                scrollable: true,
                sortable: true,                
                detailInit: ledgerTxnDetail,                    
                columns: [
                            { field: "Description", title: "Description" },
                            { field: "Amount", title: "Amount" }
                    ]                
            });
        }
    }
    

    buildInitLedgerTxnDetail() {
        var self = this;
        var queryOptions = "?$expand=LedgerAccount";
        return function (e) {
            jQuery("<div/>").appendTo(e.detailCell).kendoGrid({

                dataSource: {
                    type: "odata",
                    transport: {
                        read: {
                            url: self.servicePath + "/JournalTxns(" + e.data.ID + ")/LedgerTxns" + queryOptions,
                            dataType: "json"
                        }
                    },

                    schema: {
                        model: {
                            fields: {
                                'AbsAmount': { type: "number" },
                                'Description': { type: "string" },
                                'LedgerAccount.Name': { type: "string" }
                            }
                        },
                        data: function (data) {
                            return data.value;
                        },
                        total: function (data) {
                            return data['odata.count'];
                        }
                    },
                    pageSize: 20,
                    serverPaging: true,
                    serverFiltering: true,
                    serverSorting: true
                },

                scrollable: true,
                sortable: true,                

                columns: [                
                        { field: "Description", title: "Type" },
                        { field: "LedgerAccount.Name", title: "Account" },
                        { field: "AbsAmount", title: "Amount" }
                ]                
            });
        }
    }

}