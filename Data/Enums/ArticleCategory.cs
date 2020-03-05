using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Enums
{
    public enum ArticleCategory
    {
        [Description("Други")]
        Uncategorized = 0,

        [Description("Футбол България")]
        FootBallBulgaria = 1,

        [Description("Футбол свят")]
        FootBallWorld = 2,

        [Description("Бойни спортове")]
        Martial = 3,

        [Description("Други")]
        OtherSports = 10

    }
}
