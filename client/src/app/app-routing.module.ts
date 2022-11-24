import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './home/home.component';
import {TestErrorComponent} from './core/test-error/test-error.component';
import {NotFoundComponent} from './core/not-found/not-found.component';
import {ServerErrorComponent} from './core/server-error/server-error.component';

const routes: Routes = [
  {
    path: '', component: HomeComponent,
    data: {title: 'Home', breadcrumb: [{label: 'Home', url: '/home'}]},
  },
  {
    path: 'test-error', component: TestErrorComponent,
    data: {title: 'Test', breadcrumb: [{label: 'TestError', url: '/test-error'}]},
  },
  {
    path: 'server-error', component: ServerErrorComponent,
    data: {title: 'Server Error', breadcrumb: [{label: 'ServeError', url: '/server-error'}]},
  },
  {
    path: 'not-found', component: NotFoundComponent,
    data: {title: 'Not found', breadcrumb: [{label: 'NotFound', url: '/not-found'}]},
  },
  {
    path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule),
    data: {title: 'Shop', breadcrumb: [{label: 'Shop', url: '/shop'}]},
  },
  {
    path: 'basket', loadChildren: () => import('../app/basket/basket.module').then(a => a.BasketModule),
    data: {title: 'Basket', breadcrumb: [{label: 'Basket', url: '/basket'}]}
  },
  {
    path: 'checkout', loadChildren: () => import('./checkout/checkout.module').then((_) => _.CheckoutModule)
  },

  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
