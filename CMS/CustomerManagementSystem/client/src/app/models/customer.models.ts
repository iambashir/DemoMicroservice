export interface Customer {
  customerId: number;
  customerName: string;
  contactPerson: string;
  mobile: string;
  email: string;
  address: string;
  status: boolean;
}

export interface PagedCustomerResponse {
  totalRecords: number;
  page: number;
  pageSize: number;
  data: Customer[];
}
