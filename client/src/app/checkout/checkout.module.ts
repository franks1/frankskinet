import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import {CheckoutRoutingModule} from "./checkout-routing.module";
import {SharedModule} from "../shared/shared.module";
import {NgModule} from "@angular/core";
import { StepperComponent } from '../shared/components/stepper/stepper.component';
import { CheckoutAddressComponent } from './checkout-address/checkout-address.component';
import { CheckoutDeliveryComponent } from './checkout-delivery/checkout-delivery.component';
import { CheckoutReviewComponent } from './checkout-review/checkout-review.component';
import { CheckoutPaymentsComponent } from './checkout-payments/checkout-payments.component';
@NgModule({
  declarations: [
    CheckoutComponent,
    CheckoutAddressComponent,
    CheckoutDeliveryComponent,
    CheckoutReviewComponent,
    CheckoutPaymentsComponent
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    SharedModule,
  ],
  exports:[]
})
export class CheckoutModule { }
