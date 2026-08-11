import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Customer {
  customerId: number;
  customerName: string;
  contactPerson: string;
  mobile: string;
  email: string;
  address: string;
  status: boolean;
}

export interface CustomerResponse {
  totalRecords: number;
  page: number;
  pageSize: number;
  data: Customer[];
}

@Injectable({ providedIn: 'root' })
export class CustomerService {
  constructor(private http: HttpClient) {}

  getCustomers(page: number, pageSize: number, search: string): Observable<CustomerResponse> {
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize)
      .set('search', search);

    return this.http.get<CustomerResponse>(`${environment.customerApiUrl}/api/customer`, { params });
  }
}
