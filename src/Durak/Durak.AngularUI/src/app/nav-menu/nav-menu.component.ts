import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public isLoggedIn: Observable<boolean> = this.authService.authChanged;

  constructor(private authService: AuthService) { }

  public isExpanded: boolean = false;

  public collapse() {
    this.isExpanded = false;
  }

  public toggle() {
    this.isExpanded = !this.isExpanded;
  }

  public logout() {
    this.authService.logout();
  }
}
