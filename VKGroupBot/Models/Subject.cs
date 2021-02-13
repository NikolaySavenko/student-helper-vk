using System;
using System.Collections.Generic;

#nullable disable

namespace VKGroupBot
{
    public partial class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
