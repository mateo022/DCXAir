import { Component, OnInit } from '@angular/core';
import { DataService } from '../../services/data.service';
import { Journey } from '../../models/flight.model';

@Component({
  selector: 'app-flight-found',
  templateUrl: './flight-found.component.html',
  styleUrl: './flight-found.component.css'
})
export class FlightFoundComponent implements OnInit{


  journeys: Journey[] = [];

  constructor(private _dataService: DataService){}

  ngOnInit() {
    const data = this._dataService.getData();
    this.journeys = data;
    console.log(data);
  }
}
