import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { FlightService } from '../../services/flight.service';
import { SnackBarService } from '../../../shared/services/snackbar.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from '../../services/data.service';

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

  readonly getJorneyInformationObserver = {
    next: (data: any[]) => this.getJourneysNext(data),
    error: (errorCode: number) => this.getJourneyError(errorCode),
  };
  constructor(
    private fb: FormBuilder,
    private _flightService: FlightService,
    private _snackbarService: SnackBarService,
    private el: ElementRef, 
    private renderer: Renderer2,
    private router: Router,
    private route: ActivatedRoute,
    private _dataService: DataService
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
      isOneWay: ['', this.atLeastOneSelected],
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

  atLeastOneSelected(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    // Si el valor es 'true' o 'false', entonces es válido
    if (value === 'true' || value === 'false') {
      
        return null;
    }
    // Si el valor no es ni 'true' ni 'false', retorna un error
    return { required: true };
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

      this._flightService.getJourney(formData)
      .subscribe(this.getJorneyInformationObserver);

    }
  }

  onCircleClick(event: MouseEvent) {
    const circleElement = event.target as HTMLElement;

    // Verifica si el elemento tiene la clase 'animate'
    if (!circleElement.classList.contains('animate')) {
      // Agrega la clase 'animate' para iniciar la animación
      this.renderer.addClass(circleElement, 'animate');

      // Usa setTimeout para quitar la clase 'animate' después de 4 segundos
      setTimeout(() => {
        this.renderer.removeClass(circleElement, 'animate');
      }, 4000);
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

  getJourneysNext(data: any[]) {
    //ENVIAR A LA OTRA PAGINA
   console.log(data);
   this._dataService.setData(data);
   this.router.navigate(['flights'], { relativeTo: this.route });
   
  }

  getJourneyError(errorCode: number) {
    if (errorCode == 404) {
      this._snackbarService.openSnackBar("No se lograron encontrar vuelos disponibles.");
    }
  }

}
