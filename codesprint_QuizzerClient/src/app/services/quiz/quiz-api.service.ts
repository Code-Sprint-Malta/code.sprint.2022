import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IQuiz} from "../../dto/Quiz.model";
import {
  BASE_URL,
  GET_QUIZZES,
  GET_QUIZ,
  POST_QUIZ,
  GET_CATEGORIES,
  DELETE_QUIZ, POST_QUESTION, GET_QUESTIONS
} from "../../dto/constants";
import {Observable} from "rxjs";
import {ICategory} from "../../dto/Category.model";
import { IQuestion } from 'src/app/dto/Question.model';

@Injectable({
  providedIn: 'root'
})
export class QuizApiService {

  constructor(private http: HttpClient) { }

  postQuiz(quiz: IQuiz) {
    return this.http.post<IQuiz>(BASE_URL + POST_QUIZ, quiz);
  }

  getQuizzes(): Observable<IQuiz[]> {
    return this.http.get<IQuiz[]>(BASE_URL + GET_QUIZZES);
  }

  getQuiz(slug: string): Observable<IQuiz> {
    return this.http.get<IQuiz>(`${BASE_URL}${GET_QUIZ}/${slug}`);
  }

  getCategories(): Observable<ICategory[]> {
    return this.http.get<ICategory[]>(BASE_URL + GET_CATEGORIES);
  }

  postQuestion(slug: string, question: IQuestion) {
    return this.http.post<IQuestion>(`${BASE_URL}${POST_QUESTION}/${slug}`, question);
  }

  deleteQuiz(slug: string) {
    return this.http.delete<boolean>(`${BASE_URL}${DELETE_QUIZ}/${slug}`);
  }

  getQuestions(slug: string) {
    return this.http.get<IQuestion[]>(`${BASE_URL}${GET_QUESTIONS}/${slug}`);
  }
}
