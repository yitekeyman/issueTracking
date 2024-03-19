import {Injectable} from "@angular/core";
import {Router} from "@angular/router";
import {ApiServices} from "./api.service";
import {min, Observable, ReplaySubject} from "rxjs";
import {DomSanitizer, SafeUrl} from '@angular/platform-browser';
import {HttpClient} from '@angular/common/http';
import {IssueListModel, IssueListRetModel, IssueListReturnModel} from "../_model/IssueTrackingModel";
import {DatePipe} from "@angular/common";
import {END} from "@angular/cdk/keycodes";


@Injectable()
export class IssueTrackingService {
  url: string;


  constructor(public apiService: ApiServices, public router: Router, private sanitizer: DomSanitizer, private http: HttpClient) {
  }

  get sanitizedUrl(): SafeUrl {
    return this.sanitizer.bypassSecurityTrustResourceUrl(this.url);
  }

  public login(model: any) {
    return this.apiService.post('IssueTracking/Login', model);
  }

  public GetPriorityTypeById(model: any) {
    return this.apiService.get(`IssueTracking/GetPriorityTypeById?id=${model}`);
  }

  public GetAllPriorityTypes() {
    return this.apiService.get(`IssueTracking/GetAllPriorityTypes`);
  }

  public GetIssueRaisedSystemById(model: any) {
    return this.apiService.get(`IssueTracking/GetIssueRaisedSystemById?id=${model}`);
  }

  public GetAllIssueRaisedSystems() {
    return this.apiService.get(`IssueTracking/GetAllIssueRaisedSystems`);
  }

  public GetRaisedSystemById(model: any) {
    return this.apiService.get(`IssueTracking/GetRaisedSystemById?id=${model}`);
  }

  public GetAllRaisedSystems() {
    return this.apiService.get(`IssueTracking/GetAllRaisedSystems`);
  }

  public EditIssueType(model: any) {
    return this.apiService.post('IssueTracking/EditIssueType', model);
  }

  public GetIssueTypeById(model: any) {
    return this.apiService.get(`IssueTracking/GetIssueTypeById?id=${model}`);
  }

  public GetAllIssueType(id:any) {
    return this.apiService.get(`IssueTracking/GetAllIssueType?systemRaisedId=${id}`);
  }

  public EditBasicIssueSolution(model: any) {
    return this.apiService.post('IssueTracking/EditBasicIssueSolution', model);
  }

  public GetBasicSolutionById(model: any) {
    return this.apiService.get(`IssueTracking/GetBasicSolutionById?id=${model}`);
  }

  public GetBasicSolutionByIssueType(model: any) {
    return this.apiService.get(`IssueTracking/GetBasicSolutionByIssueType?id=${model}`);
  }

  public GetAllBasicSolution() {
    return this.apiService.get(`IssueTracking/GetAllBasicSolution`);
  }

  public convertFileToBase64(file: File): Observable<string> {
    const result = new ReplaySubject<string>(1);
    const reader = new FileReader();
    reader.readAsBinaryString(file);
    reader.onload = (event) => result.next(btoa(event.target.result.toString()));
    return result;
  }

  public convertBase64ToFile(base64: any) {
    let blob = this.dataURItoBlob(base64);
    const retFile = new File([blob], base64.fileName, {type: base64.mimeType});
    return retFile
  }

