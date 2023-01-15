import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { EnvironmentUrlService } from './environment-url.service';
import { Subject } from 'rxjs';
import { AuthResponse } from 'src/app/_interfaces/auth-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();

  constructor(
    private http: HttpClient,
    private environmentUrl: EnvironmentUrlService) { }

  public sendAuthStateChangeNotification(isAuthenticated: boolean) {
    this.authChangeSub.next(isAuthenticated);
  }

  public logout() {

    this.http.get(this.environmentUrl.authEndpoint + 'api/profiles/logout', {
      headers: {
        Authorization: 'Bearer ' + localStorage.getItem("token")
      }
    }).subscribe(data => {
      let a = 0;

      this.sendAuthStateChangeNotification(false);
    });

    localStorage.removeItem("token");
  }

  public getAccessToken(code: string) {
    const payload = new HttpParams()
      .append('grant_type', 'authorization_code')
      .append('code', code)
      .append('redirect_uri', this.environmentUrl.thisEndpoint + 'oauth/callback')
      .append('client_id', 'client-id-code')
      .append('client_secret', 'client-secret-code');
    this.http.post(this.environmentUrl.authEndpoint + 'connect/token', payload, {
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded'
      }
    }).subscribe(data => {
      var authResponse = data as AuthResponse;

      localStorage.setItem('token', authResponse.access_token);

      this.sendAuthStateChangeNotification(true);
    });
  }

  public goToLoginPage() {
    const params = [
      'response_type=code',
      'client_id=client-id-code',
      'client_secret=client-secret-code',
      'redirect_uri=' + encodeURIComponent(this.environmentUrl.thisEndpoint + 'oauth/callback'),
      'scope=api',
      'state=1234',
    ];

    window.location.href = this.environmentUrl.authEndpoint + 'connect/authorize?' + params.join('&');
  }
}
