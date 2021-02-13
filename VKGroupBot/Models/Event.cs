using System;
using System.Collections.Generic;

#nullable disable

namespace VKGroupBot
{
    public partial class Event
    {
        public int EventId { get; set; }
        public string SubjectName { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
