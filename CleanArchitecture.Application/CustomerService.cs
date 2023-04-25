using AutoMapper;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Dto;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerReposiotry _custrepo;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerReposiotry custrepo,IMapper mapper)
        {
            _custrepo = custrepo;
            _mapper = mapper;
        }
        public async Task CreateAsync(CustomerDto customer)
        {
            var actualCustomer = _mapper.Map<Customer>(customer);

            await _custrepo.CreateAsync(actualCustomer);
        }

        public async Task DeleteAsync(string id)
        {
            await _custrepo.DeleteAsync(id);
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var customer = await _custrepo.GetAllAsync();
            return _mapper.Map<List<CustomerDto>>(customer);            
        }

        public async Task<CustomerDto> GetByIdAsync(string id)
        {
            var customer = await _custrepo.GetByIdAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateAsync(string id, CustomerDto customer)
        {
            var customerToBeUpdated = _mapper.Map<Customer>(customer);
            await _custrepo.UpdateAsync(id, customerToBeUpdated);
        }
    }
}
