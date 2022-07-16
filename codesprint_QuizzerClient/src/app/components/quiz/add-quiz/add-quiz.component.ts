import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {QuizApiService} from "../../../services/quiz/quiz-api.service";
import {IQuiz, Quiz} from "../../../dto/Quiz.model";
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-quiz',
  templateUrl: './add-quiz.component.html',
  styleUrls: ['./add-quiz.component.css']
})
export class AddQuizComponent implements OnInit {
  neverShowAnswer: string = "Never";
  endShowAnswer: string = "At the end of the quiz";
  alwaysShowAnswer: string = "After each question";

  //region Form Controls Getters

  public get Title(): any { return this.addQuizForm.get('title'); }
  public get Slug(): any { return this.addQuizForm.get('slug'); }
  public get Description(): any { return this.addQuizForm.get('description'); }
  public get PassingScore(): any { return this.addQuizForm.get('passingScore'); }
  public get ShowCorrectAnswers(): any { return this.addQuizForm.get('showCorrectAnswer'); }
  public get RandomiseQuestionOrder(): any { return this.addQuizForm.get('randomiseQuestionOrder'); }
  public get MessageOnPass(): any { return this.addQuizForm.get('messageOnPass'); }
  public get MessageOnFail(): any { return this.addQuizForm.get('messageOnFail'); }

  //endregion

  //region Reactive Form Initialisation and Validation

  addQuizForm: FormGroup = new FormGroup({
    // required, min: 255, max 255
    title: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(255)
      ]),

    // required, min: 1, max: 255, 1 or more lowercase characters, no digits, uppercase or special characters
    slug: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(255),
        Validators.pattern(/^[a-z]+$/)
      ]),

    // required, required, 1 or more uppercase/lowercase characters including digits and special characters, no new lines
    description: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[a-zA-Z0-9].+$/)
    ]),

    // required, digits only between 1 and 3 digits
    passingScore: new FormControl('', [
        Validators.required,
        Validators.pattern(/^[\d+]{1,3}$/)
      ]),

    // required
    showCorrectAnswer: new FormControl(this.neverShowAnswer, Validators.required),
    randomiseQuestionOrder: new FormControl(false),

    // required, required, 1 or more uppercase/lowercase characters including digits and special characters, no new lines
    messageOnPass: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[a-zA-Z0-9].+$/)
    ]),
    messageOnFail: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[a-zA-Z0-9].+$/)
    ])
  });

  //endregion

  constructor(public api: QuizApiService) { }

  ngOnInit(): void { }

  submitQuiz() {
    // creating the model which will be posted to the server
    let quiz: IQuiz = this.addQuizForm.value as IQuiz;

    // posting the quiz
    this.api.postQuiz(quiz)
      .subscribe({
        complete: async () => {
          this.resetAddForm();
          await Swal.fire('Added', 'The Quiz has been added!', 'success');
        },
        error: async (err) =>
          await Swal.fire('Error', `There was an error while adding the quiz!`, 'error')
      });
  }

  resetAddForm() {
    this.addQuizForm.reset();
    this.ShowCorrectAnswers.setValue(this.neverShowAnswer);
  }
}
