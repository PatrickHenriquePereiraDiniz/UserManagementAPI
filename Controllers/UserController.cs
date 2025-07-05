using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var users = UserRepository.GetAll();
            if (users.Count == 0)
                return Ok(new { message = "Nenhum usuário encontrado.", data = users });

            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao listar usuários.", error = ex.Message });
        }
    }


    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var user = UserRepository.GetById(id);
            return user == null
                ? NotFound(new { message = $"Usuário com ID {id} não encontrado." })
                : Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno ao buscar usuário.", error = ex.Message });
        }
    }


    [HttpPost]
    public IActionResult Create([FromBody] User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = UserRepository.Add(user);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = UserRepository.Update(id, user);
        return success ? NoContent() : NotFound(new { message = $"Usuário com ID {id} não encontrado." });
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = UserRepository.Delete(id);
        return success ? NoContent() : NotFound();
    }
}
