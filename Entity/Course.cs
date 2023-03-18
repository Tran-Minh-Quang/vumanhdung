using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team12EUP.Entity
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }    
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
