using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePontual.Models;

namespace TestePontual.Repository.Interfaces
{
    public interface IOrdemServico
    {
        public IEnumerable<OrdemServico> OrdemServicos { get; }
        OrdemServico GetOSId(int Id);
        int GetNameId(string name);

        void CriarOS(OrdemServico os);
        void EditarOS(OrdemServico os);
        void DeletarOS(OrdemServico os);

        OrdemServico Detalhes(int Id);
    }
}