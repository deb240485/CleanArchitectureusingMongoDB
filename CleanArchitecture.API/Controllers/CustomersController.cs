using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _custService;

        public CustomersController(ICustomerService custService)
        {
            _custService = custService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _custService.GetAllAsync());
        }
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await _custService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(customer.lastupdatedon == DateTime.MinValue )
            { customer.lastupdatedon = DateTime.UtcNow;}
            await _custService.CreateAsync(customer);
            return Ok(customer.Id);
        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, CustomerDto customerIn)
        {
            var customer = await _custService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            await _custService.UpdateAsync(id, customerIn);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _custService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            await _custService.DeleteAsync(customer.Id!);
            return NoContent();
        }
    }
}
