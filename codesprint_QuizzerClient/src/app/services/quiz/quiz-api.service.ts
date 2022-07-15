import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {IQuiz} from "../../dto/Quiz.model";
import {BASE_URL, GET_QUIZZES, GET_QUIZ, POST_QUIZ, GET_CATEGORIES} from "../../dto/constants";
import {Observable} from "rxjs";
import {ICategory} from "../../dto/Category.model";

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
    return this.http.get<IQuiz>(BASE_URL + GET_QUIZ, {
        params: new HttpParams().append('slug', slug)
      });
  }

  getCategories(): Observable<ICategory[]> {
    return this.http.get<ICategory[]>(BASE_URL + GET_CATEGORIES);
  }
}
