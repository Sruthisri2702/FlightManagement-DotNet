using CommonServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginServices.Model
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

        public string Response { get; set; }


        public AuthenticateResponse(User user, string token, string res)
        {
            Id = user.UserId;
            Name = user.Name;
            Username = user.Username;
            Role = user.Role;
            Token = token;
            Response = res;

        }
    }
}
