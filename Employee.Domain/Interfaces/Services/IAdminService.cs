using Employee.Domain.Models.Requests;
using Employee.Domain.Models.Responses;
using System.Threading.Tasks;

namespace Employee.Domain.Interfaces.Services
{
    public interface IAdminService
    {
        Task<AdminResponse.CreateAdminResponse> CreateAdminAsync(AdminRequest.CreateAdminRequest request);
    }
}