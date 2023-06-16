import {Injectable} from "@angular/core";
import {Router} from "@angular/router";
import {ApiServices} from "./api.service";


@Injectable()
export class IssueTrackingService{

  constructor(public apiService:ApiServices, public router:Router){}

  public login(model:any){
    return this.apiService.post('IssueTracking/Login', model);
  }

  public GetPriorityTypeById(model:any){
    return this.apiService.get(`IssueTracking/GetPriorityTypeById?id=${model}`);
  }
  public GetAllPriorityTypes(){
    return this.apiService.get(`IssueTracking/GetAllPriorityTypes`);
  }

  public GetIssueRaisedSystemById(model:any){
    return this.apiService.get(`IssueTracking/GetIssueRaisedSystemById?id=${model}`);
  }
  public GetAllIssueRaisedSystems(){
    return this.apiService.get(`IssueTracking/GetAllIssueRaisedSystems`);
  }

  public GetRaisedSystemById(model:any){
    return this.apiService.get(`IssueTracking/GetRaisedSystemById?id=${model}`);
  }
  public GetAllRaisedSystems(){
    return this.apiService.get(`IssueTracking/GetAllRaisedSystems`);
  }

  public EditIssueType(model:any){
    return this.apiService.post('IssueTracking/EditIssueType', model);
  }

  public GetIssueTypeById(model:any){
    return this.apiService.get(`IssueTracking/GetIssueTypeById?id=${model}`);
  }
  public GetAllIssueType(){
    return this.apiService.get(`IssueTracking/GetAllIssueType`);
  }
  public EditBasicIssueSolution(model:any){
    return this.apiService.post('IssueTracking/EditBasicIssueSolution', model);
  }

  public GetBasicSolutionById(model:any){
    return this.apiService.get(`IssueTracking/GetBasicSolutionById?id=${model}`);
  }
  public GetBasicSolutionByIssueType(model:any){
    return this.apiService.get(`IssueTracking/GetBasicSolutionByIssueType?id=${model}`);
  }
  public GetAllBasicSolution(){
    return this.apiService.get(`IssueTracking/GetAllBasicSolution`);
  }

  public logout(){
    return this.apiService.post('IssueTracking/Logout', null).subscribe(res=>{
      localStorage.removeItem('username');
      localStorage.removeItem('departmentId');
      localStorage.removeItem('userId');
      localStorage.removeItem('role');
      this.router.navigate(['login']);
    })
  }
}
