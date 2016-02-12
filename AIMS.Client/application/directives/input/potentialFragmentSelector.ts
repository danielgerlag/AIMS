import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {IRemoteService} from '../../services/remoteService';
import {IDataService} from '../../services/dataService';
import {ODataWrapper} from '../../core/interfaces'
import {EntityDropdown} from '../../directives/input/entityDropdown';


@Component({
    selector: 'fragment-selector',    
    inputs: ['value', 'documentCategoryID', 'fragmentType'],
    outputs: ['valueChange']
})
@View({
    template: `    
    <ul class="list-group">
      <li class="list-group-item" *ngFor="#frag of value" [hidden]="frag.entityAspect.entityState.isDeleted == true" >
          <div class="form-inline">
              <entity-dropdown class="form-control-inline"                                              
                                    [(value)]="frag.ContentFragmentID" 
                                    name="ContentFragmentID"                                              
                                    query="GetPotentialContentFragments?documentCategoryID={{documentCategoryID}}&fragmentType='{{fragmentType}}'" 
                                    keyField="ID" 
                                    displayField="Name">
              </entity-dropdown>
          
              <button type="button" class="btn" (click)="removeFragment(frag)">Remove</button>
          </div>
      </li>  
    </ul>
    <button type="button" class="btn" (click)="addFragment()">Add</button>
  `,
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, EntityDropdown]
})
export class FragmentSelector implements OnInit {

    private remoteService: IRemoteService;
    private dataService: IDataService;

    private fragments: any[];

    public documentCategoryID: number;
    public fragmentType: string;
    
    public valueChange: EventEmitter<any> = new EventEmitter();

    private selectList: Array<any> = [];

    constructor(remoteService: IRemoteService, dataService: IDataService) {
        this.remoteService = remoteService;
        this.dataService = dataService;
    }

    ngOnInit() {
    }

    onInit() {
        this.ngOnInit();
    }

    public get value() {
        return this.fragments;
    }
    public set value(value) {
        this.fragments = value;
        this.onValueChanged();
    }

    onValueChanged() {
        this.valueChange.next(this.fragments);
    }

    public addFragment() {
        var item;
        switch (this.fragmentType) {
            case "H":
                item = this.dataService.createEntity("HeaderFragment", {});
                break;
            case "B":
                item = this.dataService.createEntity("BodyFragment", {});
                break;
            case "F":
                item = this.dataService.createEntity("FooterFragment", {});
                break;
        }        

        this.fragments.push(item);
        this.onValueChanged();
    }

    public removeFragment(item: any) {        
        item.entityAspect.setDeleted();        
        var idx = this.fragments.indexOf(item);
        if (idx > -1)
            this.fragments.splice(idx, 1);
        
    }

    

}

