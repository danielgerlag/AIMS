import {Directive, View, OnInit, EventEmitter, Input, Output, AfterContentInit, ElementRef} from 'angular2/core';
import {IConfigService, ConfigService} from '../../services/configService';

@Directive({
    selector: '[ledgerBalances]',
    inputs: ['source', 'id', 'effectiveDate']
})
export class LedgerBalances implements OnInit {
    
    source: string;
    _id: number;
    _effectiveDate: Date;

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
        this._effectiveDate = value;
        this.refresh();
    }

    get id() {
        return this._id;
    }

    set id(value) {
        this._id = value;
        this.refresh();
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
        var dateStr = moment(self.effectiveDate).format('YYYY-MM-DD');
        var path = "GetLedgerAccountBalances?source='" + self.source + "'&id=" + self.id + "&effectiveDate='" + dateStr + "'";
        var element = jQuery(this.elementRef.nativeElement);        

        element.kendoGrid({
            dataSource: self.dataSource,
            height: 300,
            scrollable: true,
            sortable: true,
            filterable: true,
            columns: self.buildColumns()
        });

        //self.refresh();
    }

    buildColumns() {
        if (this.source == 'ReportingEntity') {
            return [                
                { field: "LedgerAccountName", title: "Account" },
                { field: "Balance", title: "Balance" }
            ];
        }

        if (this.source == 'Policy') {
            return [
                { field: "ReportingEntityName", title: "Reporting Entity" },
                { field: "LedgerAccountName", title: "Account" },
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
    }

    refresh() {
        var self = this;
        var element = jQuery(this.elementRef.nativeElement);
        var grid = element.data("kendoGrid");

        if (grid) {

            var dateStr = moment(self.effectiveDate).format('YYYY-MM-DD');
            var path = "GetLedgerAccountBalances?source='" + self.source + "'&id=" + self.id + "&effectiveDate='" + dateStr + "'";

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
                            'LedgerAccountName': { type: "string" },
                            'PublicName': { type: "string" },
                            'PolicyNumber': { type: "string" },
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
    

}