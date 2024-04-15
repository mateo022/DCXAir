import { Component, OnInit } from '@angular/core';
import { DataService } from '../../services/data.service';
import { Journey } from '../../models/flight.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-flight-found',
  templateUrl: './flight-found.component.html',
  styleUrl: './flight-found.component.css'
})
export class FlightFoundComponent implements OnInit {


  journeys: Journey[] = [];
  currency: string;

  constructor(
    private _dataService: DataService,
    private router: Router) { }

  ngOnInit() {
    const data = this._dataService.getData();
    this.journeys = data;
    const currentCurrency = this._dataService.getCurrency()
    this.currency = currentCurrency
  }
  back() {
    this.router.navigate(['search']); // Usa Location para navegar al último estado de historial
  }
  formatPrice(price: number, currency: string): string {
    let symbol: string;
    // Decide el símbolo de moneda basado en el tipo de moneda
    if (currency === 'EUR') {
        symbol = '€'; // Símbolo de euros
        return `${price.toFixed(2)} ${symbol}`;
    } else {
        symbol = '$'; // Símbolo de dólares
        return `${symbol}${price.toFixed(0)}`;
    }
}
  roundPrice(price: number): number {
    return Math.round(price * 100) / 100;
  }
}
