using System;
using System.Collections.Generic;

#nullable disable

namespace VKGroupBot
{
    public partial class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool? Subscription { get; set; }
        public bool? Admin { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
