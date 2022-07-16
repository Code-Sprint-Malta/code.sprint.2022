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

        #region GET

        [HttpGet("{slug}")]
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
        
        [HttpGet("{slug}")]
        public ActionResult<List<QuestionViewModel>> GetQuestionsForQuiz(string slug)
        {
            var questions = _quizService.GetQuestionsForQuiz(slug);
            if (questions == null) return Problem("Problem while getting questions");

            return Ok(questions);
        }

        [HttpGet]
        public ActionResult<List<CategoryViewModel>> GetCategories()
        {
            var categories = _quizService.GetCategories();
            if (categories == null) return Problem("Problem while getting categories");

            return Ok(categories);
        }
        
        #endregion

        #region POST
        
        [HttpPost]
        public async Task<ActionResult<bool>> AddQuiz(QuizViewModel quiz)
        {
            var added = await _quizService.AddQuiz(quiz);
            if (!added) return Problem("Quiz not added");

            return Ok();
        }

        [HttpPost("{slug}")]
        public async Task<ActionResult<bool>> AddQuestionToQuiz(string slug, QuestionViewModel question)
        {
            var added = await _quizService.AddQuestionToQuiz(slug, question);
            if (!added) return Problem("Questions not added");

            return Ok(true);
        }
        
        #endregion

        #region DELETE
        
        [HttpDelete("{slug}")]
        public async Task<ActionResult<bool>> DeleteQuiz(string slug)
        {
            var deleted = await _quizService.DeleteQuiz(slug);
            if (!deleted) return Problem("Problem while deleting quiz");

            return Ok(true);
        }

        #endregion
    }
}