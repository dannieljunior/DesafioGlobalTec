using DesafioGlobalTec.Comum.Exceptions;
using DesafioGlobalTec.Comum.Models;
using DesafioGlobalTec.Comum.Utilitarios;
using DesafioGlobalTec.Repository.Cache;
using DesafioGlobalTec.Repository.Comum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioGlobalTec.Repository
{
    public class PessoasRepository: IRepository<Pessoa>
    {
        private CacheRepository repositorio = CacheRepository.Instancia();

        public void Excluir(int pId)
        {
            var pessoaExistente = ObterPorId(pId);
            repositorio.Pessoas.Remove(pessoaExistente);
        }

        public Pessoa ObterPorId(int pId)
        {
            var pessoaExistente = repositorio.Pessoas.FirstOrDefault(pessoa => pessoa.Codigo.Equals(pId));
            if(pessoaExistente == null)
                throw new ENotFoundException();
            return pessoaExistente;
        }

        public List<Pessoa> ObterTodos()
        {
            return repositorio.Pessoas;
        }

        public Pessoa Salvar(Pessoa pObjeto)
        {
            pObjeto.Cpf = pObjeto.Cpf.ObterSomenteNumeros();

            if (!ValidaCpfDuplicado(pObjeto.Cpf, pObjeto.Codigo))
                throw new Exception("O CPF informado já está cadastrado.");

            if (pObjeto.Codigo == 0)
            {
                pObjeto.Codigo = GerarNovoCodigo();
                repositorio.Pessoas.Add(pObjeto);
                return pObjeto;
            }
            else
            {
                var pessoaExistente = ObterPorId(pObjeto.Codigo);
                pessoaExistente.Nome = pObjeto.Nome;
                pessoaExistente.Cpf = pObjeto.Cpf.ObterSomenteNumeros();
                pessoaExistente.DataNascimento = pObjeto.DataNascimento;
                pessoaExistente.Uf = pObjeto.Uf;
                return pessoaExistente;
            }
        }

        private bool ValidaCpfDuplicado(string pCpf, int pCodigo = 0)
        {
            if (pCodigo == 0)
                return repositorio.Pessoas.FirstOrDefault(p => p.Cpf.Equals(pCpf)) == null;

            return repositorio.Pessoas.FirstOrDefault(p => p.Cpf.Equals(pCpf) && p.Codigo != pCodigo) == null;
        }

        private int GerarNovoCodigo()
        {
            int resultado = 0;
            if (repositorio.Pessoas.Count > 0)
                resultado = repositorio.Pessoas.Max(p => p.Codigo);
            resultado++;
            return resultado;
        }
    }
}
