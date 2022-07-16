using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ViewModels;

namespace Application.Interfaces
{
    public interface IQuizService
    {
        public Task<bool> AddQuiz(QuizViewModel quiz);
        
        public Task<bool> AddQuestionToQuiz(string slug, QuestionViewModel question);

        public Task<QuizViewModel> GetQuiz(string slug);

        public List<QuestionViewModel> GetQuestionsForQuiz(string slug);
        
        public List<CategoryViewModel> GetCategories();
        public List<QuizViewModel> GetQuizzes();
        public Task<bool> DeleteQuiz(string slug);
    }
}