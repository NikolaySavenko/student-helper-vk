using System;
using System.Collections.Generic;

#nullable disable

namespace VKGroupBot
{
    public partial class Link
    {
        public string SubjectName { get; set; }
        public string LecturesCode { get; set; }
        public string LecturesPassword { get; set; }
        public string LecturesLink { get; set; }
        public string PracticesCode { get; set; }
        public string PracticesPassword { get; set; }
        public string PracticesLink { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
