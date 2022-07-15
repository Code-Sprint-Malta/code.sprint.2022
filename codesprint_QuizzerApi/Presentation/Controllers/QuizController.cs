using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [EnableCors]
    [Route("/api/[action]")]
    public class QuizController : Controller
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddQuiz(QuizViewModel quiz)
        {
            var added = await _quizService.AddQuiz(quiz);
            if (!added) return Problem("Quiz not added");

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddQuestionsToQuiz(string slug, Dictionary<QuestionViewModel, AnswerViewModel> questions)
        {
            var added = await _quizService.AddQuestionsToQuiz(slug, questions);
            if (!added) return Problem("Questions not added");

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<QuizViewModel>> GetQuiz(string slug)
        {
            var quiz = await _quizService.GetQuiz(slug);
            if (quiz == null) return Problem("Problem while getting quiz");

            return Ok(quiz);
        }
        
        [HttpGet]
        public ActionResult<List<QuizViewModel>> GetQuizzes()
        {
            var quiz = _quizService.GetQuizzes();
            if (quiz == null) return Problem("Problem while getting quizzes");

            return Ok(quiz);
        }

        [HttpGet]
        public async Task<ActionResult<QuestionViewModel>> GetQuestion(int questionId)
        {
            var question = await _quizService.GetQuestion(questionId);
            if (question == null) return Problem("Problem while getting question");

            return Ok(question);
        }

        [HttpGet]
        public ActionResult<List<QuestionViewModel>> GetQuestionsForQuiz(string slug)
        {
            var questions = _quizService.GetQuestionsForQuiz(slug);
            if (questions == null) return Problem("Problem while getting questions");

            return Ok(questions);
        }

        [HttpGet]
        public ActionResult<List<AnswerViewModel>> GetAnswersForQuestion(int questionId)
        {
            var answers = _quizService.GetAnswersForQuestion(questionId);
            if (answers == null) return Problem("Problem while getting answers");

            return Ok(answers);
        }

        [HttpGet]
        public ActionResult<List<CategoryViewModel>> GetCategories()
        {
            var categories = _quizService.GetCategories();
            if (categories == null) return Problem("Problem while getting categories");

            return Ok(categories);
        }
    }
}