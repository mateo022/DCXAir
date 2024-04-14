import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

@Component({
  selector: 'app-search-route',
  templateUrl: './search-route.component.html',
  styleUrls: ['./search-route.component.css']
})
export class SearchRouteComponent implements OnInit {
  form: FormGroup;

  cities = ['City 1', 'City 2', 'City 3'];
  currencies = ['USD', 'EUR', 'GBP'];

  constructor(private fb: FormBuilder) { }

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

  // Manejo del envío del formulario
  onSubmit() {
    if (this.form.valid) {
      const formData = this.form.value;
      console.log('Form data:', formData);
    }
  }
}
