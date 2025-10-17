import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { NotificationService } from '../services/notification.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const notification = inject(NotificationService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = 'เกิดข้อผิดพลาดที่ไม่คาดคิด';

      if (error.status === 0) {
        errorMessage = 'ไม่สามารถเชื่อมต่อกับเซิร์ฟเวอร์ได้';
      } else {
        errorMessage = error.error?.message || error.error?.title || 'เกิดข้อผิดพลาดบางอย่างในระบบ';
      }
      notification.showError(errorMessage);
      return throwError(() => error);
    })
  );
};
