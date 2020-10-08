using Microsoft.AspNetCore.Mvc;
using Server.Dtos.Security;
using System.Threading.Tasks;

namespace Server.Services.Interfaces
{
    public interface IUserService
    {
        Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model);
    }
}
