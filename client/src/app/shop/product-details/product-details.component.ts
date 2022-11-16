import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product!: IProduct;
   point: number=0;

  constructor(private service: ShopService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    var id_params = this.activatedRoute.snapshot.paramMap.get('id');

    if (id_params) {
        this.point= Number.parseInt(id_params); 

      this.loadProduct(this.point);
    }
  }

  loadProduct(id: number) {
    if (id === 0)
      return;

    this.service.getProduct(id).subscribe((next) => {
      this.product = next;
    }, (error) => {
      console.log(error);

    });
  }
}