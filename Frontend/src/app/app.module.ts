import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { BasicLayoutComponent } from './layouts/basic-layout/basic-layout.component';
import { FlightModule } from './flight/flight.module';



@NgModule({
  declarations: [
      AppComponent,
      BasicLayoutComponent
  ],
  providers: [
  ],
  bootstrap: [AppComponent],
  imports: [
      BrowserModule,
      AppRoutingModule,
      FlightModule
  ]
})
export class AppModule { }