using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DesafioGlobalTec.Comum.Validadores
{
    public class ValidadorUf : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var uf = (string)value;

            if(!Constantes.Ufs.Contains(uf))
            {
                ErrorMessage = "A UF informada é inválida.";
                return false;
            }

            return true;                
        }
    }
}
