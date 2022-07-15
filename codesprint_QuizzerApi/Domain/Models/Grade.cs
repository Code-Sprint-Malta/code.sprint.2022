using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Grade
    {
        [Key]
        [StringLength(2, ErrorMessage = "Grade cannot have more than 2 characters")]
        public string GradeId { get; set; }

        public byte MinimumMark { get; set; }
        public byte MaximumMark { get; set; }
    }
}