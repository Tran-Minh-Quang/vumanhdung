using System.ComponentModel.DataAnnotations;

namespace Team12EUP.Entity
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime? Dob { get; set; }
        public int Role { get; set; } // Role 1:User   2:Member     3:Manager
        public string? Email { get; set; }
        public string? Address { get;set; }
        public string? PhoneNumber { get; set; }
        public int? Gender { get;set; }

    }
}
