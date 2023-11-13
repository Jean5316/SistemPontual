using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestePontual.Models;
using TestePontual.Repository.Interfaces;

namespace TestePontual.Controllers
{
    [Authorize]
    public class OS : Controller
    {
        private readonly IOrdemServico _context;

        public OS(IOrdemServico context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Titulo = "Page de OS's";
            var ordens = _context.OrdemServicos.ToList();
            return View(ordens);
        }

        public IActionResult CriarOS()
        {
            ViewBag.Titulo = "Page de Criar OS's";
            var opcoesEnum = Enum.GetValues(typeof(EnumStatus)).Cast<EnumStatus>();
            ViewBag.Opcoes = opcoesEnum;
            return View();
        }

        [HttpPost]
        public IActionResult CriarOS(OrdemServico os)
        {
            if (ModelState.IsValid)
            {
                var clienteId = _context.GetNameId(os.NomeCliente);
                if ( clienteId != -1)
                {
                    os.ClienteId = clienteId;
                    _context.CriarOS(os);
                    return RedirectToAction("Index", "OS");
                }

                ModelState.AddModelError("", "Cliente não cadastrado");

            }

            return View();
        }
    }
}