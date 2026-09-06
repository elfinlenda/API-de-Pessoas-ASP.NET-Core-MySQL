using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Pessoa")]
public class PessoaControle : ControllerBase
{

    // atributos da classe PessoaRepositorio

    private readonly PessoaRepositorio _pessoaRepositorio;

    // construror
    
    public PessoaControle(PessoaRepositorio pessoaRepositorio)
    {
        _pessoaRepositorio = pessoaRepositorio;
    }

    // rota de cadastro 
    
    [HttpPost]

    public IActionResult Cadastrar([FromBody] Pessoa p)
    {
        if(p.Cidade == "")
        {
            return BadRequest(new {mensagem = "A cidade e obrigatorioa!!"});
        }else if(p.Nome == "")
        {
            return BadRequest(new {mensagem = "O nome e obrigatorioa!!"});         
        }else if(p.Idade < 0 || p.Idade > 120)
        {
            return BadRequest(new {mensagem = "A idade deve estar entre 0 e 120!!"});
        }
        else
        {
            var obj = _pessoaRepositorio.CadastrarPessoa(p);
            return Created(string.Empty, obj);
        }
    }

    // Rota de selecao

    [HttpGet]
    public List<Pessoa> Selecionar()
    {
        return _pessoaRepositorio.SelecionarPessoas();
    }

    // rota de altereacao 

    [HttpPut("{codigo}")]

    public IActionResult Alterar(int codigo,[FromBody] Pessoa p)
    {
        if(!_pessoaRepositorio.ExistePessoa(codigo))
        {
            return NotFound(new {mensagem = "O codigo informado nao existe!!"});
        }
        if(p.Cidade == "")
        {
            return BadRequest(new {mensagem = "A cidade e obrigatorioa!!"});
        }else if(p.Nome == "")
        {
            return BadRequest(new {mensagem = "O nome e obrigatorioa!!"});         
        }else if(p.Idade < 0 || p.Idade > 120)
        {
            return BadRequest(new {mensagem = "A idade deve estar entre 0 e 120!!"});
        }
        else
        {
            p.Codigo = codigo;

            _pessoaRepositorio.AlterarPessoa(p);

            return Ok(p);
        }


    }



    // rota de remocao 

    [HttpDelete("{codigo}")]

    public IActionResult Remover(int codigo  )
    {
        if(_pessoaRepositorio.ExistePessoa(codigo))
        {
             _pessoaRepositorio.RemoverPessoa(codigo);
             return Ok(new {mensagem = "Pessoa removida com sucesso"});

        }
        else
        {
            return NotFound(new { mensagem = "Codigo nao encontrado"});
        }
    }




    // [HttpGet]
    // public string PrimeiraRota()
    // {
    //     return "DAAAWWM BRO!!";
    // }

    // [HttpPost]
    // public Pessoa ManipularModeloPessoa([FromBody]Pessoa p)
    // {
    //     return p;

    // }
}