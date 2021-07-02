import { Component, OnInit, ViewChild } from '@angular/core';
import { GridComponent, PageSettingsModel } from '@syncfusion/ej2-angular-grids';
import { BehaviorSubject, Observable } from 'rxjs';
import { EventModel } from 'src/app/models/event.model';
import { EventService } from 'src/app/services/event.service';

@Component({
  selector: 'app-events-list',
  templateUrl: './events-list.component.html',
  styleUrls: ['./events-list.component.css']
})
export class EventsListComponent implements OnInit {

  public isFetching: boolean;
  public eventsData: BehaviorSubject<EventModel[]>;
  public events: any[];
  public filterOptions: any;
  public dateFormat: any = { type:"date", format:"yyyy-MM-dd" }; 
  
  @ViewChild('eventsGrid', { static : false }) public grid: GridComponent;
  public selectedRowData: any;

  public pageSettings: PageSettingsModel;
  constructor(private eventService: EventService) { 
    this.eventsData = new BehaviorSubject(null);
  }

  ngOnInit() {
    this.pageSettings = { pageSize: 10 };
    this.filterOptions = {
      type: 'Menu'
    }
    this.getEvents();
  }

  getEvents(){
    let service = this.eventService.getAll().subscribe((res : any[]) => {
      this.eventsData.next(res);
      this.events = res;
    }, (error: any) => {
      //toast
    }, () => {
      service.unsubscribe();
    });
  }
  

  rowDataBound(args){ 
    args.data.startDate = new Date(args.data.startDate);
    args.data.endDate = new Date(args.data.endDate); 
  } 

  rowSelected(args){
    this.selectedRowData = args.data;
   }

  viewDetails(data){
    // this.windowService.open(CustomerTemplateDetailsComponent, {
    //   title : `View / Edit Template`,
    //   context :
    //   {
    //     command: 1,
    //     templateData: data
    //   }
    // }).onClose.subscribe(() => {
    //   this.templateEventEmmit.emit({ command : 'refresh'});
    //   setTimeout(() => {
    //     this.getCustomerTemplates();
    //   }, 1000);

    // });
  }

  delete(data){
    this.selectedRowData = data; // set the current row
    this.grid.selectRow(Number(data.index)); //data contains the index, so automatically pick the index for the selectRow

    if(!this.selectedRowData){
      return;
    }

    this.grid.deleteRecord();
    this.grid.refresh();

    this.eventService.delete(data.identifier).subscribe((res) => {
      this.getEvents();
    });
  }
}
