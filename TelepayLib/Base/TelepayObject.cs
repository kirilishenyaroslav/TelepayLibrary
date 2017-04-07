using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TelepayLib.Base
{
    public class TelepayObject
    {
        public static string EmptyString(int length)
        {
            return string.Join(" ", new string[length + 1]);
        }

        public static string EmptyNum(int length)
        {
            return string.Join("0", new string[length + 1]);
        }

        public override string ToString()
        {
            var properties =
                this.GetType().GetProperties().Where(prop => prop.IsDefined(typeof (TelepayFieldAttribute), true))
                    .Select(p =>
                        {
                            var attr =
                                p.GetCustomAttributes(typeof (TelepayFieldAttribute), true).FirstOrDefault() as
                                TelepayFieldAttribute;
                            return new {value = GetPropertyValue(p, attr), attr = attr};
                        }).OrderBy(r => r.attr.Position).Select(r => r.value).ToList();

            var DataStr = string.Join("", properties);
            return DataStr;
        }

        protected virtual object GetPropertyValue(PropertyInfo prop, TelepayFieldAttribute attr)
        {
            var value = prop.GetValue(this, null) ?? "";

            var empty = attr.Length.Value - value.ToString().Length;
            switch (attr.Type)
            {
                case FieldType.Alpha:
                    value += EmptyString(empty);
                    break;
                case FieldType.Alphanumeric:
                    value += EmptyNum(empty);
                    break;
                case FieldType.Numeric:
                    long val = 0;
                    try
                    {
                        val = Convert.ToInt64(value);
                    }
                    catch
                    {
                    }
                    //int.TryParse(value.ToString(), out val);
                    value = val.ToString("d" + attr.Length);
                    break;
                default:
                    value = value.ToString();
                    break;
            }

            if (value.ToString().Length > attr.Length)
                throw new ArgumentException(string.Concat("The values of the ", prop.ReflectedType.Name, ".", prop.Name,
                                                          " property is too long."));
            return value;
        }
    }
}