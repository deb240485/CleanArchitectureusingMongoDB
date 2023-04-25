using CleanArchitecture.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Contracts
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(string id);
        Task CreateAsync(CustomerDto customer);
        Task UpdateAsync(string id, CustomerDto customer);
        Task DeleteAsync(string id);
    }
}
