export interface IQuiz {
  title: string;
  slug: string;
  description: string;
  passingScore: number;
  showCorrectAnswer: string;
  randomiseQuestionOrder: boolean;
  messageOnPass: string;
  messageOnFail: string;
}
export class Quiz implements IQuiz {
  title: string;
  slug: string;
  description: string;
  passingScore: number;
  showCorrectAnswer: string;
  randomiseQuestionOrder: boolean;
  messageOnPass: string;
  messageOnFail: string;
}
