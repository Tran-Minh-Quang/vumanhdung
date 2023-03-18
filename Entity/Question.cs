using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team12EUP.Entity
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get;set; }
        public int Answer { get;set; }
        [ForeignKey("TestId")]
        public Guid TestId { get; set; }
        public string? Answer1 { get; set; }
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
    }
}
