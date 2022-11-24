import {Component, OnInit} from '@angular/core';
import {IBasket} from "../../shared/models/basket";
import {Observable} from "rxjs";
import {BasketService} from "../../basket/basket.service";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  title: string = "Skinet";

  basket$: Observable<IBasket> = new Observable<IBasket>();

  constructor(private basketService: BasketService) {
  }

  ngOnInit(): void
  {
    this.basket$ = this.basketService.basket$;
  }

}
