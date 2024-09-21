using System;
using System.Collections.Generic;

namespace WalletScanner.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Navigation properties
        public ICollection<Alert> Alerts { get; set; }
    }
}
