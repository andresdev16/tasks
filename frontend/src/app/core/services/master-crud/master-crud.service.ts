import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICrudder, IGetQueryParams } from './../../interfaces/icrudder';
import { environment } from '../../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class MasterCrudService {
  private readonly baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) {}

  getCrudder(uri: string, uriComplement: string): ICrudder {
    if (!uri) {
      return {} as ICrudder;
    }

    const url = `${this.baseUrl}${uri}`;

    return {
      get: (offset: number, limit: number) => {
        return this.http.get(`${url}/all/${offset}:${limit}`);
      },

      post: (body, params?, customHeaders?) => {
        const requestOptions = {
          params,
        };
        return this.http.post(`${url}${uriComplement}`, body, requestOptions);
      },

      put: (body) => this.http.put(`${url}`, body),

      delete: (registryID) => this.http.delete(`${url}/${registryID}`),
    };
  }

  getDependency(path: string): Observable<any> {
    const url = `${this.baseUrl}${path}`;
    return this.http.get(url);
  }

  put(path: string, body: {} = {}): Observable<any> {
    const url = `${this.baseUrl}${path}`;
    return this.http.put(url, body);
  }
}
