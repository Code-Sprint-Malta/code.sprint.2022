import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Quiz} from "../../dto/Quiz.model";
import {BASE_URL, POST_QUIZ} from "../../dto/constants";

@Injectable({
  providedIn: 'root'
})
export class QuizApiService {

  constructor(private http: HttpClient) { }

  async postQuiz(quiz: Quiz) {
    this.http.post<Quiz>(BASE_URL + POST_QUIZ, quiz);
  }
}
