import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

const routes: Routes = [

  {
    path: '',
    redirectTo: 'academic-level',
    pathMatch: 'full',
  },
  {
    path: 'academic-level',
    loadChildren: () =>
          import('./academic-level/academic-level.module').then((m) => m.AcademicLevelModule)
  },
  {
    path: '',
    redirectTo: 'academic-year',
    pathMatch: 'full',
  },
  {
    path: 'academic-year',
    loadChildren: () =>
          import('./academic-year/academic-year.module').then((m) => m.AcademicYearModule)
  },
  {
    path: 'class',
    loadChildren: () =>
          import('./class/class.module').then((m) => m.ClassModule)
  },
  {
    path: 'class-name',
    loadChildren: () =>
          import('./class-name/class-name.module').then((m) => m.ClassNameModule)
  },
  {
    path: 'student',
    loadChildren: () =>
          import('./student/student.module').then((m) => m.StudentModule)
  },
  

];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule { }
