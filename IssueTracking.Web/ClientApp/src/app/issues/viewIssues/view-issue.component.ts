import {Component, OnInit, ViewChild} from "@angular/core";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {ActivatedRoute, Router} from "@angular/router";
import dialog from "../../components/dialog";
import {configs} from "../../app-config";
import {
  MonacoEditorComponent,
  MonacoEditorConstructionOptions, MonacoEditorLoaderService,
  MonacoStandaloneCodeEditor
} from "@materia-ui/ngx-monaco-editor";
import {DatePipe} from "@angular/common";
import {AssignIssue, EmployeeModel, IssueCommentModel, ResourceModel} from "../../_model/IssueTrackingModel";
import swal from "sweetalert2";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-view-issue',
  templateUrl: './view-issue.component.html',
  styleUrls: ['./view-issue.component.scss']
})
export class ViewIssueComponent implements OnInit {
  public issue: any = null;
  public issueId: any = null;
  public issueTypeId: any = null;
  public url = configs.url;
  public files: File[] = [];
  public ITStaff = [];
  public ITDept = 'f48cb514-8e36-4a87-a2e0-49042c096c99';
  public assignment: AssignIssue;
  public commentResourceModel: ResourceModel;
  public commentResources: ResourceModel[] = [];
  public issueCommentModel: IssueCommentModel;
  public issueCommentForm: FormGroup;
  public commentedBy: EmployeeModel;
  public loggedInEmployeeId: string;
  public loggedInDepartmentId: string;
  public milestonesList=[];
  public dependentIssuesList=[];
  public isEdit=false;
  public isAdd=false;
  public isForward=false;
  public selectedIssue=null;
  public dueDateTime:any;

  constructor(public fb: FormBuilder, public issueTrackingService: IssueTrackingService, public activeRouting: ActivatedRoute, private monacoLoaderService: MonacoEditorLoaderService, public router:Router) {
    if (this.activeRouting.snapshot.params['issueId']) {
      this.issueId = this.activeRouting.snapshot.params['issueId'];
    }
    this.loggedInEmployeeId = localStorage.getItem("userId");
    this.loggedInDepartmentId = localStorage.getItem("departmentId");
    this.issueTrackingService.GetAllEmployeeByBranchId(this.ITDept).subscribe(res => {
      this.ITStaff = res;
    });
    this.issueTrackingService.GetAllMilestones().subscribe(res=>{
      this.milestonesList=res;
    })

    this.issueCommentForm = this.fb.group({
      issueComment: ['', Validators.required],
      commentResources: []
    })
  }

  ngOnInit() {
    this.loadIssuePage();
    this.dueDateTime=null;
    this.commentedBy = {
      id: this.loggedInEmployeeId,
      firstName: "",
      fatherName: '',
      grFatherName: '',
      username: '',
      empIdNo: ''
    }
    this.issueCommentModel = {
      id: '',
      issueId: this.issueId,
      commentedBy: this.commentedBy,
      issueCommentDate: new Date(),
      issueComment: '',
      commentResource: this.commentResources
    }

    this.commentResourceModel = {
      docRef: '',
      fileName: '',
      data: '',
      mimeType: '',
      index: 0
    };
  }

