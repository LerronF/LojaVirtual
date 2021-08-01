import { Injectable } from '@angular/core';

import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EquipeService {

  url='/api';
  constructor(private http: HttpClient) { }

  //get equipe
  getEquipe()
  {
    return this.http.get(this.url);
  }

  //get equipe un
  getUnEquipe(id:string){
    return this.http.get(this.url+'/'+id);
  }
  
  //incluir equipe
  addEquipe(equipe:Equipe){
    return this.http.post(this.url,equipe);
  }

  //Editar equipe
  editEquipe(id:string, equipe:Equipe){
    return this.http.put(this.url+'/'+id,equipe);
  }

  //deletar equipe
  deleteEquipe(id:string){
    return this.http.delete(this.url+'/'+id);
  }
}

export interface Equipe{
  id?:string;
  descricao?:string;
}