import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { 
  EVENT_CREATE_URL, EVENT_DELETE_URL, 
  EVENT_GETALL_URL, EVENT_GET_URL, 
  EVENT_UPDATE_URL } from '../constants/event-endpoints';
import { EventModel } from '../models/event.model';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http: HttpClient){
    
  }

  getAll(): Observable<EventModel[]> {
    return this.http.get<any>(`${ environment.apiUrl }${ EVENT_GETALL_URL }`);
  }

  get(id: string): Observable<EventModel> {
    return this.http.get<any>(`${ environment.apiUrl }${ EVENT_GET_URL }/${ id }`);
  }

  create(payload: EventModel) {
    return this.http.post<any>(`${ environment.apiUrl }${ EVENT_CREATE_URL }`, payload);
  }

  update(payload: EventModel){
    return this.http.put<any>(`${ environment.apiUrl }${ EVENT_UPDATE_URL }`, payload);
  }

  delete(id: string){
    return this.http.delete<any>(`${ environment.apiUrl }${ EVENT_DELETE_URL }/${ id }`, {});
  }

}
