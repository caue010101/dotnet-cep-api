
using Microsoft.AspNetCore.Mvc;
using CepSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CepSystem.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class ViaCepController : ControllerBase
    {

        private readonly IAddressService _addressService;

        public ViaCepController(IAddressService addressService)
        {
            this._addressService = addressService;
        }

        [Authorize]
        [HttpGet("{zipCode}")]
        public async Task<IActionResult> GetAddressByZipCodeAsync([FromRoute] string zipCode)
        {

            var address = await _addressService.GetByZipCodeAsync(zipCode);

            if (address == null) return NotFound(new { message = $"Address {zipCode} not found or invalid " });

            return Ok(address);
        }
    }
}
