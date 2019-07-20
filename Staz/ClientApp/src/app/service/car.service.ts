import { Injectable } from "@angular/core";
import { BehaviorSubject, zip, Observable } from "rxjs";
import { tap, map } from 'rxjs/operators';
import { HttpClientModule, HttpClient, HttpHeaders,HttpParams } from "@angular/common/http";
import { Car } from "../model/car";
import { Http} from '@angular/http';

@Injectable({
    providedIn: 'root'
})
export class CarService extends BehaviorSubject<any[]> {
    constructor(private http: HttpClient) {
        super([]);
    }
    private data: any[] = [];

    
      

    public getCars(): Observable<any[]> {
        return this.http
            .get<any[]>("https://localhost:44342/api/car");
    }

    public Create(car: Car) {
        return this.http
            .post("https://localhost:44342/api/car",car);
    }
    public Update(car: Car) {
        return this.http
            .put("https://localhost:44342/api/car", car);
    }
    public Delete(id: number) {
        var number = id.toString();
        return this.http
            .delete("https://localhost:44342/api/car/" + number);
    }
}