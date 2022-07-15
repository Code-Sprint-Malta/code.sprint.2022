using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IQuizRepository
    {
        public Task<bool> AddQuiz(Quiz quiz);
        public Task<bool> AddQuestionsToQuiz(string slug, Dictionary<Question, Answer> questions);
        public Task<Quiz> GetQuiz(string slug);
        public Task<Question> GetQuestion(int questionId);
        public Task<int> GetQuestionIdForAnswer(string answerText, string slug);
        public List<Question> GetQuestionsForQuiz(string slug);
        public Task<int> GetQuestionTypeId(string questionType);
        public List<Answer> GetAnswersForQuestion(int questionId);
        public List<Category> GetCategories();
        public List<Quiz> GetQuizzes();
    }
}