using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IQuizRepository
    {
        public Task<bool> AddQuiz(Quiz quiz);
        public Task<int> AddQuestionToQuiz(string slug, Question question);
        public Task<bool> AddAnswersToQuiz(List<Answer> answers);
        public Task<Quiz> GetQuiz(string slug);
        public List<Question> GetQuestionsForQuiz(string slug);
        public Task<int> GetQuestionTypeId(string questionType);
        public List<Answer> GetAnswersForQuestion(string slug, string questionText);
        public List<Category> GetCategories();
        public List<Quiz> GetQuizzes();
        public Task<int> GetCategoryId(string category);
        public Task<bool> DeleteQuiz(int quizId);
    }
}