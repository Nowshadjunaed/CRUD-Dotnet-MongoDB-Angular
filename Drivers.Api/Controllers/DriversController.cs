using Drivers.Api.Models;
using Drivers.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drivers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{


    private readonly ILogger<DriversController> _logger;
    private readonly DriverService _driverService;

    public DriversController(ILogger<DriversController> logger, DriverService driverService)
    {
        _logger = logger;
        _driverService = driverService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDrivers()
    {
        var drivers = await _driverService.GetAsync();
        return Ok(drivers);
    }
    [HttpPost]
    public async Task<IActionResult> CreateDrivers()
    {
        var driver = new Driver();
        driver.Name = "abc";
        driver.Team = "Communication";
        driver.Number = 1;
        await _driverService.CreateAsync(driver);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _driverService.DeleteAsync(id);
        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id)
    {
        await _driverService.UpdateAsync(id, 2);
        return NoContent();
    }


}
