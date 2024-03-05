import {Component, OnInit, ElementRef} from '@angular/core';
import {Location, LocationStrategy, PathLocationStrategy} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import Chart from 'chart.js';
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import dialog from "../../_shared/dialog";
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
  public routerLink="/LIT/issues"
  public q='';
  public state=1;
  public branch='';
  public type=0;
  public sort=0;
  public labels=0;
  public milestones=0;
  public assignee='';
  public notifications:any;
  public currentPage:any;
  constructor(location: Location, private element: ElementRef, public router: Router, public activeRouting: ActivatedRoute, public issueTrackingService:IssueTrackingService) {




  }

  ngOnInit() {
    this.username = localStorage.getItem("username");
    this.userRole = localStorage.getItem("role");
    this.departmentId = localStorage.getItem("departmentId");
    this.userId = localStorage.getItem("userId");
	this.getNotifications();


  }
  navigateWithQueryParams() {
    const queryParams = {
      state: this.state,
      q:this.q,
      branch: this.branch,
      type: this.type,
      sort: this.sort,
      labels: this.labels,
      milestones: this.milestones,
      assignee: this.assignee
    };

    // Use the navigate method to set query parameters
    this.router.navigate(['/LIT/issues'], { queryParams });
  }

  public getNotifications(){
    dialog.loading();
    this.issueTrackingService.GetNotification().subscribe(res=>{
      this.notifications=res;
      dialog.close();
    })
  }

  public setPages(pageName:any){
    localStorage.setItem('routerLink', pageName);
    this.currentPage=pageName;
  }
  public logout(){
    this.issueTrackingService.logout();
  }
}
