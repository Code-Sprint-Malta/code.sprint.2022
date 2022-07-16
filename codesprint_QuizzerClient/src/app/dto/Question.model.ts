import { IAnswer } from "./Answer.model";

export interface IQuestion {
  questionType: string;
  questionText: string;
  category: string;
  answers: IAnswer[]
}

export class Question implements IQuestion {
  questionType: string;
  questionText: string;
  category: string;
  answers: IAnswer[]
}
