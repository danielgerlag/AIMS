import {Component, ElementRef, EventEmitter} from 'angular2/core';
@Component({
    selector: 'filereader',        
    providers: [ElementRef],
    inputs: ['accept'],
    events: ['complete'],
    template: `<input type="file" (change)="changeListener($event)" accept="{{accept}}" />`
})

export class InputFileReader {
    complete: EventEmitter<any> = new EventEmitter<any>();
    accept: string;
    constructor(public elementRef: ElementRef) {
    }
    
    changeListener($event: any) {
        var self = this;
        var file: File = $event.target.files[0];                
        var myReader: FileReader = new FileReader();
        myReader.readAsBinaryString(file);
        myReader.onloadend = function (e) {
            self.complete.next(window.btoa(myReader.result));
        };
    }
}