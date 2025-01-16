import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { StoreService } from './store.service';
import { Product } from '../shared/models/product';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { StoreParams } from '../shared/models/storeParams';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrl: './store.component.scss',
})
export class StoreComponent implements OnInit {
  // ViewChild act as a template reference variable.
  // It is used to get the reference of the element in the template.
  @ViewChild('search') searchTerm?: ElementRef;
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  totalCount = 0;
  storeParams = new StoreParams();
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Ascending', value: 'priceAsc' },
    { name: 'Price: Descending', value: 'priceDesc' },
  ];

  constructor(private storeService: StoreService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(): void {
    this.storeService.getProducts(this.storeParams).subscribe({
      next: (response) => {
        this.products = response.data;
        this.totalCount = response.count;
        this.storeParams.pageNumber = response.pageIndex;
        this.storeParams.pageSize = response.pageSize;
      },
      error: (error) => console.error(error),
      complete: () => console.log('Catalog API request completed'),
    });
  }

  getBrands(): void {
    this.storeService.getBrands().subscribe({
      next: (response) =>
        (this.brands = [{ id: '', name: 'All' }, ...response]),
      error: (error) => console.error(error),
      complete: () => console.log('Catalog API request completed'),
    });
  }

  getTypes(): void {
    this.storeService.getTypes().subscribe({
      next: (response) => (this.types = [{ id: '', name: 'All' }, ...response]),
      error: (error) => console.error(error),
      complete: () => console.log('Catalog API request completed'),
    });
  }

  onTypeSelected(typeId: string) {
    this.storeParams.typeId = typeId;
    this.getProducts();
  }
  onBrandSelected(brandId: string) {
    this.storeParams.brandId = brandId;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.storeParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: PageChangedEvent) {
    this.storeParams.pageNumber = event.page;
    this.getProducts();
  }

  onSearch() {
    this.storeParams.search = this.searchTerm?.nativeElement.value;
    this.storeParams.pageNumber = 1;
    this.getProducts();
  }

  onReset() {
    if (this.searchTerm) {
      this.searchTerm.nativeElement.value = '';
      this.storeParams = new StoreParams();
      this.getProducts();
    }
  }
}
