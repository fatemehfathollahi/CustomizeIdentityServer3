using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Facade.FacadeService.Contracts;
using Management.Infrastructure.Models;
using Management.Infrastructure.Service.Contracts;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace Management.Infrastructure.Facade.FacadeServices
{
	public class ClientFacadeService : IClientFacadeService
	{
		#region Internal Fields

		internal IMapper Mapper;
		internal IClientService ClientService;

		#endregion Internal Fields

		#region Constructor

		public ClientFacadeService(IMapper mapper, IClientService clientService)
		{
			Mapper = mapper;
			ClientService = clientService;
		}

		#endregion Constructor

		#region IClientFacadeService Members

		public IEnumerable<ClientListDTO> GetAllClients()
		{
			IEnumerable<Client> tempList = ClientService.GetAllClients();
			var list = Mapper.Map<IEnumerable<ClientListDTO>>(tempList);
			return list;
		}

		public bool ExistsClientId(int id, string ClientId)
		{
			return ClientService.ExistsClientId(id, ClientId);
		}

		public bool ExistsClientName(int id, string clientName)
		{
			return ClientService.ExistsClientName(id, clientName);
		}

		public IEnumerable<ClientDTO> GetAllClientsByUserName(string userName)
		{
			IEnumerable<Client> tempList = ClientService.GetAllClientsByUserName(userName);

			var list = Mapper.Map<IEnumerable<ClientDTO>>(tempList);

			return list;
		}

		public ClientDTO GetClientByAdminUsername(string username)
		{
			throw new NotImplementedException();
		}

		public ClientDTO GetClientById(int id)
		{
			Client result = ClientService.GetClientById(id);
			return Mapper.Map<ClientDTO>(result);
		}

		public ClientDTO GetClientByName(string name)
		{
			throw new NotImplementedException();
		}

		public void AddClient(ClientDTO client)
		{
			var tempClient = Mapper.Map<Client>(client);
			ClientService.AddClient(tempClient);
		}

		public void UpdateClient(ClientDTO client)
		{
			var temp = Mapper.Map<Client>(client);
			ClientService.UpdateClient(temp);
		}

		public void DeleteClient(ClientDTO client)
		{
			var temp = Mapper.Map<Client>(client);
			ClientService.DeleteClient(temp);
		}

		#endregion IClientFacadeService Members

		#region IDisposable Members

		public void Dispose()
		{
			if (Mapper != null)
			{
				Mapper = null;
			}

			if (ClientService != null)
			{
				ClientService.Dispose();
				ClientService = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}