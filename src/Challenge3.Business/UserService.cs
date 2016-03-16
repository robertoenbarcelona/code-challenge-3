

namespace Challenge3.Business
{
    using Challenge3.Data;
    using Infrastructure.Business.Service;
    using Infrastructure.Data.Repositories;
    using System;
    using System.Linq;

    /// <summary>
    /// Holds business operation for Users
    /// </summary>
    internal class UserService : Service<User>, IUserService
    {
        public UserService(IRepositoryAsync<User> repository)
            : base(repository)
        {

        }
    }
}
