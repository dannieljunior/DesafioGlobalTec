using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DesafioGlobalTec.Comum.Validadores
{
    public class ValidadorUf : ValidationAttribute
    {
        string[] _ufs = {"AC", "AL", "AM", "AP", "BA",
                        "CE", "DF", "ES", "GO", "MA",
                        "MG", "MS", "MT", "PA", "PB",
                        "PE", "PI", "PR", "RJ", "RN",
                        "RO", "RR", "RS", "SC", "SE",
                        "SP", "TO" };

        public override bool IsValid(object value)
        {
            var uf = (string)value;

            if(!_ufs.Contains(uf))
            {
                ErrorMessage = "A UF informada é inválida.";
                return false;
            }

            return true;                
        }
    }
}
