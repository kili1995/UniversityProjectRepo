import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { MenuItem } from 'src/app/types/MenuItem.type';
import { MenuIcons } from 'src/app/types/MenuIcons.Enum';
import { AppRoutes } from 'src/app/routes/AppRoutes';
import { StorageService } from 'src/app/services/storage/storage.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {

  menuList: MenuItem[] =[
    {
      text: "Students",
      route: AppRoutes.STUDENTS,
      icon: MenuIcons.STUDENTS
    },
    {
      text: "Courses",
      route: AppRoutes.COURSES,
      icon: MenuIcons.COURSES
    },
    {
      text: "Categories",
      route: AppRoutes.CATEGORIES,
      icon: MenuIcons.CATEGORIES
    },
    {
      text: "Logout",
      route: AppRoutes.LOGOUT,
      icon: MenuIcons.LOGOUT
    }
  ];

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(
    private breakpointObserver: BreakpointObserver,
    private _storageService: StorageService
  ) {}
}
