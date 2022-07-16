import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormArray, FormControl, FormGroup, Validators} from "@angular/forms";
import {QuizApiService} from "../../../services/quiz/quiz-api.service";
import {ActivatedRoute, Router} from "@angular/router";
import {IQuiz, Quiz} from "../../../dto/Quiz.model";
import Swal from "sweetalert2";
import {ICategory} from "../../../dto/Category.model";
import {IQuestion} from "../../../dto/Question.model";

@Component({
  selector: 'app-manage-quiz',
  templateUrl: './manage-quiz.component.html',
  styleUrls: ['./manage-quiz.component.css']
})
export class ManageQuizComponent implements OnInit {
  //region Form Controls Getters

  public get QuestionType(): AbstractControl { return this.manageQuizForm.get('questionType'); }
  public get Question(): AbstractControl { return this.manageQuizForm.get('questionText'); }
  public get Category(): AbstractControl { return this.manageQuizForm.get('category'); }
  public get Answers(): FormArray { return this.manageQuizForm.get('answers') as FormArray; }

  //endregion

  singleAnswer = "Single Answer";
  multipleAnswer = "Multiple Answer";

  title: string = "";
  quiz: IQuiz = new Quiz();
  categories: ICategory[] = [];

  //region Reactive Form Initialisation and Validation

  manageQuizForm: FormGroup = new FormGroup({
    // required
    questionType: new FormControl(this.singleAnswer, Validators.required),

    // required, required, 0 or more uppercase/lowercase characters including digits and special characters, no new lines
    questionText: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[a-zA-Z0-9].*$/)
    ]),

    // characters and whitespaces only
    category: new FormControl('', [
      Validators.pattern(/^([a-zA-Z]+\s)*[a-zA-Z]+$/)
    ]),

    // this is an array of answers which will be added to the quiz
    // when the user adds a new answer, it will be added as a form group in this array
    answers: new FormArray([]),
  });

  //endregion

  constructor(private router: Router, private actRoute: ActivatedRoute, public api: QuizApiService) { }

  ngOnInit(): void {
    // adding 2 empty options
    this.addOption();
    this.addOption();

    let slug = this.actRoute.snapshot.params['slug'];
    this.api.getCategories()
      .subscribe(categories => this.categories = categories);

    this.api.getQuiz(slug)
      .subscribe(quiz => this.quiz = quiz);
  }

  submitQuestions() {
    let question: IQuestion = this.manageQuizForm.value as IQuestion;
    question.category = question.category == null ? "" : question.category;

    // radio input was always returning undefined when checked
    question.answers.forEach(answer => {
      if (answer.isCorrect === undefined) answer.isCorrect = true;
    })

    // posting the question and its answers
    this.api.postQuestion(this.quiz.slug, question)
      .subscribe({
        complete: async () => {
          this.resetManageQuizForm();
          await Swal.fire('Added', 'The Question has been added!', 'success');
        },
        error: async () => {
          await Swal.fire('Error', `There was an error while adding the question!`, 'error')
      }
    });
  }

  newOption() : FormGroup {
    return new FormGroup({
      // required
      tag: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(1),
        Validators.pattern(/^[A-Z]$/)
      ]),

      // required, digits only, between 1 and 3 digits
      score: new FormControl('', [
        Validators.required,
        Validators.pattern(/^[\d+]{1,3}$/)
      ]),
      isCorrect: new FormControl(false),

      // required, required, 0 or more uppercase/lowercase characters including digits and special characters, no new lines
      answerText: new FormControl('', [
        Validators.required,
        Validators.pattern(/^[a-zA-Z0-9].*$/)
      ])
    });
  }

  addOption() {
    this.Answers.push(this.newOption());
  }

  removeOption(index: number) {
    this.Answers.removeAt(index);
  }

  resetManageQuizForm() {
    this.manageQuizForm.reset();
    this.QuestionType.setValue(this.singleAnswer);

    // resetting the answers array to make sure there are only 2 answer options
    this.Answers.clear();
    this.addOption()
    this.addOption()
  }
}
