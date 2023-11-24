import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./modules/pages/task/task.module').then((m) => m.TaskModule),
  },
];
