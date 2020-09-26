using Framework.Core.Contracts.Services;
using Management.Infrastructure.Facade.DTOModel;
using System.Collections.Generic;

namespace Management.Infrastructure.Facade.FacadeService.Contracts
{
	public interface IClientFacadeService : IFacadeService
	{
		#region Client

		IEnumerable<ClientListDTO> GetAllClients();

		IEnumerable<ClientDTO> GetAllClientsByUserName(string userName);

		ClientDTO GetClientById(int id);

		ClientDTO GetClientByName(string name);

		ClientDTO GetClientByAdminUsername(string username);

		bool ExistsClientId(int id, string ClientId);

		bool ExistsClientName(int id, string clientName);

		void AddClient(ClientDTO client);

		void UpdateClient(ClientDTO client);

		void DeleteClient(ClientDTO client);

		#endregion Client
	}
}