using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePontual.Areas.Admin.Models;
using TestePontual.Context;
using TestePontual.Repository.Interfaces;

namespace TestePontual.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Produto> Produtos => _context.Produtos;

        public void CriarProduto(Produto produto)
        {
            _context.Add(produto);
            _context.SaveChanges();
        }

        public void DeletarProduto(Produto produto)
        {
            _context.Remove(produto);
            _context.SaveChanges();
        }

        public Produto Detalhes(int Id)
        {
            var produto = _context.Produtos.Find(Id);
            if (produto == null)
            {
                return null;
            }

            return produto;
        }

        public void EditarProduto(Produto produto)
        {
            _context.Update(produto);
            _context.SaveChanges();
        }

        public Produto GetProdutoId(int produtoId)
        {
            var produto = _context.Produtos.Find(produtoId);
            if (produto == null)
            {
                return null;
            }

            return produto;
        }
    }
}