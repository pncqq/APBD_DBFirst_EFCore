using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.DAL;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripRepository _repository;

    public TripsController(ITripRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTripsSorted()
    {
        var res = await _repository.GetTripsSortedAsync();
        return Ok(res);
    }

    [HttpDelete("/api/clients/{idClient:int}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var res = await _repository.DeleteClientAsync(idClient);

        return res switch
        {
            DatabaseErrors.ClientDeleted => Ok("Client deleted!"),
            DatabaseErrors.ClientHasTrips => StatusCode(409),
            DatabaseErrors.ClientNotPresent => StatusCode(404),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AddClientToTrip(AddClientToTripDTO data)
    {
        var result = await _repository.AddClientToTripAsync(data);

        return result ? StatusCode(201) : BadRequest();
    }
}