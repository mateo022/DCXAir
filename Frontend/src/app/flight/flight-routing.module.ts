import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SearchRouteComponent } from './components/search-route/search-route.component';
import { FlightFoundComponent } from './components/flight-found/flight-found.component';


const routes: Routes = [
  {
    path: 'search',
    children: [
      {path: '', component: SearchRouteComponent, title: 'Flight'},
      {path: 'flights', component: FlightFoundComponent, title: 'Flight Found'},
    { path: '', redirectTo: '', pathMatch: 'full' },

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FlightRoutingModule { }
