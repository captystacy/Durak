import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentUrlService {
  public authEndpoint: string = environment.authEndpoint;
  public thisEndpoint: string = environment.thisEndpoint;
}
