using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Dto;
using AutoMapper;
using CleanArchitecture.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CleanArchitecture.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
             
        }
        public async Task<UserDto?> AuthenticateAsync(string userName, string passwordText)
        {
            var userFromDB = await _userRepository.GetUsersByNameAsync(userName);

            if (userFromDB == null || userFromDB.passwordKey == null)
                return null;

            if (!MatchPasswordHash(passwordText, userFromDB.password!, userFromDB.passwordKey!))
                return null;

            var userFromDBConverted = _mapper.Map<UserValuesDto>(userFromDB);

            var authenticatedUser = new UserDto()
            {
                userName = userFromDB.userName,
                Token = CreateJWT(userFromDBConverted)
            };

            return authenticatedUser;
        }

        public async Task RegisterAsync(string userName, string password)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            UserValuesDto user = new()
            {
                userName = userName,
                password = passwordHash,
                passwordKey = passwordKey
            };

            var userToDB = _mapper.Map<User?>(user);

            await _userRepository.CreateUserAsync(userToDB);
        }

        public async Task<bool> UserAlreadyExistsAsync(string userName)
        {
            return await _userRepository.UserAlreadyExistsAsync(userName);
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }

                return true;
            }
        }

        private string CreateJWT(UserValuesDto user)
        {
            //var secretKey = _configuration.GetSection("AppSettings:Key").Value;
            // Using symetric key in this.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTSettings").GetChildren().FirstOrDefault(j => j.Key == "key")!.Value!));

            var claims = new Claim[]{
                new Claim(ClaimTypes.Name , user.userName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id!.ToString())
            };

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}
