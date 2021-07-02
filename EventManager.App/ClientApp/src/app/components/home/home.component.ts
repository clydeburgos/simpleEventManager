import { Component, OnInit, ViewChild } from '@angular/core';
import { EventsListComponent } from '../events/events-list/events-list.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public managerView: boolean = false;
  public eventData: any;
  @ViewChild('eventList', {static : false}) eventList: EventsListComponent;
  constructor() {
    
  }

  ngOnInit(): void {
   
  }

  create(){
    this.managerView = true;
  }

  cancel(){
    this.managerView = false;
  }

  savedEventEmit(action){
    this.eventList.getEvents();
  }

  
}
