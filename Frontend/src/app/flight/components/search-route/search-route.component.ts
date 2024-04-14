import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { FlightService } from '../../services/flight.service';
import { SnackBarService } from '../../../shared/services/snackbar.service';

@Component({
  selector: 'app-search-route',
  templateUrl: './search-route.component.html',
  styleUrls: ['./search-route.component.css']
})
export class SearchRouteComponent implements OnInit {
  form: FormGroup;

  cities: string[] = [];
  currencies: string[] = [];

  readonly getLocationInformationObserver = {
    next: (data: any[]) => this.getLocationsNext(data),
    error: (errorCode: number) => this.getLocationsError(errorCode),
  };

  readonly getCurrencyInformationObserver = {
    next: (data: any[]) => this.getCurrenciesNext(data),
    error: (errorCode: number) => this.getCurrenciesError(errorCode),
  };

  constructor(
    private fb: FormBuilder,
    private _flightService: FlightService,
    private _snackbarService: SnackBarService
  ) {  
    
    this._flightService.getAllLocations()
    .subscribe(this.getLocationInformationObserver);

    this._flightService.getAllCurrencies()
    .subscribe(this.getCurrencyInformationObserver);
    }

  ngOnInit(): void {
   
    this.form = this.fb.group({
      origin: ['', Validators.required],
      destination: ['', Validators.required],
      isOneWay: [true],
      currency: ['USD', Validators.required]
    });

    this.form.get('destination')?.setValidators([
      Validators.required,
      this.differentFromOrigin.bind(this)
    ]);

    this.form.get('origin')?.valueChanges.subscribe((newOrigin) => {
      const destinationControl = this.form.get('destination');
      if (destinationControl?.value === newOrigin) {
        destinationControl?.setValue(null);
      } else {
        destinationControl?.updateValueAndValidity();
      }
    });

    this.form.get('destination')?.valueChanges.subscribe((newDestination) => {
      const originControl = this.form.get('origin');
      if (originControl?.value === newDestination) {
        originControl?.setValue(null);
      } else {
        originControl?.updateValueAndValidity();
      }
    });
  }

  differentFromOrigin(control: AbstractControl): { sameOriginDestination: boolean } | null {
    const originControl = this.form.get('origin');
    const origin = originControl ? originControl.value : null;
    if (control.value === origin) {
        return { sameOriginDestination: true };
    }
    return null;
}

  onSubmit() {
    if (this.form.valid) {
      const formData = this.form.value;
      console.log('Form data:', formData);
    }
  }


  getLocationsNext(data: any[]) {
    this.cities = data.map((location: { nameLocation: string }) => location.nameLocation);
  }

  getLocationsError(errorCode: number) {
    if (errorCode == 404) {
      this._snackbarService.openSnackBar("No se encontraron ciudades disponibles.");
    }
  }


  getCurrenciesNext(data: any[]) {
    this.currencies = data.map((location: { nameCurrency: string }) => location.nameCurrency);
  }

  getCurrenciesError(errorCode: number) {
    if (errorCode == 404) {
      this._snackbarService.openSnackBar("No se encontraron monedas disponibles.");
    }
  }

}
