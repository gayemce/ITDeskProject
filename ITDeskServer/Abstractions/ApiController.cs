using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITDeskServer.Abstractions;

//DRY - Don't repeat yourself prencible
[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")] //attribute // Kullanılmasını istemediğimiz endpointte [AllowAnonymous] yazılır.
public abstract class ApiController : ControllerBase
{
}
