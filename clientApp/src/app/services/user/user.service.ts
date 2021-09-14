import { UserMasterDataModel } from './../../models/user/user.master.data';
import { DropDownModel } from './../../models/common/drop-down.model';
import { ResponseModel } from './../../models/common/response.model';
import { environment } from './../../../environments/environment';
import { UserModel } from './../../models/user/user.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }
  
  //get All user service
  getAll(): Observable<UserModel[]>{
    return this.httpClient.
      get<UserModel[]>
        (environment.apiUrl + 'User/getAllUsers')
  }

  //Save user service
  saveUser(vm: UserModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>
        (environment.apiUrl + 'User', vm);
  }

  getClassMasterData(): Observable<UserMasterDataModel> {
    return this.httpClient
    .get<UserMasterDataModel>(environment.apiUrl + "User/getUserMasterData");
  }

  ///get user by id Service
  getUserById(id:number): Observable<UserModel>{
    return this.httpClient.get<UserModel>
        (environment.apiUrl + 'User/GetUserById/'+ id);
  }

  //get user Roles Service 
  getAllRoles(): Observable<DropDownModel[]>{
    return this.httpClient.
      get<DropDownModel[]>
        (environment.apiUrl + 'User/getAllRoles')
  }

  //User Delete Service
  delete(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>
        (environment.apiUrl + 'User/' + id);
  }
}
