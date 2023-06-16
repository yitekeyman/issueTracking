import {Component, OnInit, ElementRef} from '@angular/core';
import {Location, LocationStrategy, PathLocationStrategy} from '@angular/common';
import {Router} from '@angular/router';
import Chart from 'chart.js';
import {IssueTrackingService} from "../../_Services/IssueTrackingService";

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

  constructor(location: Location, private element: ElementRef, private router: Router, public issueTrackingService:IssueTrackingService) {

  }

  ngOnInit() {
    this.username = localStorage.getItem("username");
    this.userRole = localStorage.getItem("role");
    this.departmentId = localStorage.getItem("departmentId");
    this.userId = localStorage.getItem("userId");

  }

  public logout(){
    this.issueTrackingService.logout();
  }
}
