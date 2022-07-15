namespace Application.ViewModels
{
    public class QuizViewModel
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public byte PassingScore { get; set; }
        public string ShowCorrectAnswer { get; set; }
        public bool RandomiseQuestionOrder { get; set; }
        public string MessageOnPass { get; set; }
        public string MessageOnFail { get; set; }
    }
}