using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace LexiOWL.Domain.Entities
{
    public class Statistics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }

        public long TestId { get; set; }
        [ForeignKey("TestId")]
        public Test Test { get; set; }

        public DateTime TestCompletedAt { get; set; }

        public bool IsTestCorrect { get; set; }

        public long ContentId { get; set; }
        [ForeignKey("ContentId")]
        public EducationalContent Content { get; set; }

        public DateTime ContentViewedAt { get; set; }

        public bool IsContentCompleted { get; set; }
    }
}
