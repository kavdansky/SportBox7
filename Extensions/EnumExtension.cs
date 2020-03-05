using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Extensions
{
    public static class EnumExtensions
    {





        public static SelectListItem GetDescription(this Enum GenericEnum)
        {

            SelectListItem result = new SelectListItem();
            Type genericEnumType = GenericEnum?.GetType();
            System.Reflection.MemberInfo[] memberInfo =
                        genericEnumType.GetMember(GenericEnum.ToString());

            if ((memberInfo != null && memberInfo.Length > 0))
            {

                dynamic _Attribs = memberInfo[0].GetCustomAttributes
                      (typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Length > 0))
                {
                    result.Text = ((System.ComponentModel.DescriptionAttribute)_Attribs[0]).Description;
                    result.Value = ((System.ComponentModel.DescriptionAttribute)_Attribs[0]).ToString();
                    return result;
                }
            }

            return result;
        }
    }
}
