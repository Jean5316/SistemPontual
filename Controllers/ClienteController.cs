using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestePontual.Models;
using TestePontual.Context;
using TestePontual.Repositories;
using TestePontual.Repository;
using TestePontual.ViewModels;


namespace TestePontual.Controllers
{
    public class ClienteController : Controller
    {
        //implementação do serviço do contexto representado pela interface de repositorio
        private readonly IClienteRepository _context;
        public ClienteController(IClienteRepository context)
        {
            _context = context;
        }

        //Pagina index 
        public IActionResult Index(string pesquisa)
        {

            //amazenas dados por chave e valor tipada
            // ViewData["Title"] = "Pagina Inicial";
            // ViewData["Data"] = DateTime.Now;

            //armazena dados temporarios por chave e valor tipada
            //perde o valor depois que recuperado na view
            // TempData["Nome"] = "Teste TEMPDATA";


            var ClientesListViewModel = new ClienteListViewModel();
            ClientesListViewModel.Clientes = _context.Clientes;
            //ClientesListViewModel.Clientes = _context.Clientes.Where(x => x.Nome.Contains(pesquisa)).ToList();




            //armazena dados por chave valor não tipada
            ViewBag.Titulo = "Lista de Clientes";
            //ViewBag.TotalClientes = ClientesListViewModel.Count();


            return View(ClientesListViewModel);

        }

        //Carrega Pagina de Criar(Criar.cshtml)
        public IActionResult Criar()
        {
            return View();
        }

        //Grava Dados Criados no Banco de Dados
        [HttpPost]
        public IActionResult Criar(Cliente cliente)
        {

            if (ModelState.IsValid)
            {
                _context.CriarCliente(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        //Edita Cliente
        public IActionResult Editar(int Id)
        {
            var cliente = _context.GetClientById(Id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Editar(Cliente cliente)
        {
            var ClienteDB = _context.GetClientById(cliente.Id);
            if (ClienteDB == null)
            {
                return NotFound();
            }
            ClienteDB.Nome = cliente.Nome;
            ClienteDB.Contato = cliente.Contato;
            ClienteDB.Email = cliente.Email;
            ClienteDB.Cep = cliente.Cep;
            ClienteDB.Rua = cliente.Rua;
            ClienteDB.Numero = cliente.Numero;
            ClienteDB.Complemento = cliente.Complemento;
            ClienteDB.Cidade = cliente.Cidade;
            ClienteDB.Bairro = cliente.Bairro;
            ClienteDB.Estado = cliente.Estado;

            _context.EditarCliente(ClienteDB);

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Detalhes(int id)
        {

            return View(_context.Detalhes(id));
        }

        //Deleta Cliente Do Banco de Dados
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var cliente = _context.GetClientById(Id);
            _context.DeletarCliente(cliente);

            return RedirectToAction(nameof(Index));
        }
    }
}