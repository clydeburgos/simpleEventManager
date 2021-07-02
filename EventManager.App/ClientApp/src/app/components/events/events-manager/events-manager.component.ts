import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { EventModel } from 'src/app/models/event.model';
import { EventService } from 'src/app/services/event.service';

@Component({
  selector: 'app-events-manager',
  templateUrl: './events-manager.component.html',
  styleUrls: ['./events-manager.component.css']
})
export class EventsManagerComponent implements OnInit {

  @Input('data') eventData: any;
  @Output() savedEventEmit = new EventEmitter();
  
  public isNew: boolean = false;
  public model: EventModel;
  public errors: any[];

  constructor(private eventService: EventService, private toastr: ToastrService) { 
    this.model = new EventModel();
    this.errors = [];
  }

  ngOnInit(){
    this.setData();
  }

  setData(){
    if(this.eventData) {
      this.model = this.eventData;
      this.model.startDate = new Date(this.eventData.startDate).toISOString().split('T')[0];
      this.model.endDate = new Date(this.eventData.endDate).toISOString().split('T')[0];
    }
    this.isNew = !this.eventData;
  }

  create(){
    this.eventService.create(this.model).subscribe(() => {
      this.handleSuccessSave('created');
      this.errors = [];
    }, (e: any) => {
      this.handleError(e, 'creation');
    });
  }

  update(){
    this.eventService.update(this.model).subscribe(() => {
      this.handleSuccessSave('updated');
      this.errors = [];
    }, (e: any) => {
      this.handleError(e, 'creation');
    });
  }

  private handleError(e, command){
    this.toastr.error(`Event ${ command } experienced some issues!`, 'Error');
      let errorKeys = Object.keys(e.error.errors);
      this.errors = [];
      errorKeys.forEach(element => {
        let message = e.error.errors[element];
        this.errors.push(message);
        console.log(message);
      });
  }

  private handleSuccessSave(command){
    this.model = new EventModel();
    this.savedEventEmit.emit(command);
    this.toastr.success(`Event ${command}!`, 'Success');
  }
}
