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
        this.queryOptions = "?$expand=JournalType,ReportingEntity/Public&$select=ID,Description,Reference,ReportingEntity/Public/Name,JournalType/Name";
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
            
            //pageSize: 20,
            height: 300,
            scrollable: {
                virtual: true
            },
            sortable: true,
            filterable: true,
            detailInit: txnDetailInit,
            columns: [
                { field: "ReportingEntity.Public.Name", title: "Reporting Entity" },
                { field: "JournalType.Name", title: "Type" },
                { field: "Reference", title: "Reference" },
                { field: "Description", title: "Description" }
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

                scrollable: {
                    virtual: true
                },
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
        var queryOptions = "?$expand=LedgerAccount&$select=Amount,TxnDate,LedgerAccount/Name";
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
                                'Amount': { type: "number" },
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

                scrollable: {
                    virtual: true
                },
                sortable: true,
                columns: [
                    { field: "LedgerAccount.Name", title: "Account" },
                    { field: "Amount", title: "Amount" }
                ]
            });
        }
    }

}