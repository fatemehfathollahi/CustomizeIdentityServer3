using System.Collections.Generic;

namespace ManagementApplication.Models
{
	public class DataTablePager<T> where T : class
	{
		public string sEcho { get; set; }
		public int iTotalRecords { get; set; }
		public int iTotalDisplayRecords { get; set; }
		public IEnumerable<T> aaData { get; set; }
	}
}