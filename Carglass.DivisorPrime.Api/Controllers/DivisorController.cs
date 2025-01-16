using Carglass.DivisorPrime.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Carglass.DivisorPrime.Api.Controllers;

[ExcludeFromCodeCoverage]
[Route("api/[controller]")]
[ApiController]
public class DivisorController : ControllerBase
{
    private readonly IDivisorService _service;

    public DivisorController(IDivisorService service)
    {
        _service = service;
    }

    [HttpGet("{number?}")]
    public IActionResult Get(string? number)
    {
        var response = _service.Get(number);

        return StatusCode((int)response.StatusCode, response);
    }
}
