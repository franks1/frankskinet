import { Component, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment.prod';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {

  private baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

  get404(): void {
    this.http.get(this.baseUrl + 'products/101').subscribe((response) => {
      console.log(response);
    }, (error) => console.log(error));
  }

  get500(): void {
    this.http.get(this.baseUrl + 'buggy/servererror').subscribe((response) => {
      console.log(response);
    }, (error) => console.log(error));
  }

  get400(): void {
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe((response) => {
      console.log(response);
    }, (error) => console.log(error));
  }
}
