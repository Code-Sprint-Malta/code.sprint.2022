using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ViewModels;

namespace Application.Interfaces
{
    public interface IQuizService
    {
        public Task<bool> AddQuiz(QuizViewModel quiz);
        
        public Task<bool> AddQuestionsToQuiz(string slug, Dictionary<QuestionViewModel, AnswerViewModel> questions);

        public Task<QuizViewModel> GetQuiz(string slug);

        public Task<QuestionViewModel> GetQuestion(int questionId);

        public List<QuestionViewModel> GetQuestionsForQuiz(string slug);

        public List<AnswerViewModel> GetAnswersForQuestion(int questionId);

        public List<CategoryViewModel> GetCategories();
        public List<QuizViewModel> GetQuizzes();
    }
}