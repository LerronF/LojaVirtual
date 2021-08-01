import { Component, OnInit } from '@angular/core';
import {EquipeService} from '../../SERVICES/equipe.service';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent implements OnInit {

  constructor(private EquipeService:EquipeService) { }

  ngOnInit(): void {
    this.listarEquipe();
  }

  listarEquipe()
  {
    this.EquipeService.getEquipe().subscribe(
      res=>{
        console.log(res)
      },
      err=>console.log(err)
    );
  }

}
