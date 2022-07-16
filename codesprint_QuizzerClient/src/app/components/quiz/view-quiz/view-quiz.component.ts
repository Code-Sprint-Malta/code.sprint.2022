import { Component, OnInit } from '@angular/core';
import {QuizApiService} from "../../../services/quiz/quiz-api.service";
import {IQuiz} from "../../../dto/Quiz.model";
import Swal from "sweetalert2";

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

  deleteQuiz(quizNumber: number) {
    let slug: string = this.quizzes[quizNumber].slug;

    // getting the quiz
    this.api.getQuiz(slug)
      .subscribe(quiz => slug = quiz.slug);

    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {

      // deleting the quiz only if the user confirms the modal
      if (result.isConfirmed) {
        this.api.deleteQuiz(slug)
          .subscribe(deleted => {
            if (deleted)
              Swal.fire(
                'Deleted!',
                'The quiz has been deleted.',
                'success'
              );
          });
      }
    })
  }
}
