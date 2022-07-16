export interface IAnswer {
  answerText: string;
  isCorrect: boolean;
  score: number;
  tag: string;
}

export class Answer implements IAnswer {
  answerText: string;
  isCorrect: boolean;
  score: number;
  tag: string;
}
