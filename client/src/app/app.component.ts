import {Component, OnInit} from '@angular/core';
import {BasketService} from "./basket/basket.service";
import {AccountService} from "./account/account.service";
import {IUser, User} from "./shared/models/user";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';

  constructor(private basketService: BasketService, private accountService: AccountService) {
  }

  ngOnInit() {
    this.loadBasket();
    this.loadUser();
  }

  loadUser  ()
  {
    var token = localStorage.getItem('token');
    let builder  = {token: token};

    if(builder.token)
    {
      this.accountService.loadCurrentUser(builder.token!)
        .subscribe((user: IUser) => {
            console.log(user);
          },
          (error)=> {
          console.log(error)
        });
    }
    else
    {
      var _user=new User();
      _user.displayname='';_user.token='';
      this.accountService.setDefaultUser(_user);
    }

  }

  loadBasket = () => {
    var basketId = localStorage.getItem('basket_id');
    if (basketId !== null) {
      var id = basketId.toString();
      var record = this.basketService.getBasket(id);
      record.subscribe(() => {
        console.log('Initiated');
      }, error => console.log(error));
    }
  }

}
