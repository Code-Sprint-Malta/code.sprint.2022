import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./components/login/login.component";
import {ViewQuizComponent} from "./components/quiz/view-quiz/view-quiz.component";
import {AddQuizComponent} from "./components/quiz/add-quiz/add-quiz.component";
import {StartQuizComponent} from "./components/quiz/start-quiz/start-quiz.component";
import {ManageQuizComponent} from "./components/quiz/manage-quiz/manage-quiz.component";

const routes: Routes = [
  { path: 'signin-google', component: LoginComponent},
  { path: 'quizzes', component: ViewQuizComponent },
  { path: 'add-quiz', component: AddQuizComponent },
  { path: 'about', component: LoginComponent },
  { path: 'quizzes/:slug',  component: StartQuizComponent },
  { path: 'quizzes/manage/:slug',  component: ManageQuizComponent },
  { path: 'quizzes/:slug/:questionId',  component: StartQuizComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
