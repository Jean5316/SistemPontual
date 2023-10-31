using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestePontual.Areas.Admin.Models;
using TestePontual.Repository.Interfaces;

namespace TestePontual.Areas.Admin.Controllers
{
    [Authorize("Admin")]
    [Area("Admin")]
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository _context;

        public ProdutosController(IProdutoRepository context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TiTulo = "Lista de Produtos";
            var produtos = _context.Produtos.ToList();
            return View(produtos);
        }

        public IActionResult CriarProduto()
        {
            ViewBag.Titulo = "Criar Produto";

            return View();
        }


        [HttpPost]
        public IActionResult CriarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                var produtoDb = _context.GetProdutoId(produto.Id);
                if (produtoDb == null)
                {
                    _context.CriarProduto(produto);
                    return RedirectToAction("Index", "Produtos");
                }

                ModelState.AddModelError("", "Produto ja existe!");
                return View();
            }

            return View();
        }

        public IActionResult ExcluirProduto(int id)
        {
            var produtoDb = _context.GetProdutoId(id);
            if (produtoDb != null)
            {
                _context.DeletarProduto(produtoDb);
                return RedirectToAction("Index", "Produtos");

            }

            return NotFound();

        }
    }
}