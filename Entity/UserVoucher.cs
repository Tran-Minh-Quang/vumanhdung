using System.ComponentModel.DataAnnotations;

namespace Team12EUP.Entity
{
    public class UserVoucher
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VoucherId { get; set; }
    }
}
