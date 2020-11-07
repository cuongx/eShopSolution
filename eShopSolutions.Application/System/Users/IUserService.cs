using eShop.Solution.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolutions.Application.System.Users
{
  public  interface IUserService
    {
        Task<string> Authencation(LoginResquest request);
        Task<bool> Register(RegisterRequest request);
    }
}
