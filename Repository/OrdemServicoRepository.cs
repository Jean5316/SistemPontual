using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePontual.Context;
using TestePontual.Models;
using TestePontual.Repository.Interfaces;

namespace TestePontual.Repository
{
    public class OrdemServicoRepository : IOrdemServico
    {
        private readonly AppDbContext _context;

        public OrdemServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrdemServico> OrdemServicos => _context.OS;


        public void CriarOS(OrdemServico os)
        {
            _context.Add(os);
            _context.SaveChanges();
        }

        public void DeletarOS(OrdemServico os)
        {
            _context.Remove(os);
            _context.SaveChanges();
        }

        public OrdemServico Detalhes(int Id)
        {
            var os = _context.OS.Find(Id);
            if (os == null)
            {
                return null;
            }

            return os;
        }

        public void EditarOS(OrdemServico os)
        {
            _context.Update(os);
            _context.SaveChanges();

        }

        public int GetNameId(string NomeCliente)
        {
            if (!string.IsNullOrWhiteSpace(NomeCliente))
            {
                var clienteDb = _context.Clientes.SingleOrDefault(x => x.Nome.ToLower() == NomeCliente.ToLower());
                if (clienteDb != null)
                {
                    return clienteDb.Id;
                }


            }

            return -1;
        }

        public OrdemServico GetOSId(int Id)
        {
            var osDb = _context.OS.Find(Id);
            if (osDb == null)
            {
                return null;
            }

            return osDb;
        }

   
    }
}