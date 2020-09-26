using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Validation;
using IdentityServer3.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace SecurityService.SSO.Infrastructure
{
	public class ApplicationViewService : IViewService
	{
		private IClientStore clientStore;

		public ApplicationViewService(IClientStore clientStore)
		{
			this.clientStore = clientStore;
		}

		public virtual async Task<Stream> Login(LoginViewModel model, SignInMessage message)
		{
			Client client = await clientStore.FindClientByIdAsync(message.ClientId);
			string name = client != null ? client.ClientName : null;
			return await Render(model, "login", name);
		}

		public Task<Stream> Logout(LogoutViewModel model, SignOutMessage message)
		{
			return Render(model, "logout");
		}

		public Task<Stream> LoggedOut(LoggedOutViewModel model, SignOutMessage message)
		{
			return Render(model, "loggedOut");
		}

		public Task<Stream> Consent(ConsentViewModel model, ValidatedAuthorizeRequest authorizeRequest)
		{
			return Render(model, "consent");
		}

		public Task<Stream> ClientPermissions(ClientPermissionsViewModel model)
		{
			return Render(model, "permissions");
		}

		public virtual Task<Stream> Error(ErrorViewModel model)
		{
			return Render(model, "error");
		}

		protected virtual Task<System.IO.Stream> Render(CommonViewModel model, string page, string clientName = null)
		{
			string json = Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });

			string html = LoadHtml(page);
			html = Replace(html, new
			{
				siteName = Microsoft.Security.Application.Encoder.HtmlEncode(model.SiteName),
				model = Microsoft.Security.Application.Encoder.HtmlEncode(json),
				clientName = clientName
			});

			return Task.FromResult(StringToStream(html));
		}

		private string LoadHtml(string name)
		{
			string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"content\app");
			file = Path.Combine(file, name + ".html");
			return File.ReadAllText(file);
		}

		private string Replace(string value, IDictionary<string, object> values)
		{
			foreach (string key in values.Keys)
			{
				object val = values[key];
				val = val ?? string.Empty;
				if (val != null)
				{
					value = value.Replace("{" + key + "}", val.ToString());
				}
			}
			return value;
		}

		private string Replace(string value, object values)
		{
			return Replace(value, Map(values));
		}

		private IDictionary<string, object> Map(object values)
		{
			IDictionary<string, object> dictionary = values as IDictionary<string, object>;

			if (dictionary == null)
			{
				dictionary = new Dictionary<string, object>();
				foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
				{
					dictionary.Add(descriptor.Name, descriptor.GetValue(values));
				}
			}

			return dictionary;
		}

		private Stream StringToStream(string s)
		{
			MemoryStream ms = new MemoryStream();
			StreamWriter sw = new StreamWriter(ms);
			sw.Write(s);
			sw.Flush();
			ms.Seek(0, SeekOrigin.Begin);
			return ms;
		}
	}
}