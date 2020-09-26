using System;

namespace Framework.Core.Common.DateUtilities
{
	internal class ShamsiDate
	{
		/// <summary>
		/// this constructor uses PersianCalendar and store its method returned values in a more friendler structor
		/// and properties of this class
		/// </summary>
		/// <param name="date"></param>
		internal ShamsiDate(DateTime date)
		{
			EquivalentGoergianDate = date;
			System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();

			Saal = pDate.GetYear(date);
			Mah = pDate.GetMonth(date);
			RoozeMah = pDate.GetDayOfMonth(date);
			//time
			Saat = pDate.GetHour(date);
			Daghighe = pDate.GetMinute(date);
			Saniyeh = pDate.GetSecond(date);

			RoozeHafteh = ConvertDate.MapWeekDayToNum(pDate.GetDayOfWeek(date));
		}

		internal ShamsiDate(int saal, int mah, int rooz)
		{
			Saal = saal;
			Mah = mah;
			RoozeMah = rooz;

			System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
			EquivalentGoergianDate = pDate.ToDateTime(saal, mah, rooz, 0, 0, 0, 0);

			Saat = pDate.GetHour(EquivalentGoergianDate);
			Daghighe = pDate.GetMinute(EquivalentGoergianDate);
			Saniyeh = pDate.GetSecond(EquivalentGoergianDate);

			RoozeHafteh = ConvertDate.MapWeekDayToNum(pDate.GetDayOfWeek(EquivalentGoergianDate));
		}

		#region properties

		public DateTime EquivalentGoergianDate { get; set; }

		internal string RoozeHaftehName
		{
			get
			{
				if (RoozeHafteh >= 0 && RoozeHafteh <= 6)
				{
					return ConvertDate.MapWeekDayToName(RoozeHafteh);
				}

				return "";
			}
		}

		internal string MahName
		{
			get
			{
				if (Mah > 0 && Mah < 13)
				{
					return ConvertDate.MapFarsiMonthNumToName(Mah);
				}

				return "";
			}
		}

		private string TimeSymbol => (Saat / 12 == 1 ? "عصر" : "صبح");

		private string TimeSymbolShort => (Saat / 12 == 1 ? "ص" : "ع");

		internal string ShortDate
		{
			get
			{
				string shortSal = Saal.ToString().Remove(0, 2);

				return string.Format("{0}/{1:00}/{2:00}", shortSal, Mah, RoozeMah);
			}
		}

		internal string ShortTime => string.Format("{0:00}:{1:00}", Saat, Daghighe);

		internal string LongDate => string.Format("{0}, {1:00} {2} {3}", RoozeHaftehName, RoozeMah, MahName, Saal);

		internal string LongTime => string.Format("{0:00}:{1:00}:{2:00}", Saat, Daghighe, Saniyeh);

		internal int RoozeHafteh { get; private set; }
		internal int RoozeMah { get; private set; }

		/// <summary>
		/// maheh sal be adad masalan 12
		/// </summary>
		internal int Mah { get; private set; }

		/// <summary>
		/// adade sal masalan 1392
		/// </summary>
		internal int Saal { get; private set; }

		internal int Saat { get; private set; }
		internal int Daghighe { get; private set; }
		internal int Saniyeh { get; private set; }

		#endregion properties
	}
}