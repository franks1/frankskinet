import {Component, OnInit} from '@angular/core';
import {IBasket} from "../../shared/models/basket";
import {Observable} from "rxjs";
import {BasketService} from "../../basket/basket.service";
import {IUser} from "../../shared/models/user";
import {AccountService} from "../../account/account.service";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  title: string = "Skinet";

  basket$: Observable<IBasket> = new Observable<IBasket>();
  currentUser$: Observable<IUser> = new Observable<IUser>();

  constructor(private basketService: BasketService,
              private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.currentUser$ = this.accountService.currentUser$;
  }

  logOut=()=>{
    this.accountService.logout();
  }

}
