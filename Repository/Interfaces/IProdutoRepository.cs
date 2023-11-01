using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePontual.Areas.Admin.Models;

namespace TestePontual.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        public IEnumerable<Produto> Produtos { get; }

        Produto GetProdutoId(int produtoId);

        void CriarProduto(Produto produto);
        void EditarProduto(Produto produto);
        void DeletarProduto(Produto produto);

        Produto Detalhes(int Id);

        


    }
}