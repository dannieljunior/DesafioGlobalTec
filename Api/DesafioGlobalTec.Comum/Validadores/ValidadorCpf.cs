using DesafioGlobalTec.Comum.Utilitarios;
using System.ComponentModel.DataAnnotations;

namespace DesafioGlobalTec.Comum.Validadores
{
    public class ValidadorCpf : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var cpf = (string)value;

            if (!CpfValido(cpf))
            {
                ErrorMessage = "O Cpf informado não é válido";
                return false;
            }

            return true;                
        }

        private bool CpfValido(string pCpf)
        {

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var cpf = pCpf.ObterSomenteNumeros();

            if (string.IsNullOrEmpty(cpf))
                return false;

            if (cpf.Length != 11)
                return false;

            var numSequencial = cpf.Substring(0, 9);

            var soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(numSequencial[i].ToString()) * (multiplicador1[i]);

            var resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();
            numSequencial += digito;
            int soma2 = 0;

            for (int i = 0; i < 10; i++)
                soma2 += int.Parse(numSequencial[i].ToString()) * multiplicador2[i];

            resto = soma2 % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
