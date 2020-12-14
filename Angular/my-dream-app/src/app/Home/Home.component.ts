import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { element } from 'protractor';
import { DataServiceService } from '../Shared/data-service.service';
import { Question } from '../Shared/Model';

@Component({
  selector: 'app-Home',
  templateUrl: './Home.component.html',
  styleUrls: ['./Home.component.scss'],
})
export class HomeComponent implements OnInit {
  questions: Question[];

  constructor(
    private service: DataServiceService,
    private route: ActivatedRoute
    ) {}

  ngOnInit() {
    this.service.getQuestions()
      .then((x) =>{

        var searchText;
        this.route.params.forEach((param: Params) =>{
          if (param['searchText'] !== undefined) {
            searchText = param['searchText'];
          }
        })

        if (searchText !== undefined) {
          x = x.filter((element)=>{
            if (element.topic.toLowerCase().includes(searchText.toLowerCase())) {
              return true;
            }
            return false;
          })
        }
        this.questions = x.sort( (a,b) =>{
          if(a.lastActivity > b.lastActivity){
            return -1;
          }
          if(a.lastActivity < b.lastActivity){
            return 1;
          }
          return 0;
        });
      });
  }

  onClick(){
    console.log(this.questions)
  }
}
