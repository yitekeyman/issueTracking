import {Component, OnInit,} from "@angular/core";
import {
  IssueFilterParameter,
  IssueListReturnModel,
  PatchAction,
  QueryParams,
  ResourceModel
} from "../_model/IssueTrackingModel";
import {IssueTrackingService} from "../_Services/IssueTrackingService";
import {PagerService} from "../_Services/pager.service";
import {ActivatedRoute, Router} from "@angular/router";
import dialog from "../components/dialog";
import {FormArray, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {EditIssueComponent} from "./editIssue/edit-issue.component";
import swal from "sweetalert2";


@Component({
  selector: 'app-issues',
  templateUrl: './issues.component.html'
})
export class IssuesComponent implements OnInit {

  public filterParameter: IssueFilterParameter;

  public issuesList:any;
  public selectedIssueLists:PatchAction;
  public issueListReturn: IssueListReturnModel;
  public pager: any = {};
  public isAdd = false;
  public isEdit = false;
  public selectedIssue: any;
  public selectedIssueType: any;
  pagedItems: any[];
  public ITDeptId = "f48cb514-8e36-4a87-a2e0-49042c096c99";
  public queryParams: QueryParams;
  public departmentList = [];
  public employeeList = [];
  public loggedIdDept = '';
  public priorityList=[];
  public issueTypeList=[];

  public branchName="Branches/Dept";
  public issueTypeName="Issue Type";
  public priorityName="Priority";
  public assigneeName="Assignee";
  public typeName="Type";
  public sortName="Sort";

  public memberGroup: FormGroup;
  public isMasterSel: boolean;
  public haveSelected:boolean;
  public memberForm: FormGroup;

  public loggedInUserId='';
  public loggedUserName='';

  constructor(public fb: FormBuilder, public issueTrackingService: IssueTrackingService, public pagerService: PagerService, public router: Router, public activeRouting: ActivatedRoute) {
    this.queryParams = {
      state: 1,
      query: '',
      branch: '',
      type: 0,
      issueType: -1,
      priority: 0,
      assignee: '',
      sort: 1
    }
    this.loggedIdDept = localStorage.getItem('departmentId');
    this.loggedInUserId=localStorage.getItem('userId');
    this.loggedUserName=localStorage.getItem('username');
    if (localStorage.getItem('departmentId') != 'f48cb514-8e36-4a87-a2e0-49042c096c99') {
      this.queryParams.branch = localStorage.getItem('departmentId');
      this.issueTrackingService.GetAllEmployeeByBranchId(this.queryParams.branch).subscribe(res => {
        this.employeeList = res;
      })
    } else {
      this.issueTrackingService.GetAllEmployeeByBranchId(this.ITDeptId).subscribe(res => {
        this.employeeList = res;
      })
    }

    this.issueTrackingService.GetAllBranch().subscribe(res => {
      this.departmentList = res;
    });

    this.issueTrackingService.GetAllPriorityTypes().subscribe(res=>{
      this.priorityList=res;
    })

    this.issueTrackingService.GetAllIssueType().subscribe(res=>{
      this.issueTypeList=res;
    })
    this.memberGroup = this.fb.group({
      checkUncheckAll: [''],
      listOfMem: this.fb.array([], [Validators.required])
    });
    this.isMasterSel = false;
    this.haveSelected=false;

    this.selectedIssueLists={
      caseList:[],
      remark:"Issue Closed by Patch Action"
    }
  }
  get memberArray() {
    return this.memberGroup.get('listOfMem') as FormArray;
  }
  ngOnInit() {
    this.getAllIssues();
  }

  public getAllIssues() {
    this.isEdit = false;
    this.isAdd = false;
    this.isMasterSel=false;
    this.haveSelected=false;
    this.selectedIssue = null;
    this.selectedIssueLists.caseList=[];
    this.memberArray.controls=[];
    dialog.loading();
    this.issueTrackingService.GetAllIssues(this.queryParams).subscribe(res => {
      this.issuesList = res;
      for (const z of this.issuesList.issueList) {
        this.memberForm = this.fb.group({memberId: ['']});
        this.memberArray.push(this.memberForm);
      }
      if (this.issuesList.issueList?.length > 0)
        this.setPage(1);
      dialog.close();
    }, e => {
      swal({
        type: 'error', title: 'Oops...', text: e.message
      });
    })
  }

  public setPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    this.pager = this.pagerService.getPager(this.issuesList.issueList.length, page);

    //get the paged items
    this.pagedItems = this.issuesList.issueList.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }

  public editIssue(id: any, issueTypeId: any) {
    this.selectedIssueType = issueTypeId;
    if (id != null) {
      this.isAdd = false;
      this.isEdit = true;
      this.selectedIssue = id;
    } else {
      this.isEdit = false;
      this.isAdd = true;
      this.selectedIssue = null;

    }
  }

  public seeIssueDetails(id: any) {
    this.router.navigate(['LIT/issues/view-issue', id]);
  }


  public closeModal() {
    this.isEdit = false;
    this.isAdd = false;
    this.selectedIssue = null;
    this.selectedIssueType = 0;
  }

  public queryIssues(type, id) {
    if (type == "state") {
      this.queryParams.state = id;
    } else if (type == "type") {
      this.queryParams.type = id;
    } else if (type == "sort") {
      this.queryParams.sort = id;
    } else if (type == "issueType") {
      this.queryParams.issueType = id;
    } else if (type == "priority") {
      this.queryParams.priority = id;
    } else if (type == "assignee") {
      this.queryParams.assignee = id;
    } else if (type == "query") {
      this.queryParams.query = id;
    }else if (type == "branch") {
      this.queryParams.branch = id;
    }

    this.getAllIssues();
  }

  checkUncheckAll() {
    for (let i = 0; i < this.memberArray.length; i++) {
      if (this.isMasterSel) {
        const m = this.memberGroup.controls.listOfMem as FormArray;
        m.controls[i].get("memberId").setValue(true);
        this.haveSelected=true;
      } else {
        const m = this.memberGroup.controls.listOfMem as FormArray;
        m.controls[i].get("memberId").setValue(false);
        this.haveSelected=false;
      }
    }

  }
  isAllSelected() {
    let isSelected = true;
    let hasSelected=true;
    for (let i = 0; i < this.memberArray.length; i++) {
      const m = this.memberGroup.controls.listOfMem as FormArray;

      if (m.controls[i].get("memberId").value == false) {
        isSelected = false;
      }else{
        hasSelected=false;
      }
    }
    if(hasSelected){
      this.haveSelected=false;
    }else{
      this.haveSelected=true
    }
    if (isSelected) {
      this.isMasterSel = true;
      this.checkUncheckAll();
    } else {
      this.memberGroup.get("checkUncheckAll").setValue(false);
    }
  }

  public patchClose(){
    this.buildSelectedData();
    swal({
      title: 'Are you sure?',
      text: "You want to close "+this.selectedIssueLists.caseList.length+" selected issues",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, Close!'
    }).then((result) => {
      if (result.value) {
        dialog.loading();
        this.issueTrackingService.PatchCloseIssue(this.selectedIssueLists).subscribe(res => {
          swal("Success!", res, "success").then(value => {
            this.getAllIssues();
            dialog.close();
          })

        }, e => {
          swal({
            type: 'error',
            title: 'Oops...',
            text: e.message
          });

        });
      }
    })
  }

  public patchReopen(){
    this.buildSelectedData();
    swal({
      title: 'Are you sure?',
      text: "You want to re-open "+this.selectedIssueLists.caseList.length+" selected issues",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, Re-Open!'
    }).then((result) => {
      if (result.value) {
        dialog.loading();
        this.issueTrackingService.PatchReopenIssue(this.selectedIssueLists).subscribe(res => {
          swal("Success!", res, "success").then(value => {
            this.getAllIssues();
            dialog.close();
          })

        }, e => {
          swal({
            type: 'error',
            title: 'Oops...',
            text: e.message
          });

        });
      }
    })
  }

  public buildSelectedData(){
    dialog.loading();
    let issues=this.memberGroup.value;
    this.selectedIssueLists.caseList=issues.listOfMem.map((mem, i) => mem.memberId === true ? this.issuesList.issueList[i].id : null).filter(a => a !== null);
    dialog.close();
  }
}
