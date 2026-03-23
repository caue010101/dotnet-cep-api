using Microsoft.AspNetCore.Mvc;
using CepSystem.Application.Interfaces;

namespace CepSystem.API.Controllers
{

    public class ViaCepController : ControllerBase
    {

        private IAddressService _addressService;

        public ViaCepController(IAddressService addressService)
        {
            this._addressService = addressService;
        }

        public async Task<IActionResult> GetAddressByZipCodeAsync(string zipCode)
        {

            var address = await _addressService.GetByZipCodeAsync(zipCode);

            if (address == null)
            {
                return NotFound(new { message = "Address not found or invalid ZipCode " });
            }

            return Ok(zipCode);
        }
    }
}
