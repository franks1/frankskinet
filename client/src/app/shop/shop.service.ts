import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment.prod';
import { Observable } from 'rxjs';
import { IPagination } from 'src/app/shared/models/pagination';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/producttype';
import { ShopParams } from '../shared/models/shopParams';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  private url: string = environment.baseUrl;
  constructor(private http: HttpClient) { }

  getProduct(id:number):Observable<IProduct>{

     return this.http.get<IProduct>(this.url+'products/'+id);
  }


  getProducts(shopparams: ShopParams): Observable<IPagination> {
    let params = new HttpParams();
    if (shopparams.brandId !== 0) {
      params = params.append('brandId', shopparams.brandId.toString());
    }

    if (shopparams.search ) 
    {
      params = params.append('search', shopparams.search);
    }


    if (shopparams.typeId !== 0) {
      params = params.append('typeId', shopparams.typeId.toString());
    }

    params = params.append('sort', shopparams.sort);
    params = params.append('pageIndex', shopparams.pageNumber.toString());
    params = params.append('pageSize', shopparams.pageSize.toString());
    return this.http.get<IPagination>(this.url + 'products', { params });
  }

  getBrands(): Observable<IBrand[]> {
    return this.http.get<IBrand[]>(this.url + 'products/brands');
  }

  getProductTypes(): Observable<IType[]> {
    return this.http.get<IType[]>(this.url + 'products/types');
  }


}