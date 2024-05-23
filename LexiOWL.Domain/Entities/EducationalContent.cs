using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LexiOWL.Domain.Entities
{
    public class EducationalContent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string UrlEducationalTextContent { get; set; }

        public string UrlEducationalVideoContent { get; set; }

        [ForeignKey("TopicId")]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
