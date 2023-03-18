using System.ComponentModel.DataAnnotations;

namespace Team12EUP.Entity
{
    public class Voucher
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description{get;set;}
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string DetailContent { get; set; }
    }
}
