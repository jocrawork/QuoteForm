using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace QuoteForm
{
    public class EnumHelper
    {
        public static string GetDescription(Enum en)
        {//Found this here: http://weblogs.asp.net/grantbarrington/enumhelper-getting-a-friendly-description-from-an-enum

            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }
    }

    public enum EnumCategories
    {
        [Description("--Select a Category--")]
        Default = 0,
        
        Hardware,

        Accessories,

        Software,

        ContentCreation,

        Installation,
        
        Recurring
    }
}