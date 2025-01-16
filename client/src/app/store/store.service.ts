import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { StoreParams } from '../shared/models/storeParams';

@Injectable({
  providedIn: 'root',
})
export class StoreService {
  baseUrl = 'http://localhost:8000/api/v1/';

  constructor(private http: HttpClient) {}

  getProductById(id: string) {
    return this.http.get<Product>(
      this.baseUrl + 'Catalog/GetProductById/' + id
    );
  }

  getProducts(storeParams: StoreParams) {
    let params = new HttpParams();

    if (storeParams.brandId) {
      params = params.append('brandId', storeParams.brandId.toString());
    }
    if (storeParams.typeId) {
      params = params.append('typeId', storeParams.typeId.toString());
    }
    if (storeParams.search) {
      params = params.append('search', storeParams.search);
    }
    params = params.append('sort', storeParams.sort);
    params = params.append('pageIndex', storeParams.pageNumber.toString());
    params = params.append('pageSize', storeParams.pageSize.toString());
    return this.http.get<Pagination<Product[]>>(
      this.baseUrl + 'Catalog/GetProducts',
      { params }
    );
  }

  getBrands() {
    return this.http.get<Brand[]>(this.baseUrl + 'Catalog/GetBrands');
  }

  getTypes() {
    return this.http.get<Type[]>(this.baseUrl + 'Catalog/GetTypes');
  }
}
