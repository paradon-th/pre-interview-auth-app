import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from '../../services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { response } from 'express';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-register',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(
    private authService: AuthService,
    private router: Router,
    private notification: NotificationService
  ) { }

  registerForm = new FormGroup({
    firstname: new FormControl('', [Validators.required]),
    lastname: new FormControl('', [Validators.required]),
    username: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  })
  hidePassword = true;

  onSubmit() {
    if (!this.registerForm.valid) {
      return;
    }
    const data = this.registerForm.getRawValue();
    this.authService.regiser({ 
      firstname: data.firstname!, 
      lastname: data.lastname!,
      username: data.username!, 
      email: data.email!, 
      password: data.password! })
      .subscribe({
        next: (response) => {
          this.notification.showSuccess('ลงทะเบียนผู้ใช้เรียบร้อยแล้ว');
          this.router.navigate(['/login']);
        },
        error: (err) => {
          const errorMessage = err.error?.message || err.statusText || 'เกิดข้อผิดพลาดที่ไม่คาดคิด';
          this.notification.showError(errorMessage);
        }
      });
  }
}
