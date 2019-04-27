using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social
{
    public enum PayLevel
    {
        UNSET = -1,
        VeryPoor = 0,
        Poor = 1,
        UpperPoor = 2,
        LowerMiddle = 3,
        Middle = 4,
        UpperMidle = 5,
        LowerRich = 6,
        Rich = 7,
        VeryRich = 8
    }

    public enum EducationLevel
    {
        UNSET = -1,
        Illiterate = 0,
        Literate = 1,
        Basic = 2,
        Higher = 3
    }

    class SocialHelper
    {
    }
}
