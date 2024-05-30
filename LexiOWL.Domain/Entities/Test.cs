using LexiOWL.Domain.Enums;
using System;
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

        public string Name { get; set; }

        public string UrlQuestionText { get; set; }

        public string CorrectAnswer { get; set; }

        public DateTime TestCompletedAt { get; set; }

        public bool IsTestCorrect { get; set; }

        public bool IsTestCompleted { get; set; }

        public QuestionType QuestionType {  get; set; }

        [ForeignKey("TopicId")]
        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }

        public int? StatisticsId { get; set; }

        [ForeignKey("StatisticsId")]
        public virtual Statistics Statistics { get; set; }

        [InverseProperty("Test")]
        public virtual List<TestAnswer> Answers { get; set; } = new List<TestAnswer>();
    }
}
