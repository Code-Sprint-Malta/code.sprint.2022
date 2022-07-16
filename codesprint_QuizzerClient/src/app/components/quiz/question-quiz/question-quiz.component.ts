import {Component, Input, OnInit} from '@angular/core';
import {IQuiz} from "../../../dto/Quiz.model";
import {ICategory} from "../../../dto/Category.model";
import {IQuestion} from "../../../dto/Question.model";

@Component({
  selector: 'app-question-quiz',
  templateUrl: './question-quiz.component.html',
  styleUrls: ['./question-quiz.component.css']
})
export class QuestionQuizComponent implements OnInit {
  @Input() quiz: IQuiz;
  @Input() categories: ICategory[];
  @Input() questions: IQuestion[];

  constructor() { }

  ngOnInit(): void {
  }

}
