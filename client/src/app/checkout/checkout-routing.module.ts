import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import {CheckoutComponent} from "./checkout.component";
import { StepperComponent } from '../shared/components/stepper/stepper.component';

const routes:Routes=
  [
  { path:'order', component: CheckoutComponent }
 ];

@NgModule({
  declarations: [  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports:[RouterModule]
})
export class CheckoutRoutingModule { }
