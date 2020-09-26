using System;
using System.Globalization;

namespace Framework.Core.Common.DateUtilities
{
	internal static class ConvertDate
	{
		#region Public Members

		#region Gregorian

		public static DateTime ToEn(string fadate)
		{
			if (string.IsNullOrEmpty(fadate))
			{
				return DateTime.MinValue;
			}

			int[] farsiPartArray = SplitRoozMahSalNew(fadate);

			return new PersianCalendar().ToDateTime(farsiPartArray[0], farsiPartArray[1], farsiPartArray[2], 0, 0, 0, 0);
		}

		public static DateTime ToEn(int y, int m, int d)
		{
			if (y < 100 | y > 3000 | m < 0 | m > 12 | d < 0 | d > 33)
			{
				return DateTime.MinValue;
			}

			return new PersianCalendar().ToDateTime(y, m, d, 0, 0, 0, 0);
		}

		#endregion Gregorian

		#region Persian

		public static string ToFa(DateTime dateTime)
		{
			return ToFa(dateTime, "B");
		}

		public static string ToFa(DateTime? dateTime, string format)
		{
			if (!dateTime.HasValue)
			{
				return string.Empty;
			}

			ShamsiDate sd = ToShamsiDate(dateTime.Value);

			if (format.Length == 1)
			{
				switch (format)
				{
					case "d":
						return sd.ShortDate; //93/07/27
					case "D":
						return sd.LongDate; //یکشنبه, 27 مهر 1393
					case "t":
						return sd.ShortTime;

					case "T":
						return sd.LongTime;

					case "f": //Long date + short time
						return string.Format("{0} {1}", sd.LongDate, sd.ShortTime);

					case "F": // Long date + long time //یکشنبه, 27 مهر 1393 01:15:43
						return string.Format("{0} {1}", sd.LongDate, sd.LongTime);

					case "g": //Short date + short time //93/07/27 01:14:24
						return string.Format("{0} {1}", sd.ShortDate, sd.ShortTime);

					case "G": //Short date + long time
						return string.Format("{0} {1}", sd.ShortDate, sd.LongTime);

					case "m":
					case "M": //Month and day
						return string.Format("{0} {1}", sd.MahName, sd.RoozeMah);

					case "y":
					case "Y": // year and month
						return string.Format("{0} {1}", sd.Saal, sd.MahName);

					case "B": //best with year and month and day ,simple
						return string.Format("{0}/{1:00}/{2:00}", sd.Saal, sd.Mah, sd.RoozeMah);

					default:
						return sd.ShortDate;
				}
			}
			else
			{
				format = format.Replace("YY", "yy");

				return format
					.Replace("yyyy", sd.Saal.ToString(CultureInfo.InvariantCulture))
					.Replace("yy", sd.Saal.ToString(CultureInfo.InvariantCulture).Substring(2, 2))
					.Replace("MMM", sd.MahName.ToString(CultureInfo.InvariantCulture))
					.Replace("MM", sd.Mah.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'))
					.Replace("M", sd.Mah.ToString(CultureInfo.InvariantCulture))
					.Replace("ddd", sd.RoozeHaftehName.ToString(CultureInfo.InvariantCulture))
					.Replace("dd", sd.RoozeMah.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'))
					.Replace("d", sd.RoozeMah.ToString(CultureInfo.InvariantCulture))
					.Replace("hh", sd.Saat.ToString(CultureInfo.InvariantCulture))
					.Replace("mm", sd.Daghighe.ToString(CultureInfo.InvariantCulture))
					.Replace("ss", sd.Saniyeh.ToString(CultureInfo.InvariantCulture));
			}
		}

		#endregion Persian

		#region Month/Week/Day Members

		public static string MapWeekDayToName(int sunDayOfWeek)
		{
			//we can use this method instead if switch
			//return ShamsiMonthNames[sunDayOfWeek];

			switch (sunDayOfWeek)
			{
				case 0: return "شنبه";
				case 1: return "یک شنبه";
				case 2: return "دو شنبه";
				case 3: return "سه شنبه";
				case 4: return "چهار شنبه";
				case 5: return "پنج شنبه";
				case 6: return "جمعه";

				default: throw new Exception("Map_WeekDay_ToName invalid number");
			}
		}

		public static string MapWeekDayToName(DayOfWeek sunDayOfWeek)
		{
			switch (sunDayOfWeek)
			{
				case DayOfWeek.Saturday: return "شنبه";
				case DayOfWeek.Sunday: return "یک شنبه";
				case DayOfWeek.Monday: return "دو شنبه";
				case DayOfWeek.Tuesday: return "سه شنبه";
				case DayOfWeek.Wednesday: return "چهار شنبه";
				case DayOfWeek.Thursday: return "پنج شنبه";
				case DayOfWeek.Friday: return "جمعه";
			}
			return "";
		}

		public static int MapWeekDayToNum(DayOfWeek sunDayOfWeek)
		{
			switch (sunDayOfWeek)
			{
				case DayOfWeek.Saturday: return 0;
				case DayOfWeek.Sunday: return 1;
				case DayOfWeek.Monday: return 2;
				case DayOfWeek.Tuesday: return 3;
				case DayOfWeek.Wednesday: return 4;
				case DayOfWeek.Thursday: return 5;
				case DayOfWeek.Friday: return 6;

				default: throw new Exception("Map_WeekDay_ToName invalid number");
			}
		}

		public static string MapFarsiMonthNumToName(int fmonth)
		{
			switch (fmonth)
			{
				case 1: return "فروردین";
				case 2: return "اردیبهشت";
				case 3: return "خرداد";
				case 4: return "تیر";
				case 5: return "مرداد";
				case 6: return "شهریور";
				case 7: return "مهر";
				case 8: return "آبان";
				case 9: return "آذر";
				case 10: return "دی";
				case 11: return "بهمن";
				case 12: return "اسفند";

				default: throw new Exception("Map_Month_ToName invalid number");
			}
		}

		#endregion Month/Week/Day Members

		#endregion Public Members

		#region Private Members

		private static ShamsiDate ToShamsiDate(DateTime date)
		{
			return new ShamsiDate(date);
		}

		private static ShamsiDate ToShamsiDate(int selectedYear, int selectedMonth, int selectedDay)
		{
			DateTime date = new DateTime(selectedYear, selectedMonth, selectedDay);

			return new ShamsiDate(date);
		}

		private static ShamsiDate ToShamsiDate(int? selectedYear, int? selectedMonth, int? selectedDay)
		{
			if (!(selectedYear.HasValue && selectedMonth.HasValue && selectedDay.HasValue))
			{
				return null;
			}

			DateTime date = new DateTime(selectedYear.Value, selectedMonth.Value, selectedDay.Value);

			return new ShamsiDate(date);
		}

		private static ShamsiDate ToShamsiDate(DateTime? selectedDate)
		{
			if (!selectedDate.HasValue)
			{
				return null;
			}

			return new ShamsiDate(selectedDate.Value);
		}

		private static int[] SplitRoozMahSalNew(string farsiDate)
		{
			int pYear = 0;
			int pMonth = 0;
			int pDay = 0;

			//normalize with one character
			farsiDate = farsiDate.Trim().Replace(@"\", "/").Replace(@"-", "/").Replace(@"_", "/").
				Replace(@",", "/").Replace(@".", "/").Replace(@" ", "/");

			string[] rawValues = farsiDate.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

			if (!farsiDate.Contains("/"))
			{
				if (rawValues.Length != 2)
				{
					throw new Exception("usually there should be 2 seperator for a complete date");
				}
			}
			else //mostly given in all numeric format like 13930316
			{
				// detect year side and add slashes in right places and continue
			}
			//new simple method which emcompass below methods too
			try
			{
				pYear = int.Parse(rawValues[0].TrimStart(new[] { '0' }).Trim());
				pMonth = int.Parse(rawValues[1].TrimStart(new[] { '0' }).Trim());
				pDay = int.Parse(rawValues[2].TrimStart(new[] { '0' }).Trim());

				// the year usually must be larger than 90
				//or for historic values rarely lower than 33 if 2 digit is given
				if (pYear < 33 && pYear > 0)
				{
					//swap year and day
					pYear = pDay;
					pDay = int.Parse(rawValues[0]); //convert again
				}
				//fix 2 digits of persian strings
				if (pYear.ToString(CultureInfo.InvariantCulture).Length == 2)
				{
					pYear = pYear + 1300;
				}
				//
				if (pMonth <= 0 || pMonth >= 13)
				{
					throw new Exception("mahe shamsi must be under 12 ");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(
					"invalid Persian date format: maybe all 3 numric Sal, Mah,rooz parts are not present. \r\n" + ex);
			}

			return new[] { pYear, pMonth, pDay };
		}

		private static int[] SplitRoozMahSal(string farsiDate)
		{
			int month = 0;
			int day = 0;

			#region normalization and exception hadling

			//normalize with one character
			farsiDate = farsiDate.Trim().Replace(@"\", "/").Replace(@"-", "/").Replace(@"_", "/").
				Replace(@",", "/").Replace(@".", "/").Replace(@" ", "/");

			string[] rawValues = farsiDate.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

			if (!farsiDate.Contains("/"))
			{
				if (rawValues.Length != 2)
				{
					throw new Exception("usually there should be 2 seperator for a complete date");
				}
			}
			else //mostly given in all numeric format like 13930316
			{
				// detect year side and add slashes in right places and continue
			}

			//todo: handle if date is given in rtl format like 16/02/1393
			//todo: handle if date is given in short year format like 93/03/16
			//if a number is greater than 31 it is year side
			//note:only if it is 2 digit there is risk after 1400 it becomes corrupted
			//todo: ()not very usual handle if date is given in very short format like 93/3/16

			#endregion normalization and exception hadling

			int.TryParse(farsiDate.Substring(0, 4), out int year);
			if (year == 0)
			{
				throw new Exception("the first 4 character must denots a shamsi year like 1393");
			}

			switch (farsiDate.Length)
			{
				case 10://1389/01/01
					month = Convert.ToInt32(farsiDate.Substring(5, 2));
					day = Convert.ToInt32(farsiDate.Substring(8, 2));
					break;

				case 8://13900421
					if (!farsiDate.Contains("/"))
					{
						month = Convert.ToInt32(farsiDate.Substring(4, 2));
						day = Convert.ToInt32(farsiDate.Substring(6, 2));
					}
					else if (farsiDate[4] == '/' && farsiDate[6] == '/')//1389/1/1
					{
						month = Convert.ToInt32(farsiDate.Substring(5, 1));
						day = Convert.ToInt32(farsiDate.Substring(7, 1));
					}
					break;

				case 9://1389/01/1 or //1389/1/01
					if (farsiDate.Substring(7, 1) == "/")
					{
						month = Convert.ToInt32(farsiDate.Substring(5, 2));
						day = Convert.ToInt32(farsiDate.Substring(8, 1));
					}
					else
					{
						month = Convert.ToInt32(farsiDate.Substring(5, 1));
						day = Convert.ToInt32(farsiDate.Substring(7, 2));
					}
					break;
			}
			return new[] { year, month, day };
		}

		#endregion Private Members
	}
}