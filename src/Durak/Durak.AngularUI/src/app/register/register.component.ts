import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IOperationResult } from '../models/operation-result.model';

import { AlertService } from '../_alert/alert.service';
import { AppSettings } from '../app.settings';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(private _http: HttpClient, private _alertService: AlertService) { }

  hidePassword: boolean = true;

  registerForm = new FormGroup({
    firstName: new FormControl(),
    lastName: new FormControl(),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required]),
  });

  register(): void {
    this._http.post(AppSettings.AUTH_ENDPOINT + 'profiles/register', this.registerForm.value).subscribe({
      next: (data) => {
        var operation = data as IOperationResult;

        if (!operation.ok) {
          this._alertService.error(operation.metadata.message);
          return;
        }

        this._alertService.success('Пользователь успешно зарегистрирован');
      },
      error: (error) => console.error(error)
    });
  }
}
