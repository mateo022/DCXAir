import { Injectable } from '@angular/core';
import { environment as env } from '../../../environments/environment';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { catchError, map, throwError } from 'rxjs';
import { FlightInformationRequest } from '../models/flight.model';

@Injectable({
  providedIn: 'root'
})
export class FlightService {

  constructor(private _httpClient: HttpClient) { }

  getAllLocations(){
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

  getAllCurrencies(){
    return this._httpClient.get(`${env.url_api}/${env.api_version}/Currency`, { observe: 'response' as 'response'})
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

  getJourney(FlightInfo: FlightInformationRequest){
    let params = new HttpParams()
    .set('origin', FlightInfo.origin)
    .set('destination', FlightInfo.destination)
    .set('isOneWay', FlightInfo.isOneWay)
    .set('currency', FlightInfo.currency);
  
  // Realiza la petición HTTP con los parámetros de consulta
  return this._httpClient.get(`${env.url_api}/${env.api_version}/Journey`, {
    observe: 'response',
    params: params,
  })
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
