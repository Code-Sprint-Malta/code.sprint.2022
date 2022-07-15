using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Quiz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuizId { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Slug { get; set; }
        
        [Required] 
        public string Description { get; set; }
        
        [Required] 
        public byte PassingScore { get; set; }
        
        [Required]
        public string ShowCorrectAnswer { get; set; }

        public bool RandomiseQuestionOrder { get; set; }

        [Required]
        public string MessageOnPass { get; set; }

        [Required]
        public string MessageOnFail { get; set; }
    }
}