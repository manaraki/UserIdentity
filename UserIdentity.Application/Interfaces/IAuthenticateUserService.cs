﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Application.Dtos;
using UserIdentity.Domain.Entities.User;

namespace UserIdentity.Application.Interfaces
{
    public interface IAuthenticateUserService
    {
        Task<User> Execute(UserAuthenticateDto userAuthenticateDto);
    }
}
