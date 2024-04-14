import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { BasicLayoutComponent } from './layouts/basic-layout/basic-layout.component';



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
  ]
})
export class AppModule { }