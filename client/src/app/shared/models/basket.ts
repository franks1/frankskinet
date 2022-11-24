//import { v4 as uuidv4 } from 'uuid/v4';
//import * as uuid from "uuid";
import {UUID} from 'angular2-uuid';

export interface IBasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  type: string;
}

export interface IBasket {
  id: string;
  items: IBasketItem[];
}

export class Basket implements IBasket
{
  id = UUID.UUID();
  items: IBasketItem[] = [];
}
export interface IBasketTotal
{
  shipping: number;
  subtotal: number;
  total: number;
}
