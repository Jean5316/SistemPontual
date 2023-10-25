using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestePontual.Models;
using TestePontual.Context;
using TestePontual.Repositories;
using TestePontual.Repository;

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
            System.Globalization.CultureInfo cultureinfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            var clientes = from m in _context.Clientes select m;//Consulta link no DB

            if (!string.IsNullOrEmpty(pesquisa))//Campo de pesquisa 
            {
                clientes = _context.Clientes.Where(x => x.Nome.Contains(cultureinfo.TextInfo.ToTitleCase(pesquisa)));
            }
            //amazenas dados por chave e valor tipada
            ViewData["Title"] = "Pagina Inicial";
            ViewData["Data"] = DateTime.Now;
            //armazena dados por chave valor não tipada
            ViewBag.Total = "Total de Clientes";
            ViewBag.TotalClientes = clientes.Count();
            //armazena dados temporarios por chave e valor tipada
            //perde o valor depois que recuperado na view
            TempData["Nome"] = "Jean";

            return View(clientes);

        }

        //Carrega Pagina de Criar(Criar.cshtml)
        public IActionResult Criar()
        {
            return View();
        }

        // //Grava Dados Criados no Banco de Dados
        // [HttpPost]
        // public IActionResult Criar(Cliente cliente)
        // {
        //     if(_context.)
        //     return RedirectToAction(nameof(Index));
        // }


        //     //Deleta Cliente Do Banco de Dados
        //     [HttpGet]
        //     public IActionResult Delete(int Id)
        //     {

        //         _context.Remove(Cliente);
        //         _context.SaveChanges();
        //         return RedirectToAction(nameof(Index));
        //     }

        //     //Edita Cliente
        //     public IActionResult Editar(int Id)
        //     {
        //         var Cliente = _context.Clientes.Find(Id);
        //         if (Cliente == null)
        //         {
        //             return NotFound();
        //         }
        //         return View(Cliente);

        //     }

        //     [HttpPost]
        //     public IActionResult Editar(Cliente cliente)
        //     {
        //         var ClienteDB = _context.Clientes.Find(cliente.Id);
        //         ClienteDB.Nome = cliente.Nome;
        //         ClienteDB.Contato = cliente.Contato;
        //         ClienteDB.Email = cliente.Email;
        //         ClienteDB.Cep = cliente.Cep;
        //         ClienteDB.Rua = cliente.Rua;
        //         ClienteDB.Numero = cliente.Numero;
        //         ClienteDB.Complemento = cliente.Complemento;
        //         ClienteDB.Cidade = cliente.Cidade;
        //         ClienteDB.Bairro = cliente.Bairro;
        //         ClienteDB.Estado = cliente.Estado;

        //         _context.Clientes.Update(ClienteDB);
        //         _context.SaveChanges();

        //         return RedirectToAction(nameof(Index));

        //     }

        //     //Edita Cliente
        //     public IActionResult Detalhes(int Id)
        //     {
        //         var Cliente = _context.Clientes.Find(Id);
        //         if (Cliente == null)
        //         {
        //             return NotFound();
        //         }
        //         return View(Cliente);

        //     }
    }
}