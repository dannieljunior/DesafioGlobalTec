using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioGlobalTec.Comum.Validadores
{
    public class ValidadorData : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            var data = (DateTime)value;
            DateTime dataResultado;
            try
            {
                DateTime.TryParse(data.ToString(), out dataResultado);
            }
            catch
            {
                ErrorMessage = "A data informada é inválida ou fora de uma faixa de Datetime permitido";
                return false;
            }

            return true;                
        }
    }
}
