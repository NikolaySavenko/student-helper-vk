using System;
using System.Collections.Generic;

#nullable disable

namespace VKGroupBot
{
    public partial class Couple
    {
        public string CoupleUi { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string NameCoupleOdd { get; set; }
        public string NameCoupleEven { get; set; }
        public string TimeBreak { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
