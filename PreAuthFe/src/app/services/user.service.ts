import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface UserData {
  id: string;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
}

export interface UpdateUserData {
  firstName?: string;
  lastName?: string;
  email?: string;
  role?: string;
}

export interface PaginatedUsers {
  items: UserData[];
  totalCount: number;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsers(pageIndex: number, pageSize: number): Observable<PaginatedUsers> {
    let params = new HttpParams()
      .set('pageIndex', pageIndex.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<PaginatedUsers>(`${this.apiUrl}/user`, { params });
  }

  deleteUser(userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/user/${userId}`);
  }

  updateUser(userId: string, data: UpdateUserData): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/user/${userId}`, data);
  }

}