  public dataURItoBlob(dataURI) {
    const byteString = window.atob(dataURI.data);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], {type: dataURI.mimeType});
    return blob;
  }

  public timeDifference(value: any) {
    const now = new Date();
    const passDate = new Date(value);
    const diff = now.getTime() - passDate.getTime();
    const seconds = Math.floor(diff / 1000);
    const minutes = Math.floor(seconds / 60);
    const hours = Math.floor(minutes / 60);
    const days = Math.floor(hours / 24);
    const weeks = Math.floor(days / 7);
    const months = Math.floor(days / 30);
    const years = Math.floor(days / 365);
    let ret = 'Just now'
    if (years > 0) {
      ret = years === 1 ? '1 Yr ago' : `${years} Yrs ago`;
    } else if (months > 0) {
      ret = months === 1 ? '1 Mon ago' : `${months} Mons ago`;
    } else if (weeks > 0) {
      ret = weeks === 1 ? '1 week ago' : `${weeks} weeks ago`;
    } else if (days > 0) {
      ret = days === 1 ? '1 day ago' : `${days} days ago`;
    } else if (hours > 0) {
      ret = hours === 1 ? '1 Hr ago' : `${hours} Hrs ago`;
    } else if (minutes > 0) {
      ret = minutes === 1 ? '1 Min ago' : `${minutes} Mins ago`;
    }
    return ret;

  }

  public dueTimeDifference(value: any) {
    const now = new Date();
    const passDate = new Date(value);
    const diff = passDate.getTime() - now.getTime();
    const seconds = Math.floor(diff / 1000);
    const minutes = Math.floor(seconds / 60);
    const hours = Math.floor(minutes / 60);
    const days = Math.floor(hours / 24);
    const weeks = Math.floor(days / 7);
    const months = Math.floor(days / 30);
    const years = Math.floor(days / 365);
    let ret = 'Just now'
    if (diff > 0) {
      if (years > 0) {
        ret = years === 1 ? '1 Yr later' : `${years} Yrs later`;
      } else if (months > 0) {
        ret = months === 1 ? '1 Mon later' : `${months} Mons later`;
      } else if (weeks > 0) {
        ret = weeks === 1 ? '1 week later' : `${weeks} weeks later`;
      } else if (days > 0) {
        ret = days === 1 ? '1 day later' : `${days} days later`;
      } else if (hours > 0) {
        ret = hours === 1 ? '1 Hr later' : `${hours} Hrs later`;
      } else if (minutes > 0) {
        ret = minutes === 1 ? '1 Min later' : `${minutes} Mins later`;
      }
    } else if (diff < 0) {
      if (years > 0) {
        ret = years === 1 ? '1 Yr ago' : `${years} Yrs ago`;
      } else if (months > 0) {
        ret = months === 1 ? '1 Mon ago' : `${months} Mons ago`;
      } else if (weeks > 0) {
        ret = weeks === 1 ? '1 week ago' : `${weeks} weeks ago`;
      } else if (days > 0) {
        ret = days === 1 ? '1 day ago' : `${days} days ago`;
      } else if (hours > 0) {
        ret = hours === 1 ? '1 Hr ago' : `${hours} Hrs ago`;
      } else if (minutes > 0) {
        ret = minutes === 1 ? '1 Min ago' : `${minutes} Mins ago`;
      }
    }
    return ret;

  }

  public calculateTimeDiff(startTime, endTime){
    const startDate = new Date(startTime);
    const endDate=new Date(endTime);
    const diff = endDate.getTime() - startDate.getTime();
    const days = Math.floor(diff / (1000 * 60 * 60 * 24));
    const hours = Math.floor((diff % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
    const seconds = Math.floor((diff % (1000 * 60)) / 1000);

    // Format the result as DD:HH:mm:ss=
    let formattedTime=`${this.padZero(0)}s`;
    if(days>0){
      formattedTime = `${this.padZero(days)}d, ${this.padZero(hours)}h, ${this.padZero(minutes)}m, ${this.padZero(seconds)}s`;
    }else if(hours>0){
      formattedTime = `${this.padZero(hours)}h, ${this.padZero(minutes)}m, ${this.padZero(seconds)}s`;
    }else if(minutes>0){
      formattedTime = `${this.padZero(minutes)}m, ${this.padZero(seconds)}s`;
    }else if(seconds>0){
      formattedTime = `${this.padZero(seconds)}s`;
    }


    return formattedTime;

  }
  padZero(num: number): string {
    return num < 10 ? `0${num}` : num.toString();
  }
  convertDate(date: any) {
    let currentDate = new Date();
    let dates = new Date(date);
    let dateTimePipe = new DatePipe("en-US");
    let ret = "";
    if (dateTimePipe.transform(currentDate, 'MMM dd, yyyy') == dateTimePipe.transform(date, 'MMM dd, yyyy')) {
      ret = dateTimePipe.transform(date, 'hh:mm:ss aa ');
    } else if (dateTimePipe.transform(currentDate, 'W MMM yyyy') == dateTimePipe.transform(date, 'W MMM yyyy')) {
      ret = dateTimePipe.transform(date, 'EE hh:mm:ss aa');
    } else if (dateTimePipe.transform(currentDate, 'yyyy') == dateTimePipe.transform(date, 'yyyy')) {
      ret = dateTimePipe.transform(date, 'MMM dd hh:mm:ss aa');
    } else {
      ret = dateTimePipe.transform(date, 'MMM dd, yyyy hh:mm:ss aa');
    }

    return ret;
  }

  public GetAllIssueStatusTypes() {
    return this.apiService.get(`IssueTracking/GetAllIssueStatusTypes`);
  }

  public AddIssue(model: any) {
    return this.apiService.post(`IssueTracking/AddIssue`, model);
  }

  public EditIssue(model: any) {
    return this.apiService.post(`IssueTracking/EditIssue`, model);
  }

  public GetAllIssues(model: any) {
    return this.apiService.post(`IssueTracking/GetAllIssues`, model);
  }

  public GetsAllIssues() {
    return this.apiService.get(`IssueTracking/GetsAllIssues`);
  }


  public GetIssueById(model: any) {
    return this.apiService.get(`IssueTracking/GetIssueById?id=${model}`);
  }


  // public GetIssueByStatus(model:any){
  //   return this.apiService.get(`IssueTracking/GetIssueByStatus?status=${model}`);
  // }

  public GetAllBranch() {
    return this.apiService.get(`IssueTracking/GetAllBranch`);
  }

  public GetAllEmployee() {
    return this.apiService.get(`IssueTracking/GetAllEmployee`);
  }

  public GetAllEmployeeByBranchId(model: any) {
    return this.apiService.get(`IssueTracking/GetAllEmployeeByBranchId?id=${model}`);
  }

  public AssignIssue(model: any) {
    return this.apiService.post(`IssueTracking/AssignIssue`, model);
  }

  public AddIssueComment(model: any) {
    return this.apiService.post('IssueTracking/AddComment', model);
  }

  public EditIssueComment(model: any) {
    return this.apiService.post('IssueTracking/EditComment', model);
  }

  public CloseIssue(issueId: string, remark: string) {
    return this.apiService.post(`IssueTracking/CloseIssue?issueId=${issueId}&remark=${remark}`, null);
  }

  public ReopenIssue(issueId: string, remark: string) {
    return this.apiService.post(`IssueTracking/ReopenIssue?issueId=${issueId}&remark=${remark}`, null);
  }

  public PatchCloseIssue(model: any) {
    return this.apiService.post('IssueTracking/PatchCloseIssue', model);
  }

  public PatchReopenIssue(model: any) {
    return this.apiService.post('IssueTracking/PatchReopenIssue', model);
  }

  public GetDashboard(model: any) {
    return this.apiService.get(`IssueTracking/GetDashboard?model=${model}`);
  }

  public EditMileStone(model: any) {
    return this.apiService.post('IssueTracking/EditMileStone', model);
  }

  public GetAllMilestones() {
    return this.apiService.get('IssueTracking/GetAllMilestones');
  }

  public GetMilestoneById(id: any) {
    return this.apiService.get(`IssueTracking/GetMilestoneById?id=${id}`);
  }

  public DeleteMilestone(id: any) {
    return this.apiService.post(`IssueTracking/DeleteMilestone?id=${id}`, null);
  }

  public AddMilestoneToIssue(issueId: any, milestoneId: any) {
    return this.apiService.post(`IssueTracking/AddMilestoneToIssue?issueId=${issueId}&milestoneId=${milestoneId}`, null);
  }

  public RemoveMilestoneFromIssue(id: any) {
    return this.apiService.post(`IssueTracking/RemoveMilestoneFromIssue?id=${id}`, null);
  }

  public RemoveAssignFromIssue(id: any) {
    return this.apiService.post(`IssueTracking/RemoveAssignFromIssue?id=${id}`, null);
  }
  public GetAllDependents(id: any) {
    return this.apiService.get(`IssueTracking/GetAllDependents?issueId=${id}`);
  }
  public AddDependencyToIssue(issueId: any, depeId: any) {
    return this.apiService.post(`IssueTracking/AddDependencyToIssue?issueId=${issueId}&depeId=${depeId}`, null);
  }

  public RemoveDependencyFromIssue(id: any) {
    return this.apiService.post(`IssueTracking/RemoveDependencyFromIssue?id=${id}`, null);
  }
  public StartTask(id: any) {
    return this.apiService.post(`IssueTracking/StartTask?issueId=${id}`, null);
  }
  public EndTask(id: any) {
    return this.apiService.post(`IssueTracking/EndTask?id=${id}`, null);
  }
  public AddDueDate(id: any, dueDate:any) {
    return this.apiService.post(`IssueTracking/AddDueDate?issueId=${id}&dueDate=${dueDate}`, null);
  }

  public GetNotification(status:boolean){
    return this.apiService.get(`IssueTracking/GetNotification?status=${status}`);
  }
  public GetUnReadNotification(){
    return this.apiService.get(`IssueTracking/GetUnReadNotification`);
  }
  public MarkReadNotification(model:any){
    return this.apiService.post(`IssueTracking/MarkReadNotification?notId=${model}`,null);
  }

  public GetPhoneBook(model: any) {
    return this.apiService.post(`IssueTracking/GetPhoneBook`, model);
  }

  public GetHeadOfficeDept() {
    return this.apiService.get(`IssueTracking/GetHeadOfficeDept`);
  }
  public ForwardIssue(model:any) {
    return this.apiService.post(`IssueTracking/ForwardIssue`,model);
  }
  public CancelIssue(issueId: string, remark: string) {
    return this.apiService.post(`IssueTracking/CancelIssue?issueId=${issueId}&remark=${remark}`, null);
  }

  public PatchMakeReadNotification(model: any) {
    return this.apiService.post('IssueTracking/PatchMakeReadNotification', model);
  }
  public logout() {
    return this.apiService.post('IssueTracking/Logout', null).subscribe(res => {
      localStorage.removeItem('username');
      localStorage.removeItem('departmentId');
      localStorage.removeItem('userId');
      localStorage.removeItem('role');
      this.router.navigate(['login']);
    })
  }

}
