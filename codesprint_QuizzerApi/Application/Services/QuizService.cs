using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _repository;

        public QuizService(IQuizRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddQuiz(QuizViewModel quiz)
        {
            return await _repository.AddQuiz(new Quiz
            {
                Title = quiz.Title.Trim(),
                Slug = quiz.Slug.Trim().ToLower().Replace(" ", ""),
                Description = quiz.Description.Trim(),
                PassingScore = quiz.PassingScore,
                ShowCorrectAnswer = quiz.ShowCorrectAnswer,
                RandomiseQuestionOrder = quiz.RandomiseQuestionOrder,
                MessageOnPass = quiz.MessageOnPass,
                MessageOnFail = quiz.MessageOnFail
            });
        }

        public async Task<bool> AddQuestionsToQuiz(string slug, Dictionary<QuestionViewModel, AnswerViewModel> questions)
        {
            var questionsToPost = questions.ToDictionary(question => 
                new Question
                {
                    QuestionText = question.Key.QuestionText, 
                    QuestionTypeId = _repository.GetQuestionTypeId(question.Key.QuestionType).Result, 
                    QuizId = _repository.GetQuiz(slug).Result.QuizId,
                }, question => 
                new Answer
                {
                    AnswerText = question.Value.AnswerText,
                    IsCorrect = question.Value.IsCorrect,
                    Tag = question.Value.Tag,
                    QuestionId = _repository.GetQuestionIdForAnswer(question.Value.AnswerText, slug).Result
                });

            return await _repository.AddQuestionsToQuiz(slug, questionsToPost);
        }

        public async Task<QuizViewModel> GetQuiz(string slug)
        {
            var quiz = await _repository.GetQuiz(slug);
            if (quiz == null) return new QuizViewModel();

            return new QuizViewModel
            {
                Title = quiz.Title,
                Slug = quiz.Slug,
                Description = quiz.Description,
                PassingScore = quiz.PassingScore,
                MessageOnPass = quiz.MessageOnPass,
                MessageOnFail = quiz.MessageOnFail,
                RandomiseQuestionOrder = quiz.RandomiseQuestionOrder
            };
        }

        public async Task<QuestionViewModel> GetQuestion(int questionId)
        {
            var question = await _repository.GetQuestion(questionId);
            if (question == null) return new QuestionViewModel();

            return new QuestionViewModel
            {
                QuestionText = question.QuestionText,
                Category = question.Category.Name
            };
        }

        public List<QuestionViewModel> GetQuestionsForQuiz(string slug)
        {
            var questions =  _repository.GetQuestionsForQuiz(slug);
            if (questions == null) return new List<QuestionViewModel>();

            var toReturn = new List<QuestionViewModel>();
            questions.ForEach(q => toReturn.Add(new QuestionViewModel
            {
                QuestionText = q.QuestionText,
                Category = q.Category.Name,
                QuestionType = q.Type.Type
            }));

            return toReturn;
        }

        public List<AnswerViewModel> GetAnswersForQuestion(int questionId)
        {
            var answers = _repository.GetAnswersForQuestion(questionId);
            if (answers == null) return new List<AnswerViewModel>();
            
            var toReturn = new List<AnswerViewModel>();
            answers.ForEach(a => toReturn.Add(new AnswerViewModel
            {
                AnswerText = a.AnswerText,
                IsCorrect = a.IsCorrect,
                Score = a.Score
            }));

            return toReturn;
        }

        public List<CategoryViewModel> GetCategories()
        {
            var categories = _repository.GetCategories();
            if (categories == null) return new List<CategoryViewModel>();
            
            var toReturn = new List<CategoryViewModel>();
            categories.ForEach(c => toReturn.Add(new CategoryViewModel
            {
               Name = c.Name
            }));

            return toReturn;
        }

        public List<QuizViewModel> GetQuizzes()
        {
            var quizzes = _repository.GetQuizzes();
            if (quizzes == null) return new List<QuizViewModel>();
            
            var toReturn = new List<QuizViewModel>();
            quizzes.ForEach(quiz => toReturn.Add(new QuizViewModel()
            {
                Title = quiz.Title,
                Slug = quiz.Slug,
                Description = quiz.Description,
                PassingScore = quiz.PassingScore,
                MessageOnPass = quiz.MessageOnPass,
                MessageOnFail = quiz.MessageOnFail,
                RandomiseQuestionOrder = quiz.RandomiseQuestionOrder
            }));

            return toReturn;
        }
    }
}