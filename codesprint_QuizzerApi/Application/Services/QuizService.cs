using System.Collections.Generic;
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

        #region GET

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

        public List<QuizViewModel> GetQuizzes()
        {
            var quizzes = _repository.GetQuizzes();
            if (quizzes == null) return new List<QuizViewModel>();
            
            var toReturn = new List<QuizViewModel>();
            quizzes.ForEach(quiz => toReturn.Add(new QuizViewModel
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
        
        public List<QuestionViewModel> GetQuestionsForQuiz(string slug)
        {
            var questions =  _repository.GetQuestionsForQuiz(slug);
            if (questions == null) return new List<QuestionViewModel>();

            var toReturn = new List<QuestionViewModel>();
            questions.ForEach(q => toReturn.Add(new QuestionViewModel
            {
                QuestionText = q.QuestionText,
                Category = q.Category.Name,
                QuestionType = q.Type.Type,
                Answers = GetAnswersForQuestion(slug, q.QuestionText)
            }));

            return toReturn;
        }

        private List<AnswerViewModel> GetAnswersForQuestion(string slug, string questionText)
        {
            var answers = _repository.GetAnswersForQuestion(slug, questionText);
            if (answers == null) return new List<AnswerViewModel>();
            
            var toReturn = new List<AnswerViewModel>();
            answers.ForEach(a => toReturn.Add(new AnswerViewModel
            {
                AnswerText = a.AnswerText,
                IsCorrect = a.IsCorrect,
                Tag = a.Tag,
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

        #endregion

        #region POST

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

        public async Task<bool> AddQuestionToQuiz(string slug, QuestionViewModel question)
        {
            
            var questionToPost = new Question
            {
                QuestionText = question.QuestionText,
                QuestionTypeId = _repository.GetQuestionTypeId(question.QuestionType).Result,
                QuizId = _repository.GetQuiz(slug).Result.QuizId,
                CategoryId = _repository.GetCategoryId(question.Category).Result
            };

            var questionId = await _repository.AddQuestionToQuiz(slug, questionToPost);
            if (questionId == -1) return false;
            
            var answersToPost = new List<Answer>();
            question.Answers.ForEach(answer =>
            {
                answersToPost.Add(new Answer
                {
                    AnswerText = answer.AnswerText,
                    IsCorrect = answer.IsCorrect,
                    Score = answer.Score,
                    Tag = answer.Tag,
                    QuestionId = questionId,
                    QuizId = _repository.GetQuiz(slug).Result.QuizId
                });
            });

            return await _repository.AddAnswersToQuiz(answersToPost);
        }

        #endregion

        #region DELETE

        public async Task<bool> DeleteQuiz(string slug)
        {
            var quiz = await _repository.GetQuiz(slug);
            if (quiz == null) return false;

            return await _repository.DeleteQuiz(quiz.QuizId);
        }

        #endregion
    }
}