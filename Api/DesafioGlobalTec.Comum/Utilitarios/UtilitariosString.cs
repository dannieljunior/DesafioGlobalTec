using System.Text.RegularExpressions;

namespace DesafioGlobalTec.Comum.Utilitarios
{
    public static class UtilitariosString
    {
        public static string ObterSomenteNumeros(this string pTexto)
        {
            if (string.IsNullOrEmpty(pTexto))
                return null;
            Regex regex = new Regex(@"[^0-9]");
            string retorno = regex.Replace(pTexto, string.Empty);
            return retorno;
        }
    }
}
