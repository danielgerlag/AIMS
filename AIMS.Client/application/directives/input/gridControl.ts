import {Directive, View, OnInit, EventEmitter, Input, Output, AfterContentInit, ElementRef} from 'angular2/core';
import {IConfigService, ConfigService} from '../../services/configService';

@Directive({
    selector: '[odata-grid]',
    inputs: ['path']
})
export class oDataGrid implements AfterContentInit {
    path: string;
    elementRef: ElementRef;
    configService: IConfigService;
    servicePath: string;

    private valueChange: EventEmitter<any> = new EventEmitter();

    constructor(elementRef: ElementRef, configService: IConfigService) {
        this.elementRef = elementRef;
        this.configService = configService;
        this.servicePath = configService.getSettings().api + "Data.svc";
    }

    ngAfterContentInit() {
        var self = this;
        var element = jQuery(this.elementRef.nativeElement);

        element.kendoGrid({
            dataSource: {
                type: "odata",
                transport: {
                    read: {
                        url: self.servicePath + "/" + self.path,
                        dataType: "json"
                    }
                },
                
                schema: {
                    model: {
                        fields: {
                            Name: { type: "string" },
                            FirstName: { type: "string" }
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
            //groupable: true,
            sortable: true,
            filterable: true,

            columns: [
                { field: "Name", title: "Name" },
                { field: "FirstName", title: "First Name" }
            ]
        });
    }


}