import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApplicationRoutes } from './routes/Routes';

@NgModule({
  imports: [RouterModule.forRoot(ApplicationRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
