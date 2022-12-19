import {CommonModule} from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import {PagingHeaderComponent} from './components/paging-header/paging-header.component';
import {PagerComponent} from './components/pager/pager.component'
import {CarouselModule} from "ngx-bootstrap/carousel";
import {OrderTotalsComponent} from './components/order-totals/order-totals.component';
import {BsDropdownModule} from "ngx-bootstrap/dropdown";
import {TextInputComponent} from './components/text-input/text-input.component';
import {CdkStepperModule} from "@angular/cdk/stepper";
import {StepperComponent} from './components/stepper/stepper.component';
import { ReactiveFormsModule } from '@angular/forms';
import {NgModule} from "@angular/core";
import { RouterModule } from '@angular/router';
import { BasketSummaryComponent } from './components/basket-summary/basket-summary.component';
@NgModule({
  declarations: [
    PagingHeaderComponent, PagingHeaderComponent, PagerComponent,
    OrderTotalsComponent, TextInputComponent, StepperComponent, BasketSummaryComponent
  ],
  imports: [
    CdkStepperModule,
    CommonModule,
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    ReactiveFormsModule,RouterModule
  ],
  exports: [
    CdkStepperModule,
    PaginationModule, PagingHeaderComponent, PagerComponent, OrderTotalsComponent,
    TextInputComponent,BasketSummaryComponent,
    ReactiveFormsModule, BsDropdownModule, StepperComponent]
})
export class SharedModule {


}
