using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace System
{
    public static partial class ExpandMethod
    {
        /// <summary>
        /// 字符串转成整型数组
        /// </summary>
        public static int[] ConvertToIntArray(this string[] value)
        {
            if (value == null)
            {
                return null;
            }
            int[] array = new int[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = Convert.ToInt32(value[i]);
            }
            return array;
        }

        /// <summary>
        /// 集合拼接成字符串
        /// </summary>
        public static string ConvertToString<T>(this IList<T> list, string split)
        {
            if (list != null)
            {
                StringBuilder str = new StringBuilder();
                foreach (var item in list)
                {
                    str.AppendFormat("{0}{1}", item, split);
                }
                string value = str.ToString();
                if (value.Length > 0)
                {
                    value = value.Remove(value.Length - split.Length);
                }
                return value;
            }
            return "";
        }

        public static int[] ConvertToIntArray(this string value, char split)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                string[] str_array = value.Split(new char[] { split }, StringSplitOptions.RemoveEmptyEntries);
                int[] array = new int[str_array.Length];
                for (int i = 0; i < str_array.Length; i++)
                {
                    array[i] = int.Parse(str_array[i]);
                }
                return array;
            }
            return null;
        }

        /// <summary>
        /// 获取字符窜后缀名
        /// </summary>
        /// <param name="path"></param>
        /// <returns>.xxx</returns>
        public static string GetFileSuffix(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }
            else
            {
                return path.Substring(path.LastIndexOf('.'));
            }
        }

    }
}