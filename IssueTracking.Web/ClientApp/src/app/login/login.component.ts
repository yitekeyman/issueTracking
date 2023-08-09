import {Component, OnInit} from "@angular/core";
import {LoginUser} from "../_model/IssueTrackingModel";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {FormsModule} from '@angular/forms';
import {ReactiveFormsModule} from '@angular/forms';
import {IssueTrackingService} from "../_Services/IssueTrackingService";
import {Router} from "@angular/router";
import dialog from "../components/dialog";

@Component({
  selector:"app-login",
  templateUrl:"./login.component.html"
})
export class LoginComponent implements OnInit{
  public loginModel:LoginUser;
  loginForm: FormGroup;
  constructor(private fb: FormBuilder,private router: Router, private issueTrackingService:IssueTrackingService) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }
  ngOnInit() {
    this.loginModel={
      username:"",
      password:""
    }
  }

  public loginUser(){
    dialog.loading();
    this.issueTrackingService.login(this.loginModel).subscribe(res=>{
      localStorage.setItem('username', this.loginModel.username);
      localStorage.setItem('role', res.role);
      localStorage.setItem('fullname', res.fullname);
      localStorage.setItem('departmentId', res.departmentId);
      localStorage.setItem('userId', res.userId);
      this.router.navigateByUrl("LIT").then(() => dialog.close()).catch(dialog.error);
    }, dialog.error)

  }
}
