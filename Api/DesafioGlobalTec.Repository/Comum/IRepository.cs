using DesafioGlobalTec.Comum.Interfaces;
using System.Collections.Generic;

namespace DesafioGlobalTec.Repository.Comum
{
    public interface IRepository<T> where T: IDto
    {
        List<T> ObterTodos();
        T ObterPorId(int pId);
        T Salvar(T pObjeto);
        void Excluir(int pId);
    }
}
