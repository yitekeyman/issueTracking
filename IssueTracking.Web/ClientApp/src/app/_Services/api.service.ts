import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from "@angular/router";
import {configs} from "../app-config";
import {Observable, ReplaySubject, throwError} from "rxjs";
import { catchError } from 'rxjs/operators';

export class ErrorMsg {
  status: number;
  msg: string;
  title: string;
}

@Injectable()
export class ApiServices {
  public cookie: string;
  public base_url = configs.url;
  public KEY_NAME = '.ASPNetCoreSession';

  public headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Accept': 'application/json',
    'Access-Control-Allow-Origin':'*'
  });


  public static apiEvent = new ReplaySubject(1);

  constructor(public _http: HttpClient, public router: Router) {
  }


  public get(path: string): Observable<any> {
    return this._http.get(`${this.base_url}${path}`, {withCredentials: true, headers: this.headers}).pipe(catchError(ApiServices.handleError));
  }

  public post(path: string, body: any): Observable<any> {
    return this._http.post(`${this.base_url}${path}`, body, {withCredentials: true, headers: this.headers}).pipe(catchError(ApiServices.handleError));
  }

  public put(path: string, body: any): Observable<any> {
    return this._http.put(`${this.base_url}${path}`, body, {withCredentials: true, headers: this.headers}).pipe(catchError(ApiServices.handleError));
  }

  public delete(path: string): Observable<any> {
    return this._http.delete(`${this.base_url}${path}`, {withCredentials: true, headers: this.headers}).pipe(catchError(ApiServices.handleError));
  }

  public patch(path: string, body: any): Observable<any> {
    return this._http.patch(`${this.base_url}${path}`, body, {withCredentials: true, headers: this.headers}).pipe(catchError(ApiServices.handleError));
  }

  public postFormData(path: string, form_data: FormData): Observable<any> {
    const formDataHeaders = new HttpHeaders();
    formDataHeaders.set('enctype', 'multipart/form-data');
    formDataHeaders.set('Accept', 'application/json');
    formDataHeaders.set('Authorization', 'Bearer ' + window.localStorage.getItem(this.KEY_NAME));
    formDataHeaders.set('Access-Control-Allow-Headers', '*');

    return this._http.post(`${this.base_url}${path}`, form_data, {headers: formDataHeaders}).pipe(catchError(ApiServices.handleError));
  }


  public static handleError(response: any) {
    const err = {status: response.status || 0, message: response.error && response.error.message || ''};
    console.log(response);

    let errMsg = new ErrorMsg();


    if (response.ok) {
      return response;

    } else if (response.status === 0) {
      errMsg = {status: response.status, msg: response.message, title: 'Network Error'};
      ApiServices.apiEvent.next(errMsg);
      // this.logout();
    } else if (response.status === 422) {
      //  ApiService.apiEvent.next({msg:"Un-processable Entity!", title:"Validation Error"});
      // let the component handle the error
    } else if (response.status === 400) {
      errMsg = {status: response.status, msg: 'Your session expired, Please login again', title: 'Session Expired'};
      ApiServices.apiEvent.next(errMsg);
      // this.logout();
    } else if (response.status === 401) {
      errMsg = {status: response.status, msg: err.message, title: 'Unauthorized'};
      ApiServices.apiEvent.next(errMsg);
    } else if (response.status === 404) {
      errMsg = {status: response.status, msg: 'File Not found!', title: ' Not Found'};
      ApiServices.apiEvent.next(errMsg);
    }  else if (response.status === 500) {
      errMsg = {status: response.status, msg: err.message, title: 'Server Error'};
      ApiServices.apiEvent.next(errMsg);

    } else if (response.status == 403) {
      errMsg = {status: response.status, msg: 'You are unauthorized for this action', title: 'Forbidden'};
      ApiServices.apiEvent.next(errMsg);

    }

    return throwError(err);
  }

}
