import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskListComponent } from './task-list/task-list.component';
import { TaskFormComponent } from './task-form/task-form.component';
import { LoginComponent } from './login/login.component';

//export const routes: Routes = [];

export const routes: Routes = [
    {
      path: '',
      component: TaskListComponent
    },
    {
      path: 'tasks',
      component: TaskListComponent
    },
    {
      path: 'tasks/new',
      component: TaskFormComponent
    },
    {
      path: 'tasks/:id',
      component: TaskFormComponent
    },
    {
      path: 'login',
      component: LoginComponent
    }
  ];
  
  @NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }