import { Routes } from "@angular/router";
import { AuthGuard } from "../guards/auth/auth.guard";
import { CategoriesPageComponent } from "../pages/categories-page/categories-page.component";
import { CoursesPageComponent } from "../pages/courses-page/courses-page.component";
import { LoginPageComponent } from "../pages/login-page/login-page.component";
import { RegisterPageComponent } from "../pages/register-page/register-page.component";
import { StudentsPageComponent } from "../pages/students-page/students-page.component";

export const ApplicationRoutes: Routes = [
    {
      path: '',
      redirectTo: 'login',
      pathMatch: 'full'
    },
    {
      path: 'login',
      component: LoginPageComponent
    },
    {
      path: 'register',
      component: RegisterPageComponent
    },
    {
      path:'students',
      component: StudentsPageComponent,
      canActivate: [AuthGuard]
    },
    {
      path:'courses',
      component: CoursesPageComponent,
      canActivate: [AuthGuard]
    },
    {
      path:'categories',
      component: CategoriesPageComponent,
      canActivate: [AuthGuard]
    },
  ];