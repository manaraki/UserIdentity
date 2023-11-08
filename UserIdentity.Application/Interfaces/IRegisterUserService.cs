using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Application.Dtos;

namespace UserIdentity.Application.Interfaces
{
    public interface IRegisterUserService
    {
        Task<int> Execute(UserRegisterDto userRegisterDto);
    }
}
