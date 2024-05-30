using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LexiOWL.Domain.Entities
{
    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Age { get; set; }

        public string AmountOfKnowledge { get; set; }

        public string TrainingIntensity { get; set; }

        public bool Notification { get; set; }

        public string ProfilePhotoPath { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
