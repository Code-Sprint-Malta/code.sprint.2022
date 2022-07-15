import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Answers} from "../../../dto/Answers.enum";
import {QuizApiService} from "../../../services/quiz/quiz-api.service";
import {Quiz} from "../../../dto/Quiz.model";

@Component({
  selector: 'app-add-quiz',
  templateUrl: './add-quiz.component.html',
  styleUrls: ['./add-quiz.component.css']
})
export class AddQuizComponent implements OnInit {
  public get Title(): any { return this.addQuizForm.get('title'); }
  public get Slug(): any { return this.addQuizForm.get('slug'); }
  public get Description(): any { return this.addQuizForm.get('description'); }
  public get PassingScore(): any { return this.addQuizForm.get('passingScore'); }
  public get ShowCorrectAnswers(): any { return this.addQuizForm.get('showCorrectAnswers'); }
  public get RandomiseQuestionOrder(): any { return this.addQuizForm.get('randomiseQuestionOrder'); }
  public get MessageOnPass(): any { return this.addQuizForm.get('messageOnPass'); }
  public get MessageOnFail(): any { return this.addQuizForm.get('messageOnFail'); }

  addQuizForm: FormGroup = new FormGroup({
    title: new FormControl('',
      [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(255)
      ]),
    slug: new FormControl('',
      [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(255),
        Validators.pattern(/^[a-z]+$/)
      ]),
    description: new FormControl('',
      [
        Validators.required
      ]),
    passingScore: new FormControl(0,
      [
        Validators.required,
        Validators.pattern(/^[\d+]{1,3}$/)
      ]),
    showCorrectAnswers: new FormControl(Answers.NEVER, Validators.required),
    randomiseQuestionOrder: new FormControl(false),
    messageOnPass: new FormControl('', Validators.required),
    messageOnFail: new FormControl('', Validators.required)
  });

  constructor(public api: QuizApiService) { }

  ngOnInit(): void {
  }

  async submitQuiz() {
    await this.api.postQuiz(new Quiz(this.Title, this.Slug, this.Description,
      this.PassingScore, this.ShowCorrectAnswers, this.RandomiseQuestionOrder, this.MessageOnPass, this.MessageOnFail));
  }
}
