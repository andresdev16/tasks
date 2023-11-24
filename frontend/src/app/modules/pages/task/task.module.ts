import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskFormComponent } from './containers/task-form/task-form.component';
import { TaskListComponent } from './containers/task-list/task-list.component';
import { RouterModule, Routes } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { InputErrorModule } from '../../../shared/directives/input-error/input-error.module';
import { ReactiveFormsModule } from '@angular/forms';
import { RequiredModule } from '../../../shared/pipes/required/required.module';
import { FormControlModule } from '../../../shared/pipes/form-control/form-control.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCheckboxModule } from '@angular/material/checkbox';

const routes: Routes = [
  {
    path: '',
    component: TaskListComponent,
  },
];

@NgModule({
  declarations: [TaskFormComponent, TaskListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormControlModule,
    RequiredModule,
    ReactiveFormsModule,
    InputErrorModule,
    MatTableModule,
    MatPaginatorModule,
    MatCheckboxModule,
  ],
})
export class TaskModule {}
