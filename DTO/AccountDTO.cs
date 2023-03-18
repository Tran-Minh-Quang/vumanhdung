using System.Text.Json.Serialization;

namespace Team12EUP.DTO
{
    public class AccountDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get;set; }
    }
}
