using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LexiOWL.Domain.Entities
{
    public class TestAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string AnswerText { get; set; }

        [ForeignKey("Test")]
        public long TestId { get; set; }

        [InverseProperty("Answers")]
        public Test Test { get; set; }
    }
}
