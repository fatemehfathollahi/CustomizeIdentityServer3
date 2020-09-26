using Newtonsoft.Json;

namespace Framework.Core.Common.Extensions
{
	public static class JsonExtensions
	{
		public static string ToJson(this object obj)
		{
			return JsonConvert.SerializeObject(obj, Formatting.Indented,
				new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
		}
	}
}