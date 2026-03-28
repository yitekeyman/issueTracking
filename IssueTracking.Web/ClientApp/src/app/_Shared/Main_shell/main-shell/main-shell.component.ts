import {Component, Input, OnInit} from '@angular/core';
import {Router} from '@angular/router';

import {IMainShellRoute} from '../interfaces';

import {IssueTrackingService} from "../../../_Services/IssueTrackingService";

@Component({
    selector: 'app-main-shell',
    templateUrl: 'main-shell.component.html'
})
export class MainShellComponent implements OnInit {

  @Input('routes')
    public routes: IMainShellRoute[] = [];

    public menuShowed = false;
    public storage: WindowLocalStorage;
    public username: string | null;
    public roleName:string|null;
    public notification=[];
    public routerLink:string|null;


    constructor (private adminService: IssueTrackingService, public router: Router) {
    }

    public ngOnInit(): void {

        this.username = localStorage.getItem('username');
        this.routerLink=localStorage.getItem('routerLink');


    }


    logout() {
        // window.location.reload();
        this.adminService.logout();
        this.router.navigate(['login']).then(r => true);
    }

}
