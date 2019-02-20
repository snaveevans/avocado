import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EventForm } from "@avocado/events/models/EventForm";
import { EventModel } from "@avocado/events/models/EventModel";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class EventService {
  private url = `api/events`;
  constructor(private http: HttpClient) {}

  public getEvents(): Observable<EventModel[]> {
    return this.http.get<EventModel[]>(this.url);
  }

  public getEvent(id: string): Observable<EventModel> {
    const url = `${this.url}/${id}`;
    return this.http.get<EventModel>(url);
  }

  public create(form: EventForm): Observable<EventModel> {
    return this.http.post<EventModel>(this.url, form);
  }
}
