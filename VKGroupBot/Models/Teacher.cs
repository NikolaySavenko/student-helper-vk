using System;
using System.Collections.Generic;

#nullable disable

namespace VKGroupBot
{
    public partial class Teacher
    {
        public int TeacherId { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public string KindOfActivity { get; set; }
        public string TeacherMail { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
