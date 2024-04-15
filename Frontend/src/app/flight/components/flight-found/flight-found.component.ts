import { Component, OnInit } from '@angular/core';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-flight-found',
  templateUrl: './flight-found.component.html',
  styleUrl: './flight-found.component.css'
})
export class FlightFoundComponent implements OnInit{

  constructor(private _dataService: DataService){}

  ngOnInit() {
    const data = this._dataService.getData();
    console.log(data); // Aquí tienes los datos para usar en tu componente
  }
}
