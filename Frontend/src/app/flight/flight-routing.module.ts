import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SearchRouteComponent } from './components/search-route/search-route.component';


const routes: Routes = [
  {
    path: 'search',
    children: [
      {path: '', component: SearchRouteComponent, title: 'Vuelos'},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FlightRoutingModule { }
