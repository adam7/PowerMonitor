using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerMonitor.Web.Extensions
{
    public static class StringExtensions
    {
        public static string Tail(this string text, int length)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= length)
                return text;
            else
            {
                return text.Substring(text.Length - length);
            }
        }
    }
}