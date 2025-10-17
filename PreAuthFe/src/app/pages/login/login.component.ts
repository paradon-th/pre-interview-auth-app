import { Component } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl, Validators, FormGroupDirective, NgForm } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { ErrorStateMatcher } from '@angular/material/core';
import { AuthService } from '../../services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { NotificationService } from '../../services/notification.service';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  })
  hidePassword = true;

  constructor(
    private authService: AuthService,
    private router: Router,
    private notification: NotificationService
  ) { }

  onSubmit() {
    if (!this.loginForm.valid) {
      return;
    }

    const data = this.loginForm.getRawValue();
    this.authService.login({ email: data.email!, password: data.password! })
      .subscribe({
        next: (response) => {
          this.notification.showSuccess('เข้าสู่ระบบสำเร็จ');
          localStorage.setItem('authToken', response.token);
          this.router.navigate(['/dashboard']);
        },
        error: (err) => {
          const errorMessage = err.error?.message || err.statusText || 'เกิดข้อผิดพลาดที่ไม่คาดคิด';
          this.notification.showError(errorMessage);
        }
      });
  }

  toLogin(){
    this.router.navigate(['/login']);
  }


}
