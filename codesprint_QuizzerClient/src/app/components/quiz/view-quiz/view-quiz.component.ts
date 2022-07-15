import { Component, OnInit } from '@angular/core';
import {QuizApiService} from "../../../services/quiz/quiz-api.service";
import {IQuiz, Quiz} from "../../../dto/Quiz.model";

@Component({
  selector: 'app-view-quiz',
  templateUrl: './view-quiz.component.html',
  styleUrls: ['./view-quiz.component.css']
})
export class ViewQuizComponent implements OnInit {
  quizzes: IQuiz[] = [];
  value: string = "";

  constructor(public api: QuizApiService) {
  }

  ngOnInit(): void {
    this.api.getQuizzes()
      .subscribe(quizzes => this.quizzes = quizzes);
  }
}
