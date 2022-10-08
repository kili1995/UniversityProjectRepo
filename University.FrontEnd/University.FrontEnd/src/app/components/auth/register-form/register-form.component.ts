import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { flatMap } from 'rxjs';
import { MaterialModule } from 'src/app/modules/material-modules/material.module';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss']
})
export class RegisterFormComponent implements OnInit {

  registerForm: FormGroup = new FormGroup({});
  snackBarHorizontalPosition: MatSnackBarHorizontalPosition = 'right';
  snackBarVerticalPosition: MatSnackBarVerticalPosition = 'top';
  showSpinner: boolean = false;
  
  constructor(private _formBuilder: FormBuilder,
    private _snackBar: MatSnackBar,
    private _authService: AuthService,
    private _router: Router) { }

  ngOnInit(): void {
    this.registerForm = this._formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
      repeatedPassword: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    })
  }

  createUser(){
    let {userName, password, repeatedPassword, email} = this.registerForm.value;
    if(password != repeatedPassword){
      this._snackBar.open("Passwords doesn't match", 'OK', {
        duration: 3000,
        horizontalPosition: this.snackBarHorizontalPosition,
        verticalPosition: this.snackBarVerticalPosition,
      });
      return;
    }
    this.showSpinner = true;
    this._authService.createUser(userName, password, email).subscribe({
      next: (response: any) => {
        this._snackBar.open("Usuario creado correctamente. Será redirigido al Login.", 'OK', {
          duration: 3000,
          horizontalPosition: this.snackBarHorizontalPosition,
          verticalPosition: this.snackBarVerticalPosition,
        });
        this._router.navigate(["/"]);
        this.registerForm.reset();
      },
      error: (errorResponse: any) =>{
        this._snackBar.open(errorResponse.error, 'OK', {
          duration: 3000,
          horizontalPosition: this.snackBarHorizontalPosition,
          verticalPosition: this.snackBarVerticalPosition,
        });
        this.showSpinner = false;
      },
      complete: () =>{
        this.showSpinner = false;
      }
    });
  };

}
