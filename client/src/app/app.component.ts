import {Component, OnInit} from '@angular/core';
import {BasketService} from "./basket/basket.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  constructor(private basketService: BasketService) { }

  ngOnInit() {
    var basketId = localStorage.getItem('basket_id');
    if (basketId !==null) {
      var id =basketId.toString();
      var record = this.basketService.getBasket(id);
      record.subscribe(() => {
        console.log('Initiated');
      }, error => console.log(error));
    }

  }
}
