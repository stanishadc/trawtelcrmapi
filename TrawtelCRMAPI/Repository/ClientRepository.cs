using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Client> GetAllClients()
        {
            return FindAll()
                .OrderBy(ow => ow.CreatedDate)
                .ToList();
        }
        public Client GetClientById(Guid ClientId)
        {
            return FindByCondition(client => client.ClientId.Equals(ClientId)).FirstOrDefault();
        }
        public void CreateClient(Client client)
        {
            Create(client);
        }
        public void UpdateClient(Client client)
        {
            Update(client);
        }
        public void DeleteClient(Client client)
        {
            Delete(client);
        }
    }
}
