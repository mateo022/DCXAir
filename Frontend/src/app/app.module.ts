import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { BasicLayoutComponent } from './layouts/basic-layout/basic-layout.component';
import { FlightModule } from './flight/flight.module';
import { HttpClientModule } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { SharedModule } from './shared/shared.module';



@NgModule({
  declarations: [
      AppComponent,
      BasicLayoutComponent
  ],
  providers: [
  
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent],
  imports: [
      BrowserModule,
      AppRoutingModule,
      FlightModule,
      HttpClientModule,
      SharedModule
  ]
})
export class AppModule { }