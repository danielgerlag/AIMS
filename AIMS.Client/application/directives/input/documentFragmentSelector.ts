import {Component, View, OnInit, EventEmitter} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, ControlGroup} from 'angular2/common';
import {IRemoteService} from '../../services/remoteService';
import {ODataWrapper} from '../../core/interfaces'
import {EntityDropdown} from '../../directives/input/entityDropdown';
import {DocumentBuilderModels} from '../../core/models'


@Component({
    selector: 'fragment-selector',    
    inputs: ['value', 'documentDefinitionID', 'fragmentType'],
    outputs: ['valueChange']
})
@View({
        template: `    
    <ul class="list-group">
      <li class="list-group-item" *ngFor="#frag of value"  >
          <div class="form-inline">
              <span *ngIf="!frag.Custom" class="form-inline">
                  <entity-dropdown class="form-control-inline"
                                        [(value)]="frag.ContentFragmentID"                                     
                                        query="GetAvailableContentFragments?documentDefinitionID={{documentDefinitionID}}&fragmentType='{{fragmentType}}'" 
                                        keyField="ID" 
                                        displayField="Name">
                  </entity-dropdown>
              </span>
              <span *ngIf="frag.Custom" class="form-inline">
                <input type="text" [(ngModel)]="frag.CustomContent">
              </span>          
              <button type="button" class="btn" (click)="removeFragment(frag)"><i class="fa fa-trash fa-fw"></i></button>
              <button type="button" class="btn" (click)="moveUp(frag)"><i class="fa fa-arrow-up fa-fw"></i></button>
              <button type="button" class="btn" (click)="moveDown(frag)"><i class="fa fa-arrow-down fa-fw"></i></button>
          </div>
      </li>  
    </ul>
    <button type="button" class="btn" (click)="addFragment(false)">Add</button>
    <button type="button" class="btn" (click)="addFragment(true)">Add Custom</button>    
  `,
    directives: [CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, EntityDropdown]
})
export class FragmentSelector implements OnInit {

    private documentDefinitionID: number;
    private fragmentType: string;
    private remoteService: IRemoteService;
    private fragments: DocumentBuilderModels.DocumentFragment[];
    
    public valueChange: EventEmitter<any> = new EventEmitter();
    
    private selectList: Array<any> = [];

    constructor(remoteService: IRemoteService) {
        this.remoteService = remoteService;
    }

    ngOnInit() {
    }

    onInit() {
        this.ngOnInit();
    }

    

    public get value() {
        return this.fragments.sort((a, b) => { return (a.Order - b.Order); });
    }
    public set value(value) {
        this.fragments = value;
        this.onValueChanged();
    }

    onValueChanged() {
        this.valueChange.next(this.fragments);
    }

    public addFragment(custom: boolean) {
        var frag = new DocumentBuilderModels.DocumentFragment();
        frag.Order = this.fragments.length;
        frag.Custom = custom;
        this.fragments.push(frag);
        this.onValueChanged();
    }

    public removeFragment(item: DocumentBuilderModels.DocumentFragment) {        
        var idx = this.fragments.indexOf(item);
        if (idx > -1)
            this.fragments.splice(idx, 1);        
    }

    public moveUp(item: DocumentBuilderModels.DocumentFragment) {
        var sibling = this.findPrevious(item);
        if (sibling) {
            var oldValue = item.Order;
            item.Order = sibling.Order;
            sibling.Order = oldValue;            
        }        
    }

    public moveDown(item: DocumentBuilderModels.DocumentFragment) {
        var sibling = this.findNext(item);
        if (sibling) {
            var oldValue = item.Order;
            item.Order = sibling.Order;
            sibling.Order = oldValue;
        }
    }

    private findPrevious(item: DocumentBuilderModels.DocumentFragment): DocumentBuilderModels.DocumentFragment {
        var sorted = this.fragments.sort((a, b) => { return (a.Order - b.Order); });
        var itemIndex = sorted.indexOf(item);
        if (itemIndex < 1)
            return null;
        return sorted[itemIndex - 1];
    }

    private findNext(item: DocumentBuilderModels.DocumentFragment): DocumentBuilderModels.DocumentFragment {
        var sorted = this.fragments.sort((a, b) => { return (a.Order - b.Order); });
        var itemIndex = sorted.indexOf(item);
        if (itemIndex == (sorted.length - 1))
            return null;
        return sorted[itemIndex + 1];
    }


    

}

