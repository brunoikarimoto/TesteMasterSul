using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    // Essa lista é uma forma de armazenar localmente os usuários
    private List<Usuario> usuarios = new List<Usuario>();

    // Assim que instanciado ele cria esses 3 usuários
    public UsuarioController(){
        usuarios.Add(new Usuario {Id = 1, Nome = "Bruno"});
        usuarios.Add(new Usuario {Id = 2, Nome = "Luciana"});
        usuarios.Add(new Usuario {Id = 3, Nome = "Suzy"});
    }

    // Rota para pegar todos os usuários
    [HttpGet]
    [Route("/usuario")]
    public ActionResult<IEnumerable<Usuario>> Get()
    {
        return usuarios;
    }

    //Rota para buscar um usuário por id
    [HttpGet]
    [Route("/usuario/{id}")]
    public ActionResult<Usuario> BuscarId([FromRoute] int id)
    {
        Usuario? usuario = usuarios.FirstOrDefault(u => u.Id == id);

        if(usuario is null) return BadRequest("Usuário não encontrado");

        return usuario;
    }
}
