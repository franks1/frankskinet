import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/producttype';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', { static: false }) searchTerm!: ElementRef
  products: IProduct[] = [];
  brands: IBrand[] = [];
  producttypes: IType[] = [];
  totalCount!: number;
  // brandIdSelected: number=0;
  // typeIdSelected: number=0;
  // sortSelected='name';

  shopParams = new ShopParams();

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];


  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getProductTypes();
  }


  private getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.count;
      console.log(response);
    }, (e) => { console.log(e); });
  }

  private getBrands() {
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{ id: 0, name: 'All' }, ...response];
    }, (e) => { console.log(e); });
  }


  private getProductTypes() {
    this.shopService.getProductTypes().subscribe(response => {
      this.producttypes = [{ id: 0, name: 'All' }, ...response];
    }, (e) => { console.log(e); });
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber=1;
    this.getProducts()
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber=1;
    this.getProducts()
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }


  onPageChanged(event: any) {
    if(this.shopParams.pageNumber !== event){
    this.shopParams.pageNumber = event;
    this.getProducts();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = undefined;
    this.shopParams=new ShopParams();
    this.getProducts();
  }


}
