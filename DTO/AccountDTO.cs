﻿using System.Text.Json.Serialization;

namespace Team12EUP.DTO
{
    public class AccountDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
    public class AccountDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string FullName { get;set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [JsonIgnore]
        public int Role { get; set; }
    }
}
