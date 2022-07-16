using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizContext _context;

        public QuizRepository(QuizContext context)
        {
            _context = context;
        }

        #region GET

        public async Task<Quiz> GetQuiz(string slug)
        {
            try
            {
                return await _context.Quizzes.FirstOrDefaultAsync(quiz => quiz.Slug.Equals(slug));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Quiz> GetQuizzes()
        {
            try
            {
                return _context.Quizzes.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<Question> GetQuestion(string slug, string questionText)
        {
            try
            {
                var quiz = await GetQuiz(slug);
                return await _context.Questions.FirstOrDefaultAsync(question => question.QuizId == quiz.QuizId && question.QuestionText.Equals(questionText));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Question> GetQuestionsForQuiz(string slug)
        {
            try
            {
                var quiz = GetQuiz(slug).Result;
                if (quiz == null) return null;
                
                return _context.Questions.Where(q => q.QuizId == quiz.QuizId).ToList();
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public async Task<int> GetQuestionTypeId(string questionType)
        {
            try
            {
                var qType = await _context.QuestionTypes.FirstOrDefaultAsync(qt => qt.Type.Equals(questionType));
                if (qType == null) await AddQuestionType(questionType);
                qType = await _context.QuestionTypes.FirstOrDefaultAsync(qt => qt.Type.Equals(questionType));
                
                return qType!.TypeId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<Answer> GetAnswersForQuestion(string slug, string questionText)
        {
            try
            {
                var question = GetQuestion(slug, questionText).Result;
                if (question == null) return null;
                
                return _context.Answers.Where(a => a.QuestionId == question.QuestionId).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> GetCategoryId(string category)
        {
            try
            {
                var cat = await _context.Categories.FirstOrDefaultAsync(c => c.Name.Equals(category));
                if (cat == null) await AddCategory(category);
                cat = await _context.Categories.FirstOrDefaultAsync(c => c.Name.Equals(category));

                return cat!.CategoryId;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        
        public List<Category> GetCategories()
        {
            try
            {
                return _context.Categories.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        

        #endregion

        #region POST

        public async Task<bool> AddQuiz(Quiz quiz)
        {
            try
            {
                await _context.Quizzes.AddAsync(quiz);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<int> AddQuestionToQuiz(string slug, Question question)
        {
            try
            {
                var quiz = _context.Quizzes.FirstOrDefault(quiz => quiz.Slug.Equals(slug));
                if (quiz == null) return -1;

                await _context.Questions.AddAsync(question);
                await _context.SaveChangesAsync();

                var questionEntity = _context.Entry(question);
                return questionEntity.Entity.QuestionId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<bool> AddAnswersToQuiz(List<Answer> answers)
        {
            try
            {
                answers.ForEach(async answer => await _context.Answers.AddAsync(answer));
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private async Task AddQuestionType(string questionType)
        {
            try
            {
                await _context.QuestionTypes.AddAsync(new QuestionType { Type = questionType });
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        private async Task AddCategory(string category)
        {
            try
            {
                await _context.Categories.AddAsync(new Category() { Name = category });
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        #endregion

        #region DELETE

        public async Task<bool> DeleteQuiz(int quizId)
        {
            try
            {
                var relatedAnswers = _context.Answers.Where(a => a.QuizId == quizId);
                var relatedQuestions = _context.Questions.Where(q => q.QuizId == quizId);
                await relatedAnswers.ForEachAsync(ans => _context.Answers.Remove(ans));
                await relatedQuestions.ForEachAsync(que => _context.Questions.Remove(que));
                
                var quiz = await _context.Quizzes.FindAsync(quizId);
                _context.Quizzes.Remove(quiz);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}