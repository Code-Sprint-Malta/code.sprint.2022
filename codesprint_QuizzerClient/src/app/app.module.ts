import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { HeaderComponent } from './components/header/header.component';
import { AddQuizComponent } from './components/quiz/add-quiz/add-quiz.component';
import { ViewQuizComponent } from './components/quiz/view-quiz/view-quiz.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import {AuthenticationService} from "./services/auth/authentication.service";
import { ManageQuizComponent } from './components/quiz/manage-quiz/manage-quiz.component';
import { StartQuizComponent } from './components/quiz/start-quiz/start-quiz.component';
import { QuestionQuizComponent } from './components/quiz/question-quiz/question-quiz.component';
import {QuizApiService} from "./services/quiz/quiz-api.service";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HeaderComponent,
    AddQuizComponent,
    ViewQuizComponent,
    ManageQuizComponent,
    StartQuizComponent,
    QuestionQuizComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [
    AuthenticationService,
    QuizApiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
