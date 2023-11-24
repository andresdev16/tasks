import { Observable } from 'rxjs';

export interface ICrudder {
  get: (
    ffset: number,
    limit: number,
    observeResponse?: boolean
  ) => Observable<any>;
  post: (body: any, params?: any, customHeaders?: any) => Observable<any>;
  put: (body: any) => Observable<any>;
  delete: (id: any) => Observable<any>;
}

export interface IGetQueryParams {
  pagination?: { size: number; currentPage: number };
  filter?: any;
  query?: any;
}
