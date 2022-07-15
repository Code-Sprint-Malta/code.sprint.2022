namespace Application.ViewModels
{
    public class AnswerViewModel
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public byte Score { get; set; }
        public string Tag { get; set; }
    }
}