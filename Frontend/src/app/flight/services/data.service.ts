import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }
  private data: any;
  private currency: string;
  
  setData(data: any) {
    this.data = data;
  }

  getData() {
    return this.data;
  }

  setCurrency(currency:string){
    this.currency = currency
  }

  getCurrency(){
    return this.currency;
  }
}
