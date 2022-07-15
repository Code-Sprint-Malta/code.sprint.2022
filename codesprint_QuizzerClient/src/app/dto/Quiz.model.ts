export class Quiz {
  private title: string = "";
  private slug: string = "";
  private description: string = "";
  private passingScore: number = 0;
  private showCorrectAnswers: number = 0;
  private randomiseQuestionOrder: boolean = false;
  private messageOnPass: string = "";
  private messageOnFail: string = "";

  public get Title(): string { return this.title; }
  public get Slug(): string { return this.slug; }
  public get Description(): string { return this.description; }
  public get PassingScore(): number { return this.passingScore; }
  public get ShowCorrectAnswers(): number { return this.showCorrectAnswers; }
  public get RandomiseQuestionOrder(): boolean { return this.randomiseQuestionOrder; }
  public get MessageOnPass(): string { return this.messageOnPass; }
  public get MessageOnFail(): string { return this.messageOnFail; }

  constructor(title: string, slug: string, description: string, passingScore: number,
              showCorrectAnswers: number, randomiseQuestionOrder: boolean, messageOnPass: string, messageOnFail: string) {
    this.title = title;
    this.slug = slug;
    this.description = description;
    this.passingScore = passingScore;
    this.showCorrectAnswers = showCorrectAnswers;
    this.randomiseQuestionOrder = randomiseQuestionOrder;
    this.messageOnPass = messageOnPass;
    this.messageOnFail = messageOnFail;
  }
}
