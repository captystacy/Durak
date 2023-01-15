import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { OperationResult } from '../_interfaces/operation-result.model';

import { AlertService } from '../shared/services/alert.service';
import { EnvironmentUrlService } from '../shared/services/environment-url.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(
    private http: HttpClient, 
    private alertService: AlertService,
    private environmentUrl: EnvironmentUrlService
    ) { }

  hidePassword: boolean = true;

  registerForm = new FormGroup({
    firstName: new FormControl(),
    lastName: new FormControl(),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required]),
  });

  register(): void {
    this.http.post(this.environmentUrl.authEndpoint + 'api/profiles/register', this.registerForm.value).subscribe({
      next: (data) => {
        var operation = data as OperationResult;

        if (!operation.ok) {
          this.alertService.error(operation.metadata.message);
          return;
        }

        this.alertService.success('Пользователь успешно зарегистрирован');
      },
      error: (error) => console.error(error)
    });
  }
}
