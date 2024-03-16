import {Component, OnInit, ElementRef} from '@angular/core';
import {Location, LocationStrategy, PathLocationStrategy} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import Chart from 'chart.js';
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import dialog from "../../_shared/dialog";
import {EmployeeModel} from "../../_model/IssueTrackingModel";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  public username: string = "";
  public departmentId: string = "";
  public departmentName: string = "";
  public userRole: string = "";
  public userId: string = "";

  public routerLink = "/LIT/issues"
  public q = '';
  public state = 1;
  public branch = '';
  public type = 0;
  public sort = 0;
  public labels = 0;
  public milestones = 0;
  public assignee = '';
  public notifications: any;
  public currentPage: any;
  public employee: EmployeeModel=null;

  public totalNotif:number=0;
  constructor(location: Location, private element: ElementRef, public router: Router, public activeRouting: ActivatedRoute, public issueTrackingService: IssueTrackingService) {
    this.username = localStorage.getItem("username");
    this.userRole = localStorage.getItem("role");
    this.departmentId = localStorage.getItem("departmentId");
    this.userId = localStorage.getItem("userId");
    this.issueTrackingService.GetUnReadNotification().subscribe(res2=>{
      this.totalNotif=res2;
    })
  }

  ngOnInit() {

    //this.getNotifications();
    this.issueTrackingService.GetAllEmployeeByBranchId(this.departmentId).subscribe(res => {
      for (let i = 0; i < res.length; i++) {
        if (res[i].id == this.userId) {
          this.employee = res[i];
        }
      }

    })

  }

  navigateWithQueryParams() {
    const queryParams = {
      state: this.state,
      q: this.q,
      branch: this.branch,
      type: this.type,
      sort: this.sort,
      labels: this.labels,
      milestones: this.milestones,
      assignee: this.assignee
    };

    // Use the navigate method to set query parameters
    this.router.navigate(['/LIT/issues'], {queryParams});
  }



  public setPages(pageName: any) {
    localStorage.setItem('routerLink', pageName);
    this.currentPage = pageName;
  }

  public showNotification(){
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['LIT/notification']);
    });

  }
  public logout() {
    this.issueTrackingService.logout();
  }
}
