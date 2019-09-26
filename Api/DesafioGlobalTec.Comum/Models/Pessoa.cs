using DesafioGlobalTec.Comum.Interfaces;
using DesafioGlobalTec.Comum.Utilitarios;
using DesafioGlobalTec.Comum.Validadores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioGlobalTec.Comum.Models
{
    public class Pessoa: IDto
    {
        public int Codigo { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [ValidadorCpf]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "O campo UF é obrigatório")]
        [ValidadorUf]
        public string Uf { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
