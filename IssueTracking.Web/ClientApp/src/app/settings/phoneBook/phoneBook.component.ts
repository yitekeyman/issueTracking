import {Component, OnInit} from "@angular/core";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {PagerService} from "../../_Services/pager.service";
import dialog from "../../components/dialog";
import {PhoneBookSearchParam} from "../../_model/IssueTrackingModel";
import {FormBuilder, FormGroup} from "@angular/forms";

@Component({
  selector:'app-phonebook',
  templateUrl:'./phoneBook.component.html'
})
export class PhoneBookComponent implements OnInit{
  public phonebookList=[];
  public pager: any = {};
  pagedItems: any[];
  public searchPhoneBook:PhoneBookSearchParam;
  public searchForm:FormGroup;
  public ITDeptId = "f48cb514-8e36-4a87-a2e0-49042c096c99";
  public loggedInEmployeeId: string;
  public loggedInDepartmentId: string | "";
  public departmentList: any[];
  constructor(public issueTrackingService:IssueTrackingService, public pagerService:PagerService, public fb:FormBuilder) {
    this.loggedInEmployeeId = localStorage.getItem('userId');
    this.loggedInDepartmentId = localStorage.getItem("departmentId");
    this.issueTrackingService.GetAllBranch().subscribe(res => {
      this.departmentList = res;
    });
    this.searchForm=this.fb.group({
      empId:[''],
      name:[''],
      department:['']
    })
  }
  ngOnInit() {
    this.searchPhoneBook={
      empIdNo:'',
      name:'',
      departmentId:'',
    }
    this.getPhonebook();
  }

  public getPhonebook(){
    dialog.loading();
    this.phonebookList=[];
    this.issueTrackingService.GetPhoneBook(this.searchPhoneBook).subscribe(res => {
      this.phonebookList = res;
      if (this.phonebookList.length > 0) {
        if (this.pager.currentPage == 0) {
          this.setPage(1);
        } else {
          this.setPage(this.pager.currentPage);
        }
      } else {
        this.setPage(1);
      }
      dialog.close();
    }, dialog.error)
  }

  public setPage(page: number) {
    // if (page < 1 || page > this.pager.totalPages) {
    //   return;
    // }

    this.pager = this.pagerService.getPager(this.phonebookList.length, page);

    //get the paged items
    this.pagedItems = this.phonebookList.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }
}
