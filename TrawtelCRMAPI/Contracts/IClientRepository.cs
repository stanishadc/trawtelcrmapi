using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IClientRepository : IRepositoryBase<Client>
    {
        IEnumerable<Client> GetAllClients();
        Client GetClientById(Guid ClientId);
        void CreateClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Client client);
    }
}
