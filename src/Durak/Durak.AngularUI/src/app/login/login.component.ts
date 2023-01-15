import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
  ) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      let code = params['code'];

      if (code) {
        this.router.navigate([], {
          queryParams: {
            'code': null,
            'state': null,
          },
          queryParamsHandling: 'merge'
        })
        
        this.authService.getAccessToken(code);
      }
    });
  }

  public goToLoginPage() {
    this.authService.goToLoginPage();
  }
}