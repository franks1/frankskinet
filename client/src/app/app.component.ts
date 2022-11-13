import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from 'src/models/pagination';
import { IProduct } from '../models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  products: IProduct[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<IPagination>('https://localhost:7047/api/products/?pageSize=50').subscribe(
      (next) => {
        this.products = next.data;
      },
      error => {  console.log(error);   });
  }
}
