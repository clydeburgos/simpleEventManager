import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { EventsListComponent } from '../events/events-list/events-list.component';
import { EventsManagerComponent } from '../events/events-manager/events-manager.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public managerView: boolean = false;
  public eventData: any;
  @ViewChild('eventList', {static : false}) eventList: EventsListComponent;
  @ViewChild('eventsManager', { static : false }) eventsManager: EventsManagerComponent;

  constructor(private changeDetect: ChangeDetectorRef) {
    
  }

  ngOnInit(): void {
   
  }

  create(){
    this.managerView = true;
    this.eventData = null;
  }

  cancel(){
    this.managerView = false;
  }

  savedEventEmit(action){
    this.eventList.getEvents();
    this.eventData = null;
    this.managerView = false;
    this.changeDetect.detectChanges();
  }

  showDetailEventEmit(data) {
    if(data) {
      this.managerView = true;
      this.eventData = data;
    } else {
      this.managerView = false;
    }
    this.changeDetect.detectChanges();
    setTimeout(() => {
      this.eventsManager.setData();
    }, 500);
    
  }

  
}
