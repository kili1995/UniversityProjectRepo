import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  setSessionStorage(key: string, value: any){
    sessionStorage.setItem(key, value);
  }

  getSessionStorageValue(key: string){
    return sessionStorage.getItem(key);
  }  

  removeSessionStorage(key:string){
    sessionStorage.removeItem(key);
  }
}
