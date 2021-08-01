import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {IncluirComponent} from './COMPONENTS/incluir/incluir.component';
import {InicioComponent} from './COMPONENTS/inicio/inicio.component';
import {ModificarComponent} from './COMPONENTS/modificar/modificar.component';

const routes: Routes = [
  {path:'', redirectTo:'/inicio',pathMatch:'full'},
  {path:'inicio', component: InicioComponent},
  {path:'add', component: IncluirComponent},
  {path:'edit', component: ModificarComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
