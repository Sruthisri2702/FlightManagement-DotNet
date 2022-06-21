using CommonServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LoginServices.Model
{
        public interface IUserRepository
        {
            AuthenticateResponse Authenticate(AuthenticateRequest model);
            IEnumerable<User> GetAll();
            User GetById(int id);
            User Add(User u);

        }
}
