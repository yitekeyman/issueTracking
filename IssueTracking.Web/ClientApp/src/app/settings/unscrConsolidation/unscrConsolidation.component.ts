import {Component, OnInit} from "@angular/core";
import {FormBuilder, FormGroup} from "@angular/forms";
import {Router} from "@angular/router";
import swal from "sweetalert2";
import {IssueTrackingService} from "../../_Services/IssueTrackingService";
import {PagerService} from "../../_Services/pager.service";
import dialog from "../../_shared/dialog";
import { HighlightPipe } from "src/app/highlight.pipe";

@Component({
  selector:"app-unscrconsolidation",
  templateUrl:"./unscr-consolidation.component.html",
  styleUrls: ['./unscr-consolidation.component.css'],
})
export class UnscrConsolidationComponent implements OnInit{
  public results = null;
  public pager: any = {};
  pagedItems: any[];
  public searchParams="";
  public searchForm: FormGroup;
  public selectedId:number;
  public showDetails:boolean|false;
  filteredData: any[];
  public isLoading = false;

  constructor(public fb: FormBuilder, public router: Router, public issueTrackingService: IssueTrackingService, public pagerService: PagerService) {
    this.searchForm = fb.group({
      param:['']
    })
  }
  ngOnInit() {
    this.getResults();
  }

  public getResults() {
    this.isLoading=true;
    dialog.loading();
    this.results=[];
    this.issueTrackingService.SearchMoneyLaundry(this.searchParams).subscribe(res => {

      this.results = res;

      if (this.results.length > 0) {
        this.setPage(1);
      }
      this.isLoading = false;
      dialog.close();
    }, e => {
      this.isLoading = false;
      swal({
        type: 'error', title: 'Oops...', text: e.message
      });
    })
  }

  public setPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    this.pager = this.pagerService.getPager(this.results.length, page);

    //get the paged items
    this.pagedItems = this.results.slice(this.pager.startIndex, this.pager.endIndex + 1);

  }

  public search(param){
    this.searchParams = param;
    this.getResults();
  }

  public seeUNConsolidation(id:number){

    this.selectedId=id;
    this.showDetails=true;

  }
  public closeModal() {
    this.showDetails = false;
    this.selectedId=0;

  }
}


