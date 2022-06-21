using CommonServices.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace LoginServices.Model
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;
        private readonly AppDbContext _context;

        public SQLUserRepository(IOptions<AppSettings> appSettings, AppDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;

        }
        //public User Add(User user)
        //{
        //    context.Users.Add(user);
        //    context.SaveChanges();
        //    return user;
        //}
        //public User Validate(User u)
        //{
        //    var TotalUsers = context.Users;
        //    var validUser = new User();
        //    if (TotalUsers.Count() > 0)
        //    {
        //        validUser = TotalUsers.Where(t => t.UserName == u.UserName && t.Password == u.Password).FirstOrDefault();
        //    }

        //    if (validUser == null)
        //    {
        //        return validUser = new User();
        //    }
        //    else
        //    return validUser;

        //}

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //private List<User> _users = new List<User>
        //{
        //    new User { UserId = 1, Name = "Test", Username = "test", Password = "test", Role = "Admin"},
        //    new User { UserId = 1, Name = "World", Username = "world", Password = "world", Role = "User"}

        //};

        //private readonly AppSettings _appSettings;

        //public SQLUserRepository(IOptions<AppSettings> appSettings)
        //{
        //    _appSettings = appSettings.Value;
        //}

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.User.FirstOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == model.Password);
            var user_role = _context.User.FirstOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Role.ToLower() == model.Role.ToLower());

            // return null if user not found
            if (user == null)
                return new AuthenticateResponse(new User { Username = model.Username, Password = model.Password },"", "Username or Password is incorrect");
           else if(user_role == null)
                return new AuthenticateResponse(new User { Username = model.Username, Password = model.Password }, "", "You are not authorized");

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token,"Success");
        }

        public IEnumerable<User> GetAll()
        {
            return _context.User;
        }

        public User GetById(int id)
        {
            return _context.User.FirstOrDefault(x => x.UserId == id);
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 10 mins
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToLower())

                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User Add(User u)
        {
            _context.User.Add(u);
            _context.SaveChanges();
            return u;
        }
    }
}


