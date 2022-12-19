import {Component, OnInit} from '@angular/core';
import {CdkStepper} from "@angular/cdk/stepper";
import {StepperComponent} from "../shared/components/stepper/stepper.component";
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  checkoutForm!: FormGroup<any | null>;

  constructor(private formBuilder: FormBuilder) {

  }

  ngOnInit(): void {
    this.createCheckoutForm();
  }

  createCheckoutForm() {
    // this.checkoutForm=new FormGroup<any>({
    //   firstName: new FormControl([null,Validators.required])
    // })
    this.checkoutForm = this.formBuilder.group({
      addressForm: this.formBuilder.group({
        firstName: [null, Validators.required],
        lastName: [null, Validators.required],
        state: [null, Validators.required],
        city: [null, Validators.required],
        street: [null, Validators.required],
        zipCode: [null, Validators.required]
      }),
      deliveryForm: this.formBuilder.group({
        deliveryMethod: [null, Validators.required]
      }),
      paymentForm: this.formBuilder.group({
        nameOnCard: [null, Validators.required]
      })
    });
  }
}
