import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BASE_URL, GOOGLE_LOGIN} from "../../dto/constants";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  loginWithGoogle() {
    return this.http.get(BASE_URL + GOOGLE_LOGIN);
  }
}
