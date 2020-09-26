using Framework.Core.Contracts.Services;
using Management.Infrastructure.Models;
using System.Collections.Generic;

namespace Management.Infrastructure.Service.Contracts
{
	public interface IClientService : IService
	{
		#region Client

		IEnumerable<Client> GetAllClients();

		IEnumerable<Client> GetAllClientsByUserName(string userName);

		Client GetClientById(int id);

		Client GetClientByName(string name);

		Client GetClientByAdminUsername(string username);

		bool ExistsClientId(int id, string ClientId);

		bool ExistsClientName(int id, string clientName);

		void AddClient(Client client);

		void UpdateClient(Client client);

		void DeleteClient(Client client);

		#endregion Client

		#region ClientClaim

		IEnumerable<ClientClaim> GetClientClaim(int clientId);

		void AddClientClaim(int clientId, params ClientClaim[] clientClaim);

		void DeleteClientClaim(int clientId, params ClientClaim[] clientClaim);

		void UpdateClientClaim(int clientId, ClientClaim clientClaim);

		#endregion ClientClaim

		#region ClientCorsOrigin

		IEnumerable<ClientCorsOrigin> GetClientCorsOrigin(int clientId);

		void AddClientCorsOrigin(int clientId, params ClientCorsOrigin[] corsOrgin);

		void DeleteClientCorsOrigin(int clientId, params ClientCorsOrigin[] corsOrgin);

		void UpdateClientCorsOrigin(int clientId, ClientCorsOrigin corsOrgin);

		#endregion ClientCorsOrigin

		#region ClientCustomGrantTypes

		IEnumerable<ClientCustomGrantType> GetClientCustomGrantTypes(int clientId);

		void AddClientCustomGrantType(int clientId, params ClientCustomGrantType[] customGrantType);

		void DeleteClientCustomGrantType(int clientId, params ClientCustomGrantType[] customGrantType);

		void UpdateClientCustomGrantType(int clientId, ClientCustomGrantType customGrantType);

		#endregion ClientCustomGrantTypes

		#region ClientIdPRestrictions

		IEnumerable<ClientIdPRestriction> GetClientIdPRestrictions(int clientId);

		void AddClientIdPRestriction(int clientId, params ClientIdPRestriction[] idPRestriction);

		void DeleteClientIdPRestriction(int clientId, params ClientIdPRestriction[] idPRestriction);

		void UpdateClientIdPRestriction(int clientId, ClientIdPRestriction idPRestriction);

		#endregion ClientIdPRestrictions

		#region ClientPostLogoutRedirectUri

		IEnumerable<ClientPostLogoutRedirectUri> GetClientPostLogoutRedirectUri(int clientId);

		void AddClientPostLogoutRedirectUri(int clientId, params ClientPostLogoutRedirectUri[] postLogoutRedirectUri);

		void DeleteClientPostLogoutRedirectUri(int clientId, params ClientPostLogoutRedirectUri[] postLogoutRedirectUri);

		void UpdateClientPostLogoutRedirectUri(int clientId, ClientPostLogoutRedirectUri postLogoutRedirectUri);

		#endregion ClientPostLogoutRedirectUri

		#region ClientRedirectUri

		IEnumerable<ClientRedirectUri> GetClientRedirectUri(int clientId);

		void AddClientRedirectUri(int clientId, params ClientRedirectUri[] redirectUri);

		void DeleteClientRedirectUri(int clientId, params ClientRedirectUri[] redirectUri);

		void UpdateClientRedirectUri(int clientId, ClientRedirectUri redirectUri);

		#endregion ClientRedirectUri

		#region ClientScope

		IEnumerable<ClientScope> GetClientScope(int clientId);

		void AddClientClientScope(int clientId, params ClientScope[] scope);

		void DeleteClientScope(int clientId, params ClientScope[] scope);

		#endregion ClientScope

		#region ClientSecret

		IEnumerable<ClientSecret> GetClientSecret(int clientId);

		void AddClientSecret(int clientId, params ClientSecret[] secret);

		void DeleteClientSecret(int clientId, params ClientSecret[] secret);

		#endregion ClientSecret

		#region ClientRole

		IEnumerable<Role> GetClientRole(int clientId);

		void AddClientRole(int clientId, params Role[] role);

		void DeleteClientRole(int clientId, params Role[] role);

		#endregion ClientRole

		#region ClientPermission

		IEnumerable<Permission> GetClientPermission(int clientId);

		void AddClientPermission(int clientId, params Permission[] permission);

		void DeleteClientPermission(int clientId, params Permission[] permission);

		void UpdateClientPermission(int clientId, Permission permission);

		#endregion ClientPermission

		#region ClientUser

		IEnumerable<User> GetClientUser(int clientId);

		void AddClientUser(int clientId, params User[] user);

		void DeleteClientUser(int clientId, params User[] user);

		#endregion ClientUser
	}
}