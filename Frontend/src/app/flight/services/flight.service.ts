import { Injectable } from '@angular/core';
import { environment as env } from '../../../environments/environment';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { catchError, map, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FlightService {

  constructor(private _httpClient: HttpClient) { }

  getAllLocation(){
    return this._httpClient.get(`${env.url_api}/${env.api_version}/Location`, { observe: 'response' as 'response'})
    .pipe(
      catchError(error => {
        console.error('Error en la solicitud HTTP:', error);
        return throwError(error);
      }),
      map((response: HttpResponse<any>) => {
        if (response.status === 200) {
          return response.body;
        } else {
          throw new Error(`Unexpected response status: ${response.status}`);
        }
      })
    );
  }
}
