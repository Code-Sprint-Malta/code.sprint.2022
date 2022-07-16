import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {QuizApiService} from "../../../services/quiz/quiz-api.service";
import {IQuiz, Quiz} from "../../../dto/Quiz.model";
import {ICategory} from "../../../dto/Category.model";
import {IQuestion} from "../../../dto/Question.model";

@Component({
  selector: 'app-start-quiz',
  templateUrl: './start-quiz.component.html',
  styleUrls: ['./start-quiz.component.css']
})
export class StartQuizComponent implements OnInit {
  quiz: IQuiz = new Quiz();
  categories: ICategory[] = [];
  questions: IQuestion[] = [];

  play: boolean = true;

  constructor(private router: Router, private actRoute: ActivatedRoute, public api: QuizApiService) { }

  ngOnInit(): void {
    // getting the quiz, the question categories, questions and answers
    // the answers are stored in the questions object as an array

    let slug = this.actRoute.snapshot.params['slug'];
    this.api.getQuiz(slug)
      .subscribe(quiz => this.quiz = quiz);

    this.api.getCategories()
      .subscribe(categories => this.categories = categories);

    this.api.getQuestions(slug)
      .subscribe(questions => this.questions = questions);
  }

  startQuiz() {
    this.play = true;
  }
}
