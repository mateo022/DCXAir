import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchRouteComponent } from './components/search-route/search-route.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlightRoutingModule } from './flight-routing.module';



@NgModule({
  declarations: [
    SearchRouteComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FlightRoutingModule

  ]
})
export class FlightModule { }
