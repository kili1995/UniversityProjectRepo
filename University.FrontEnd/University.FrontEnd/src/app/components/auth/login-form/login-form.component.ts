import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ROUTES } from '@angular/router';
import { MaterialModule } from 'src/app/modules/material-modules/material.module';
import { AuthService } from 'src/app/services/auth/auth.service';
import { StorageService } from 'src/app/services/storage/storage.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit {

  loginForm: FormGroup = this._formBuilder.group({});

  constructor(
    private _formBuilder: FormBuilder, 
    private _router:Router,
    private _authService: AuthService,
    private _storageService: StorageService) { }

  ngOnInit(): void {
    this.loginForm = this._formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });

    this._storageService.removeSessionStorage("jwtToken");
  }

  login(){
    let userName = this.loginForm.value["userName"];
    let password = this.loginForm.value["password"];
    this._authService.authUser(userName, password).subscribe({
      next: (response: any) => {
        if(!response){
          console.error("Empty response.");
          return;
        }
        if(!response.token){
          console.error("Token not found.");
          return;
        }
        let tokenInfo = response.token;
        this._storageService.setSessionStorage('jwtToken', tokenInfo.token);
        this._router.navigate(["students"]);
      },
      error: (errorResponse: any) =>{
        console.error(`Error: ${errorResponse.error}`);
        this.loginForm.reset();
        this._storageService.removeSessionStorage("jwtToken");
      },
      complete: () => {
        console.info('Authentication process finished');
        this.loginForm.reset();        
      }
    });
  }
}