  loadIssuePage() {
    dialog.loading();
    this.getDependentIssues();
    this.issueTrackingService.GetIssueById(this.issueId).subscribe(res => {
      this.issue = res;
      dialog.close();
    }, dialog.error);
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

  setLink(doc) {
    return !doc.id && doc.mimeType && doc.Data ?
      `javascript:;` :
      `${configs.url}IssueTracking/GetFilePath?fileName=${doc.docRef}&mimeType=${doc.mimeType}`;
  }

  showImage(doc) {
    window.open(`data:${doc.mimeType};base64,${doc.data}`);
  }

  public assignStaff(staffId: any) {
    this.assignment = {
      id: "",
      issueId: this.issueId,
      assignedTo: staffId
    }
    dialog.loading();
    this.issueTrackingService.AssignIssue(this.assignment).subscribe(res => {
      dialog.close();
      swal({
        type: 'success',
        title: 'You have Successfully assign staff',
        showConfirmButton: false,
        timer: 1500
      }).then(value => {
        this.loadIssuePage();
      });
    }, e => {
      swal({
        type: 'error', title: 'Oops...', text: e.message
      });
    })
  }

  public getAssign(empId: any) {
    let employee = "";
    for (let i of this.ITStaff) {
      if (i.id == empId) {
        employee = i;
      }
    }
    return employee;
  }

  onSelect(event) {
    let files = event.addedFiles;

    if (files) {
      for (let i = 0; i < files.length; i++) {
        //this.resetResourceModel();
        let file = files[i];
        if (file) {
          this.issueTrackingService.convertFileToBase64(file).subscribe(base64 => {
            this.commentResourceModel.data = base64;
            this.commentResourceModel.mimeType = file.type;
            this.commentResourceModel.fileName = file.name;
            let resMod = this.commentResourceModel;
            this.commentResources.push(resMod);
            this.resetResourceModel();
          })
        }
      }
    }
    this.files.push(...event.addedFiles);
  }

  onRemove(event) {
    this.files.splice(this.files.indexOf(event), 1);
    this.commentResources = [];
    for (let i = 0; i < this.files.length; i++) {
      //this.resetResourceModel();
      let file = this.files[i];
      if (file) {
        this.issueTrackingService.convertFileToBase64(file).subscribe(base64 => {
          this.commentResourceModel.data = base64;
          this.commentResourceModel.mimeType = file.type;
          this.commentResourceModel.fileName = file.name;
          let resMod = this.commentResourceModel;
          this.commentResources.push(resMod);
          this.resetResourceModel();
        })
      }
    }

  }

  public resetResourceModel() {
    this.commentResourceModel = {
      docRef: "",
      fileName: "",
      data: "",
      mimeType: "",
      index: 0
    }
  }

  public selectIssueComment(model: IssueCommentModel) {
    this.issueCommentModel.id = model.id;
    this.issueCommentModel.issueComment = model.issueComment;
    this.commentResources = model.commentResource;
    for (let i = 0; i < this.commentResources.length; i++) {
      let image = this.issueTrackingService.convertBase64ToFile(this.commentResources[i]);
      this.files.push(image);
    }
  }

  public saveComment() {
    dialog.loading();
    if (this.issueCommentModel.id == '') {
      this.issueTrackingService.AddIssueComment(this.issueCommentModel).subscribe(res => {
        dialog.close();
        swal({
          type: 'success',
          title: 'You have Successfully saved Changes',
          showConfirmButton: false,
          timer: 1500
        }).then(value => {
          this.clearCommentForm();
          this.loadIssuePage();
        });
      }, e => {
        swal({
          type: 'error', title: 'Oops...', text: e.message
        });
      })
    } else {
      this.issueTrackingService.EditIssueComment(this.issueCommentModel).subscribe(res => {
        dialog.close();
        swal({
          type: 'success',
          title: 'You have Successfully saved Changes',
          showConfirmButton: false,
          timer: 1500
        }).then(value => {
          //this.loadPage.next(true);
          this.clearCommentForm();
          this.loadIssuePage();
        });
      }, e => {
        swal({
          type: 'error', title: 'Oops...', text: e.message
        });
      })
    }
  }

  clearCommentForm(){
    this.issueCommentForm.reset();
    this.resetResourceModel();
    this.issueCommentModel.id="";
    this.issueCommentModel.issueComment="";
    this.commentResources=[];
    this.files=[];
  }

  public IssueClose(){
    swal({
      title: 'Are you sure?',
      text: "You want to close selected issues",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, Close!'
    }).then((result) => {
      if (result.value) {
        dialog.loading();
        this.issueTrackingService.CloseIssue(this.issueId, "Issue Close").subscribe(res => {
          swal("Success!", "You have successfully closed issue", "success").then(value => {
            this.loadIssuePage();
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

  public Reopen(){
    swal({
      title: 'Are you sure?',
      text: "You want to re-open selected issues",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, Re-Open!'
    }).then((result) => {
      if (result.value) {
        dialog.loading();
        this.issueTrackingService.ReopenIssue(this.issueId,"Issue Re-Open").subscribe(res => {
          swal("Success!", "You have successfully Re-open issue", "success").then(value => {
           this.loadIssuePage();
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

  public addMilestone(id){
    dialog.loading();
    this.issueTrackingService.AddMilestoneToIssue(this.issueId, id).subscribe(res=>{
      this.loadIssuePage();
    },e=>{
      swal({
        type: 'error',
        title: 'Oops...',
        text: e.message
      });
    })
  }
  public removeMilestone(id){
    swal({
      title: 'Are you sure?',
      text: "You want to remove selected milestone",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, Remove!'
    }).then((result) => {
      if (result.value) {
        dialog.loading();
        this.issueTrackingService.RemoveMilestoneFromIssue(id).subscribe(res => {
          swal("Success!", "You have successfully remove milestone", "success").then(value => {
            this.loadIssuePage();
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
  public removeAssign(id){
    swal({
      title: 'Are you sure?',
      text: "You want to remove selected Assign",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, Remove!'
    }).then((result) => {
      if (result.value) {
        dialog.loading();
        this.issueTrackingService.RemoveAssignFromIssue(id).subscribe(res => {
          swal("Success!", "You have successfully remove assign", "success").then(value => {
            this.loadIssuePage();
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
  public editIssue(selectedIssue){
    if(selectedIssue==null){
      this.isAdd=true;
      this.isEdit=false;
      this.selectedIssue=selectedIssue;
    }else{
      this.isEdit=true;
      this.isAdd=false;
      this.selectedIssue=selectedIssue;
    }
  }

  public forwardIssue(){
    this.isEdit = false;
    this.isAdd = false;
    this.isForward=true;
  }
  public closeModal() {
    this.isEdit = false;
    this.isAdd = false;
    this.isForward=false;
    this.selectedIssue = null;
  }

  public getDependentIssues(){
    this.issueTrackingService.GetAllDependents(this.issueId).subscribe(res=>{
      this.dependentIssuesList=res;
    })
  }

  public addDependency(id){
    dialog.loading();
    this.issueTrackingService.AddDependencyToIssue(this.issueId, id).subscribe(res=>{
      this.loadIssuePage();
    },e=>{
      swal({
        type: 'error',
        title: 'Oops...',
        text: e.message
      });
    })
  }
  public removeDependency(id){
    swal({
      title: 'Are you sure?',
      text: "You want to remove selected Dependency",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, Remove!'
    }).then((result) => {
      if (result.value) {
        dialog.loading();
        this.issueTrackingService.RemoveDependencyFromIssue(id).subscribe(res => {
          swal("Success!", "You have successfully remove dependency", "success").then(value => {
            this.loadIssuePage();
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

  public startTask(){
    swal({
      title: 'Are you sure?',
      text: "You want to start task",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, start!'
    }).then((result) => {
      if (result.value) {
        dialog.loading();
        this.issueTrackingService.StartTask(this.issueId).subscribe(res => {
          swal("Success!", "You have successfully start task", "success").then(value => {
            this.loadIssuePage();
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

  public endTask(id){
    swal({
      title: 'Are you sure?',
      text: "You want to ended task",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, Ended!'
    }).then((result) => {
      if (result.value) {
        dialog.loading();
        this.issueTrackingService.EndTask(id).subscribe(res => {
          swal("Success!", "You have successfully ended task", "success").then(value => {
            this.loadIssuePage();
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

  public setDueDate(){
    if(this.dueDateTime!=null){
      dialog.loading();
      this.issueTrackingService.AddDueDate(this.issueId, this.dueDateTime).subscribe(res=>{
        swal("Success!", "You have successfully set due date", "success").then(value => {
          this.dueDateTime=null;
          this.loadIssuePage();
          dialog.close();
        })
      }, e => {
        swal({
          type: 'error',
          title: 'Oops...',
          text: e.message
        });
      });
    }else {
      swal({
        type: 'error',
        title: 'Oops...',
        text: "Please Select Due Date"
      });
    }
  }
  public seeIssueDetails(id: any) {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['LIT/issues/view-issue', id]);
    });

  }
  public seeSolutionDetails(id: any) {
      this.router.navigate(['/LIT/settings/view-basic-solution', id]);
  }
}
