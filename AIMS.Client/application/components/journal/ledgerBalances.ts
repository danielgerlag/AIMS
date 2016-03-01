import {Directive, View, OnInit, EventEmitter, Input, Output, AfterContentInit, ElementRef} from 'angular2/core';
import {IConfigService, ConfigService} from '../../services/configService';

@Directive({
    selector: '[ledgerBalances]',
    inputs: ['source', 'id', 'effectiveDate', 'ledgerID']
})
export class LedgerBalances implements OnInit {

    source: string;
    _id: number;
    _effectiveDate: Date;
    _ledgerID: number;

    elementRef: ElementRef;
    configService: IConfigService;
    servicePath: string;
    queryOptions: string;

    dataSource: kendo.data.DataSource;

    private valueChange: EventEmitter<any> = new EventEmitter();

    get effectiveDate() {
        return this._effectiveDate;
    }

    set effectiveDate(value) {
        if (this._effectiveDate != value) {
            this._effectiveDate = value;
            this.refresh();
        }
    }

    get id() {
        return this._id;
    }

    set id(value) {
        if (this._id != value) {
            this._id = value;
            this.refresh();
        }
    }

    get ledgerID() {
        return this._ledgerID;
    }

    set ledgerID(value) {
        if (this._ledgerID != value) {
            this._ledgerID = value;
            this.refresh();
        }
    }

    constructor(elementRef: ElementRef, configService: IConfigService) {
        this.elementRef = elementRef;
        this.configService = configService;
        this.servicePath = configService.getSettings().api + "Data.svc";
        this.queryOptions = "";
        //?$expand=LedgerAccount,ReportingEntity/Public&$select=Amount,TxnDate,LedgerAccount/Name,ReportingEntity/Public/Name

    }

    ngOnInit() {
        var self = this;
        //var dateStr = moment(self.effectiveDate).format('YYYY-MM-DD');
        //var path = "GetLedgerAccountBalances?source='" + self.source + "'&id=" + self.id + "&effectiveDate='" + dateStr + "'";
        var element = jQuery(this.elementRef.nativeElement);
        var ledgerTxnDetail = this.buildInitLedgerTxnDetail();

        element.kendoGrid({
            dataSource: self.dataSource,
            toolbar: ["excel"],
            groupable: true,
            height: 350,
            scrollable: true,
            sortable: true,
            filterable: true,
            resizable: true,
            detailInit: ledgerTxnDetail,
            columns: self.buildColumns()
        });

        //self.refresh();
    }


    buildColumns() {
        if (this.source == 'ReportingEntity') {
            return [
                { field: "LedgerAccountName", title: "Account" },
                { field: "LedgerAccountType", title: "Account Type" },
                { field: "Balance", title: "Balance" }
            ];
        }

        if (this.source == 'Policy') {
            return [
                { field: "ReportingEntityName", title: "Reporting Entity" },
                { field: "LedgerAccountName", title: "Account" },
                { field: "LedgerAccountType", title: "Account Type" },
                { field: "PublicName", title: "Public" },
                { field: "Balance", title: "Balance" }
            ];
        }

        if (this.source == 'Public') {
            return [
                { field: "ReportingEntityName", title: "Reporting Entity" },
                { field: "LedgerAccountName", title: "Account" },
                { field: "PolicyNumber", title: "Policy" },
                { field: "Balance", title: "Balance" }
            ];
        }

        if (this.source == 'Agent') {
            return [
                { field: "ReportingEntityName", title: "Reporting Entity" },
                { field: "LedgerAccountName", title: "Account" },
                { field: "Balance", title: "Balance" }
            ];
        }
    }

    refresh() {
        var self = this;
        var element = jQuery(this.elementRef.nativeElement);
        var grid = element.data("kendoGrid");

        if (grid) {

            var dateStr = moment(self.effectiveDate).format('YYYY-MM-DD');
            var path = "GetLedgerAccountBalances?source='" + self.source + "'&id=" + self.id + "&ledgerID=" + self.ledgerID + "&effectiveDate='" + dateStr + "'";

            self.dataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: self.servicePath + "/" + path + self.queryOptions,
                        dataType: "json"
                    }
                },

                schema: {
                    model: {
                        fields: {
                            'ReportingEntityName': { type: "string" },
                            'ReportingEntityID': { type: "number" },
                            'LedgerAccountID': { type: "number" },
                            'LedgerAccountName': { type: "string" },
                            'LedgerAccountType': { type: "string" },
                            'PublicName': { type: "string" },
                            'PublicID': { type: "number" },
                            'PolicyID': { type: "number" },
                            'EffectiveDate': { type: "date" },
                            'Balance': { type: "number" }
                        }
                    },
                    data: function (data) {
                        return data.value;
                    },
                    total: function (data) {
                        return data['odata.count'];
                    }
                }
            });

            self.dataSource.read();
            grid.setDataSource(self.dataSource);
        }

    }

    buildInitLedgerTxnDetail() {
        var self = this;
        var queryOptions = "";
        return function (e) {
            var dateStr = moment(e.data.EffectiveDate).format("YYYY-MM-DD");
            var query = "/GetLedgerTxnBalances?reportingEntityID=" + e.data.ReportingEntityID + "&ledgerAccountID=" + e.data.LedgerAccountID + "&effectiveDate='" + dateStr + "'";

            if (e.data.PublicID) {
                query = query + "&publicID=" + e.data.PublicID;
            }

            if (e.data.PolicyID) {
                query = query + "&policyID=" + e.data.PolicyID;
            }

            jQuery("<div/>").appendTo(e.detailCell).kendoGrid({

                dataSource: {
                    //type: "odata",
                    transport: {
                        read: {
                            url: self.servicePath + query,
                            dataType: "json"
                        }
                    },

                    schema: {
                        model: {
                            fields: {
                                'Amount': { type: "number" },
                                'Balance': { type: "number" },
                                'Description': { type: "string" },
                                'Reference': { type: "string" },
                                'PolicyNumber': { type: "string" },
                                'TxnDate': { type: "date" }
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
                resizable: true,
                columns: [
                    { field: "TxnDate", title: "Date", format: "{0: yyyy-MM-dd}" },
                    { field: "Description", title: "Description" },
                    { field: "Reference", title: "Reference" },
                    { field: "PolicyNumber", title: "Policy" },
                    { field: "Amount", title: "Amount" },
                    { field: "Balance", title: "Balance" }]

            });
        }
    }
}