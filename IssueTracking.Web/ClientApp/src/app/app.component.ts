import { Component} from '@angular/core';
import {Router} from "@angular/router";
import {ApiServices, ErrorMsg} from "./_Services/api.service";
import {IssueTrackingService} from "./_Services/IssueTrackingService";
import swal from "sweetalert2";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public errData : {msg: '', title: ''};
constructor(public router:Router, public apiService:ApiServices, public ITService:IssueTrackingService) {
  ApiServices.apiEvent.subscribe((errMsg: ErrorMsg)=>{
    if (errMsg.status === 400){ //session expired head to login  page
      swal({
        type: 'error', title: errMsg.title, text: errMsg.msg
      });
     this.ITService.logout();
    }
  })
}
}
