using AutoMapper;
using CustomerAPI.Interfaces;
using CustomerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customerDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null) return NoContent();
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = _mapper.Map<Customer>(customerDto);
            await _customerService.CreateCustomer(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customerDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customerDto)
        {
            if (id != customerDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = _mapper.Map<Customer>(customerDto);
            var result = await _customerService.UpdateCustomer(customer);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomer(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }

}
