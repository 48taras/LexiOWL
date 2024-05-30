using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;

namespace LexiOWL.Domain.Entities
{
    public class Statistics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [InverseProperty("Statistics")]
        public virtual List<EducationalContent> Contents { get; set; } = new List<EducationalContent>();
        
        [InverseProperty("Statistics")]
        public virtual List<Test> Tests { get; set; } = new List<Test>();

    }
}
