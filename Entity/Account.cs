using System.ComponentModel.DataAnnotations;

namespace Team12EUP.Entity
{
    public class Account
    {
        [Key]
        public Guid Id {  get; set; }
        public string UserName { get;set; }
        public string Password { get;set; }

    }
}
