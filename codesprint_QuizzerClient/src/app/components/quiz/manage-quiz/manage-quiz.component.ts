import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {QuizApiService} from "../../../services/quiz/quiz-api.service";
import {ActivatedRoute, Router} from "@angular/router";
import {IQuiz, Quiz} from "../../../dto/Quiz.model";
import Swal from "sweetalert2";
import {ICategory} from "../../../dto/Category.model";

@Component({
  selector: 'app-manage-quiz',
  templateUrl: './manage-quiz.component.html',
  styleUrls: ['./manage-quiz.component.css']
})
export class ManageQuizComponent implements OnInit {
  public get QuestionType(): any { return this.manageQuizForm.get('questionType'); }
  public get Question(): any { return this.manageQuizForm.get('question'); }
  public get Category(): any { return this.manageQuizForm.get('category'); }
  public get Tag(): any { return this.manageQuizForm.get('tag'); }
  public get Score(): any { return this.manageQuizForm.get('score'); }
  public get IsCorrect(): any { return this.manageQuizForm.get('isCorrect'); }
  public get AnswerText(): any { return this.manageQuizForm.get('answerText'); }

  manageQuizForm: FormGroup = new FormGroup({
    questionType: new FormControl(0, Validators.required),
    question: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[a-zA-Z0-9].+$/)
    ]),
    category: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[a-zA-Z]+$/)
    ]),
    tag: new FormControl('', [
      Validators.required,
      Validators.minLength(1),
      Validators.maxLength(1),
      Validators.pattern(/^[A-Z]$/)
    ]),
    score: new FormControl(0, [
      Validators.required,
      Validators.pattern(/^[\d+]{1,3}$/)
    ]),
    isCorrect: new FormControl(false, Validators.required),
    answerText: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[a-zA-Z0-9].+$/)
    ])
  });

  title: string = "";
  quiz: IQuiz = new Quiz();
  categories: ICategory[] = [];

  constructor(private router: Router, private actRoute: ActivatedRoute, public api: QuizApiService) { }

  ngOnInit(): void {
    let slug = this.actRoute.snapshot.params['slug'];
    this.api.getCategories()
      .subscribe(categories => this.categories = categories);

    this.api.getQuiz(slug)
      .subscribe(quiz => this.quiz = quiz);
  }

  submitQuestions() {
    // this.api.postQuestions(slug);
  // .subscribe({
  //     complete: async () => {
  //       this.resetForm(this.addQuizForm);
  //       await Swal.fire('Added', 'The Quiz has been added!', 'success');
  //     },
  //     error: async (err) => {
  //       await Swal.fire('Error', `There was an error while adding the quiz!\n${err}`, 'error')
  //     }
  //   });
  }
}
