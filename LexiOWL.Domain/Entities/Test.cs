using LexiOWL.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LexiOWL.Domain.Entities
{
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string UrlQuestionText { get; set; }

        public string CorrectAnswer { get; set; }

        public QuestionType QuestionType {  get; set; }

        [ForeignKey("TopicId")]
        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }

        [InverseProperty("Test")]
        public virtual List<TestAnswer> Answers { get; set; } = new List<TestAnswer>();
    }
}
