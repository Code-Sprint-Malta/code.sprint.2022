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

        public async Task<bool> AddQuestionsToQuiz(string slug, Dictionary<Question, Answer> questions)
        {
            try
            {
                var quiz = _context.Quizzes.FirstOrDefault(quiz => quiz.Equals(slug));
                if (quiz == null) return false;

                foreach (var question in questions)
                {
                    question.Key.QuizId = quiz.QuizId;
                    await _context.Questions.AddAsync(question.Key);
                }
                await _context.SaveChangesAsync();

                foreach (var answer in questions)
                {
                    answer.Value.QuestionId = answer.Key.QuestionId;
                    await _context.Answers.AddAsync(answer.Value);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<Quiz> GetQuiz(string slug)
        {
            try
            {
                return await _context.Quizzes.FirstOrDefaultAsync(quiz => quiz.Slug.Equals(slug));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Question> GetQuestion(int questionId)
        {
            try
            {
                return await _context.Questions.FirstOrDefaultAsync(question => question.QuestionId == questionId);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        /// <summary>
        /// This method returns the question id for a question given the answer text and slug.
        /// </summary>
        /// <param name="answerText">The answer text of the question</param>
        /// <param name="slug">The quiz slug</param>
        /// <returns>Question ID -> int</returns>
        public async Task<int> GetQuestionIdForAnswer(string answerText, string slug)
        {
            try
            {
                var answer = await _context.Answers.FirstOrDefaultAsync(a => a.AnswerText.Equals(answerText) && a.Quiz.Slug.Equals(slug));
                return answer.Question.QuestionId;
            }
            catch (Exception)
            {
                return -1;
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
            catch (Exception e)
            {
                return null;
            }
            
        }

        public async Task<int> GetQuestionTypeId(string questionType)
        {
            try
            {
                var qType = await _context.QuestionTypes.FirstOrDefaultAsync(qt => qt.Type.Equals(questionType));
                if (qType == null) return -1;

                return qType.TypeId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<Answer> GetAnswersForQuestion(int questionId)
        {
            try
            {
                var question = GetQuestion(questionId).Result;
                if (question == null) return null;
                
                return _context.Answers.Where(a => a.QuestionId == question.QuestionId).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Category> GetCategories()
        {
            try
            {
                return _context.Categories.ToList();
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                return null;
            }
        }
    }
}