using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LexiOWL.Domain.Entities
{
    public class EducationalContent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string UrlEducationalTextContent { get; set; }

        public string UrlEducationalVideoContent { get; set; }

        public DateTime ContentViewedAt { get; set; }

        public bool IsContentCompleted { get; set; }

        [ForeignKey("TopicId")]
        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }

        public int? StatisticsId { get; set; }

        [ForeignKey("StatisticsId")]
        public virtual Statistics Statistics { get; set; }

        public TimeSpan TimeSpent { get; set; } = TimeSpan.Zero;
        public bool IsTimerRunning { get; set; } = false;
    }
}
