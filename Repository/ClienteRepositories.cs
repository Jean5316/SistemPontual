using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using SQLitePCL;
using TestePontual.Context;
using TestePontual.Models;
using TestePontual.Repository;
using Microsoft.AspNetCore.Mvc;


namespace TestePontual.Repositories
{
    public class ClienteRepositories : IClienteRepository
    {
        //injeção de instancia da classe de context para acessar os dados
        private readonly AppDbContext _context;

        public ClienteRepositories(AppDbContext context)
        {
            _context = context;
        }

        //Metodos
        public Cliente GetClientById(int Id)
        {
            var cliente = _context.Clientes.Find(Id);
            if (cliente == null)
            {
                return null;
            }
            return cliente;
        }

        public void CriarCliente(Cliente cliente)
        {
            _context.Add(cliente);
            _context.SaveChanges();

        }

        public void EditarCliente(Cliente cliente)
        {

            _context.Update(cliente);
            _context.SaveChanges();


        }

        public void DeletarCliente(Cliente cliente)
        {

            _context.Remove(cliente);
            _context.SaveChanges();
        }

        public Cliente Detalhes(int cliente)
        {
            var Cliente = GetClientById(cliente);
            return Cliente;
        }

        



        //implementando propriedade clientes que retorna uma lista de clientes
        public IEnumerable<Cliente> Clientes => _context.Clientes;



    }
}