using Framework.Core.Common.DateUtilities;
using System;

namespace Framework.Core.Common.Extensions
{
	public static class DateExtensions
	{
		public static string ToLongStringPersianDate(this DateTime date)
		{
			return ConvertDate.ToFa(date, "D");
		}

		public static string ToShortStringPersianDate(this DateTime date)
		{
			return ConvertDate.ToFa(date);
		}

		public static string ToStringPersianWithFormat(this DateTime date, string format)
		{
			return ConvertDate.ToFa(date, format);
		}

		public static string ToLongStringPersianTime(this DateTime date)
		{
			return ConvertDate.ToFa(date, "T");
		}

		public static string ToShortStringPersianTime(this DateTime date)
		{
			return ConvertDate.ToFa(date, "t");
		}

		public static string ToStringPersian(this DateTime date)
		{
			return ConvertDate.ToFa(date, "F");
		}

		public static DateTime ToDateGregorian(this string persianDate)
		{
			return ConvertDate.ToEn(persianDate);
		}
	}
}