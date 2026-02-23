using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Top5.Domain.Models;

namespace Top5.Domain.Entities
{
    public class Token
    {
        public Guid id {  get; set; }
        public Guid playerId { get; set; }
        public string hashedToken { get; set; }
        public DateTime expiresAt { get; set; }
        public DateTime createdAt { get; set; }
        public bool isRevoked { get; set; } = false;
        public DateTime? revokedAt { get; set; }

        [JsonIgnore]
        public Player player { get; set; }

    }
}
