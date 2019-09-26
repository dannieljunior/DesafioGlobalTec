using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DesafioGlobalTec.Comum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DesafioGlobalTec.WebApi.Controllers
{
    /// <summary>
    /// Método de autenticação. Para a definição de usuário e senha não foi feita 
    /// nenhuma configuração de identidade, poderia ter um serviço a parte, que 
    /// o fizesse, buscando usuário e senha armazenado num banco de dados 
    /// com a senha criptografada. Além disso eu poderia criar alguma configuração de claim customizado, 
    /// e definir a autorização por um grupo específico, usuário, permissão, etc...
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        [HttpPost("token")]
        public ActionResult GetToken(string usuario, string senha)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
                    throw new Exception("Informe o usuário ou a senha para autenticação.");

                if (usuario.Equals("gtec") && senha.Equals("gtec"))
                {
                    string fraseChaveSegura = Constantes.FraseSegura;
                    var chaveSeguraSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fraseChaveSegura));
                    var credenciais = new SigningCredentials(chaveSeguraSimetrica, SecurityAlgorithms.HmacSha256Signature);

                    var token = new JwtSecurityToken(
                        issuer: Constantes.SolicitanteToken,
                        audience: Constantes.GrupoSolicitantes,
                        expires: DateTime.Now.AddMinutes(20), /*** expira em 20 minutos, poderia ser configurável, se for relevante ***/
                        signingCredentials: credenciais
                        );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                    throw new Exception("Usuário ou senha inválidos.");
            }
            catch (Exception erro)
            {
                return StatusCode(401, erro.Message);
            }
        }
    }
}