import {Component, OnInit} from '@angular/core';
import {IssueTrackingService} from "../_Services/IssueTrackingService";
import {ActivatedRoute, Router} from "@angular/router";
import dialog from "../components/dialog";
import swal from "sweetalert2";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  public ITDeptId = "f48cb514-8e36-4a87-a2e0-49042c096c99";
  public loggedInEmployeeId: string;
  public loggedInDepartmentId: string | "";

  public departmentList: any[];
  public selectedDepartment: any;
  public dashboard: any | null;

  constructor(public issueTrackingService: IssueTrackingService, public router: Router, public activeRouting: ActivatedRoute) {

    this.loggedInEmployeeId = localStorage.getItem('userId');
    this.loggedInDepartmentId = localStorage.getItem("departmentId");
    this.issueTrackingService.GetAllBranch().subscribe(res => {
      this.departmentList = res;
    });


  }

  ngOnInit() {
    if (this.ITDeptId != this.loggedInDepartmentId) {
      this.selectedDepartment = this.loggedInDepartmentId;
    }else{
      this.selectedDepartment="";
    }
    this.getDashboard();
  }

  public getDashboard() {
    dialog.loading();
    this.issueTrackingService.GetDashboard(this.selectedDepartment).subscribe(res => {
      this.dashboard = res;
      dialog.close();
    }, e => {
      swal({
        type: 'error', title: 'Oops...', text: e.message
      });
    })
  }

  convertDate(date: any) {
    let currentDate = new Date();
    let dates = new Date(date);
    let dateTimePipe = new DatePipe("en-US");
    let ret = "";
    if (dateTimePipe.transform(currentDate, 'MMM dd, yyyy') == dateTimePipe.transform(date, 'MMM dd, yyyy')) {
      ret = dateTimePipe.transform(date, 'hh:mm aa ');
    } else if (dateTimePipe.transform(currentDate, 'W MMM yyyy') == dateTimePipe.transform(date, 'W MMM yyyy')) {
      ret = dateTimePipe.transform(date, 'EE hh:mm aa');
    } else if (dateTimePipe.transform(currentDate, 'yyyy') == dateTimePipe.transform(date, 'yyyy')) {
      ret = dateTimePipe.transform(date, 'MMM dd');
    } else {
      ret = dateTimePipe.transform(date, 'MMM dd, yyyy');
    }

    return ret;
  }
  public seeIssueDetails(id: any) {
    this.router.navigate(['LIT/issues/view-issue', id]);
  }

  selectBranch(){
    this.getDashboard();
  }
}
