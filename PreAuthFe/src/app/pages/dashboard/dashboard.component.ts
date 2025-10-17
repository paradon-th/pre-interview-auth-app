import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { UserData, UserService } from '../../services/user.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '../../services/auth.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EditUserDialogComponent } from '../../components/edit-user-dialog/edit-user-dialog.component';
import { UpdateUserData } from '../../services/user.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  userList: UserData[] = [];
  isLoading = true;
  isAdmin = false;
  displayedColumns: string[] = ['username', 'email', 'firstName', 'lastName', 'role'];

  constructor(
    private userService: UserService,
    private authService: AuthService,
    public dialog: MatDialog,
    private notification: NotificationService
  ) {}

  ngOnInit(): void {
    const userRole = this.authService.getUserRole();
    this.isAdmin = userRole === 'Admin';
    if (this.isAdmin) {
      this.displayedColumns.push('actions');
    }

    this.loadUsers();
  }

  loadUsers(): void {
    this.isLoading = true;
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.userList = data;
        this.isLoading = false;
      },
      error: (err) => {
        // console.error('Failed to load users', err);
        this.isLoading = false;
      }
    });

  }

  editUser(user: UserData): void {
    const dialogRef = this.dialog.open(EditUserDialogComponent, {
      width: '400px',
      height: '500px',
      data: { user: user }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const updatedData: UpdateUserData = result;
        this.userService.updateUser(user.id, updatedData).subscribe({
          next: () => {
            this.notification.showSuccess('แก้ไขข้อมูลผู้ใช้สำเร็จ');
            this.loadUsers(); 
          },
          error: (err) => {
            const errorMessage = err.error?.message || 'เกิดข้อผิดพลาดที่ไม่คาดคิด';
            this.notification.showError(errorMessage);
          }
        });
      }
    });
  }

  deleteUser(userId: string): void {
    if (confirm('คุณต้องการลบผู้ใช้นี้?')) {
      this.userService.deleteUser(userId).subscribe({
        next: () => {
          this.notification.showSuccess('ลบผู้ใช้สำเร็จ');
          this.loadUsers(); 
        },
        error: (err) => {
          const errorMessage = err.error?.message || 'เกิดข้อผิดพลาดที่ไม่คาดคิด';
          this.notification.showError(errorMessage);
        }
      });
    }
  }

}
