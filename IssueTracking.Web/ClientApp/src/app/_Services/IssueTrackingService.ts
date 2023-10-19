import {Injectable} from "@angular/core";
import {Router} from "@angular/router";
import {ApiServices} from "./api.service";
import {Observable, ReplaySubject} from "rxjs";
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import {IssueListRetModel} from "../_model/IssueTrackingModel";


@Injectable()
export class IssueTrackingService{
  url: string;

  constructor(public apiService:ApiServices, public router:Router, private sanitizer: DomSanitizer){}

  get sanitizedUrl(): SafeUrl {
    return this.sanitizer.bypassSecurityTrustResourceUrl(this.url);
  }

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


  public convertFileToBase64(file: File): Observable<string> {
    const result = new ReplaySubject<string>(1);
    const reader = new FileReader();
    reader.readAsBinaryString(file);
    reader.onload = (event) => result.next(btoa(event.target.result.toString()));
    return result;
  }

/*
  public convertBase64ToFile(base64:any){
    let blob=this.dataURItoBlob(base64);
    const retFile=new File([blob],base64.fileName, {type:base64.mimeType});
    return retFile
  }
*/
  public convertBase64ToFile(base64: any): File {
    const byteCharacters = atob(base64);
    const byteArrays = [];

    for (let offset = 0; offset < byteCharacters.length; offset += 512) {
      const slice = byteCharacters.slice(offset, offset + 512);
      const byteNumbers = new Array(slice.length);

      for (let i = 0; i < slice.length; i++) {
        byteNumbers[i] = slice.charCodeAt(i);
      }

      const byteArray = new Uint8Array(byteNumbers);
      byteArrays.push(byteArray);
    }

    const blob = new Blob(byteArrays);
    const fileName = base64.fileName; // Assuming fileName is a property of the `base64` object

    const urlCreator = window.URL || window.webkitURL;
    const imageUrl = urlCreator.createObjectURL(blob);

    const sanitizedUrl: SafeUrl = this.sanitizer.bypassSecurityTrustResourceUrl(imageUrl);
    const retFile = new File([blob], fileName, { type: base64.mimeType });
    retFile['sanitizedUrl'] = sanitizedUrl; // Add sanitizedUrl property to the retFile object
    return retFile;
  }
  public dataURItoBlob(dataURI) {
    const byteString = window.atob(dataURI.data);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], { type: dataURI.mimeType });
    return blob;
  }


  public GetAllIssueStatusTypes(){
    return this.apiService.get(`IssueTracking/GetAllIssueStatusTypes`);
  }

  public AddIssue(model:any){
    return this.apiService.post(`IssueTracking/AddIssue`, model);
  }
  public EditIssue(model:any){
    return this.apiService.post(`IssueTracking/EditIssue`, model);
  }

  public GetAllIssues(){
    return this.apiService.get(`IssueTracking/GetAllIssues`);
  }

  public GetIssueById(model:any){
    return this.apiService.get(`IssueTracking/GetIssueById?id=${model}`);
  }

  // public GetIssueByStatus(model:any){
  //   return this.apiService.get(`IssueTracking/GetIssueByStatus?status=${model}`);
  // }

  public GetAllBranch(){
    return this.apiService.get(`IssueTracking/GetAllBranch`);
  }
  public GetAllEmployee(){
    return this.apiService.get(`IssueTracking/GetAllEmployee`);
  }

  public GetAllEmployeeByBranchId(model:any){
    return this.apiService.get(`IssueTracking/GetAllEmployeeByBranchId?id=${model}`);
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
