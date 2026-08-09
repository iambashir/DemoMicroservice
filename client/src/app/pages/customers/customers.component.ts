import { Component, OnInit } from '@angular/core';
import { Customer, CustomerService } from '../../core/customer.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html'
})
export class CustomersComponent implements OnInit {
  customers: Customer[] = [];
  page = 1;
  pageSize = 10;
  totalRecords = 0;
  search = '';
  loading = false;
  error = '';

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    this.load(true);
  }

  load(reset = false): void {
    if (reset) {
      this.page = 1;
      this.customers = [];
    }

    this.loading = true;
    this.error = '';

    this.customerService.getCustomers(this.page, this.pageSize, this.search).subscribe({
      next: response => {
        this.customers = reset ? response.data : [...this.customers, ...response.data];
        this.totalRecords = response.totalRecords;
        this.loading = false;
      },
      error: err => {
        this.loading = false;
        this.error = err.status === 401 ? 'Token invalid বা expired হয়েছে। আবার login করুন।' : 'Customer data load করা যায়নি।';
      }
    });
  }

  loadMore(): void {
    this.page += 1;
    this.load(false);
  }

  hasMore(): boolean {
    return this.customers.length < this.totalRecords;
  }
}
