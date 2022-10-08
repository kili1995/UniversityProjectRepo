import { Component } from '@angular/core';
import { StorageService } from './services/storage/storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'University Proyect';

  constructor(private _storageService: StorageService){}

  userIsAuthenticated(){
    let tokenKey = 'jwtToken';
    let token = this._storageService.getSessionStorageValue(tokenKey);
    if(!token){
      return false;
    }
    return true;
  }
}
