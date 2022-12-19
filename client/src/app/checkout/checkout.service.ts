import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { DeliveryMethod } from '../shared/models/deliverymethod';
@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseUrl:string=environment.baseUrl;
  constructor(private https:HttpClient) { }
  getDeliveryMedthods()
  {
    return this.https.get<DeliveryMethod[]>(this.baseUrl+'order/deliveryMethods')
      .pipe(
        map((item:DeliveryMethod[])=>{
          return item.sort((a,b)=>b.price-a.price);
        })
      )
  }
}
