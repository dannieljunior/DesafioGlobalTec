using DesafioGlobalTec.Comum.Models;
using System.Collections.Generic;

namespace DesafioGlobalTec.Repository.Cache
{
    public sealed class CacheRepository
    {
        private readonly List<Pessoa> _pessoas = new List<Pessoa>();
        public List<Pessoa> Pessoas => _pessoas;

        private static CacheRepository _instancia;

        private CacheRepository(){}

        public static CacheRepository Instancia()
        {
            if (_instancia == null)
            {
                _instancia = new CacheRepository();
            }

            return _instancia;
        }
    }
}
