import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Answers} from "../../../dto/Answers.enum";
import {QuizApiService} from "../../../services/quiz/quiz-api.service";
import {Quiz} from "../../../dto/Quiz.model";
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

  public get Title(): any { return this.addQuizForm.get('title'); }
  public get Slug(): any { return this.addQuizForm.get('slug'); }
  public get Description(): any { return this.addQuizForm.get('description'); }
  public get PassingScore(): any { return this.addQuizForm.get('passingScore'); }
  public get ShowCorrectAnswers(): any { return this.addQuizForm.get('showCorrectAnswers'); }
  public get RandomiseQuestionOrder(): any { return this.addQuizForm.get('randomiseQuestionOrder'); }
  public get MessageOnPass(): any { return this.addQuizForm.get('messageOnPass'); }
  public get MessageOnFail(): any { return this.addQuizForm.get('messageOnFail'); }

  addQuizForm: FormGroup = new FormGroup({
    title: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(255)
      ]),
    slug: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(255),
        Validators.pattern(/^[a-z]+$/)
      ]),
    description: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[a-zA-Z0-9].+$/)
    ]),
    passingScore: new FormControl(0, [
        Validators.required,
        Validators.pattern(/^[\d+]{1,3}$/)
      ]),
    showCorrectAnswers: new FormControl(Answers.NEVER, Validators.required),
    randomiseQuestionOrder: new FormControl(false),
    messageOnPass: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[a-zA-Z0-9].+$/)
    ]),
    messageOnFail: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[a-zA-Z0-9].+$/)
    ])
  });

  constructor(public api: QuizApiService) { }

  ngOnInit(): void {
  }

  submitQuiz() {
    const showCorrectAnswer =
      this.ShowCorrectAnswers.value == 1 ? this.neverShowAnswer :
      this.ShowCorrectAnswers.value == 2 ? this.endShowAnswer :
      this.ShowCorrectAnswers.value == 3 ? this.alwaysShowAnswer : "";

    let quiz = new Quiz();
    quiz.title = this.Title.value;
    quiz.slug = this.Slug.value;
    quiz.description = this.Description.value;
    quiz.passingScore = this.PassingScore.value;
    quiz.showCorrectAnswer = showCorrectAnswer;
    quiz.randomiseQuestionOrder = this.RandomiseQuestionOrder.value;
    quiz.messageOnPass = this.MessageOnPass.value;
    quiz.messageOnFail = this.MessageOnFail.value;

    this.api.postQuiz(quiz)
      .subscribe({
        complete: async () => {
          this.resetForm(this.addQuizForm);
          await Swal.fire('Added', 'The Quiz has been added!', 'success');
        },
        error: async (err) => {
          await Swal.fire('Error', `There was an error while adding the quiz!\n${err}`, 'error')
        }
      });
  }

  resetForm(form: FormGroup) {
    form.reset();
  }
}
