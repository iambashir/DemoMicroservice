import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs';
import { Customer } from '../../models/customer.models';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrl: './customers.component.scss'
})
export class CustomersComponent implements OnInit {
  customers: Customer[] = [];
  page = 1;
  readonly pageSize = 10;
  totalRecords = 0;
  search = '';
  loading = false;
  errorMessage = '';

  constructor(private readonly customerService: CustomerService) {}

  ngOnInit(): void {
    this.loadCustomers(true);
  }

  get hasMore(): boolean {
    return this.customers.length < this.totalRecords;
  }

  onSearch(): void {
    this.loadCustomers(true);
  }

  loadMore(): void {
    if (this.loading || !this.hasMore) {
      return;
    }

    this.page += 1;
    this.loadCustomers(false);
  }

  loadCustomers(reset: boolean): void {
    if (this.loading) {
      return;
    }

    const targetPage = reset ? 1 : this.page;
    if (reset) {
      this.page = 1;
      this.customers = [];
      this.totalRecords = 0;
    }

    this.loading = true;
    this.errorMessage = '';
    this.customerService.getCustomers(targetPage, this.pageSize, this.search)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: response => {
          this.totalRecords = response.totalRecords;
          this.page = response.page;
          this.customers = reset ? response.data : this.customers.concat(response.data);
        },
        error: error => {
          if (!reset) {
            this.page = Math.max(1, this.page - 1);
          }
          this.errorMessage = error.error?.message ?? 'Customer loading failed.';
        }
      });
  }
}
