using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobalTec.Comum.Exceptions
{
    public class ENotFoundException: Exception
    {
        public override string Message => "Não foi possível realizar a operação solicitada. Não foi localizado o registro para o Id Informado!";
    }
}
