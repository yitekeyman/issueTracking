import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import {Routes, RouterModule, Route} from '@angular/router';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import {LoginComponent} from "./login/login.component";
import {AdminLayoutModule} from "./layouts/admin-layout/admin-layout.module";
import {IssuesComponent} from "./issues/issues.component";
import {EditIssueComponent} from "./issues/editIssue/edit-issue.component";
import {DashboardComponent} from "./dashboard/dashboard.component";
import {ViewIssueComponent} from "./issues/viewIssues/view-issue.component";

const routes: Routes =[
  {
    path: 'login',
    component:LoginComponent
  },


  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'LIT',
    redirectTo: 'LIT/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'LIT',
    component: AdminLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () => AdminLayoutModule
      }]},
  {
    path: 'issues',

    component: IssuesComponent,
    children: [
      {
        path: 'edit-issue',
        component: EditIssueComponent

      }

    ]
  }, */
  {
    path: '**',
    redirectTo: 'login',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes)
  ],
  exports: [

  ],
})
export class AppRoutingModule { }


