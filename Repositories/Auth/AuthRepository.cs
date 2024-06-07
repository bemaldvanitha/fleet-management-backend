using fleet_management_backend.Data;
using fleet_management_backend.Models.Domains;
using fleet_management_backend.Models.DTO.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace fleet_management_backend.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly FleetManagerDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(FleetManagerDbContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            try
            {
                var isAccountExist = await _context.User.Include(x => x.UserType).FirstOrDefaultAsync(u => u.MobileNumber == 
                    loginRequest.MobileNumber);

                if (isAccountExist == null)
                {
                    return new LoginResponseDTO 
                    {
                        Message = "Authentication Error",
                        StatusCode = 401,
                        Token = ""
                    };
                }

                string hashedPassword = string.Join("", MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(loginRequest.Password))
                        .Select(x => x.ToString("x2")));

                if(!isAccountExist.Password.Equals(hashedPassword))
                {
                    return new LoginResponseDTO
                    {
                        Message = "Authentication Error",
                        StatusCode = 401,
                        Token = ""
                    };
                }

                var token = GenerateJWTToken(isAccountExist);

                return new LoginResponseDTO
                {
                    Message = "Login Success",
                    StatusCode = 200,
                    Token = token
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new LoginResponseDTO
                {
                    Message = ex.Message,
                    StatusCode = 500,
                    Token = ""
                };
            }
        }

        public async Task<SignupResponseDTO> SignUp(SignupRequestDTO signupRequest)
        {
            try
            {
                var isEmailExist = await _context.User.FirstOrDefaultAsync(u => u.Email == signupRequest.Email);
                var isMobileNumberExist = await _context.User.FirstOrDefaultAsync(u => u.MobileNumber == signupRequest.MobileNumber);

                if (isEmailExist != null && signupRequest.Email != "")
                {
                    return new SignupResponseDTO
                    {
                        Message = "Email Already Exist",
                        StatusCode = 400,
                        Token = ""
                    };
                }

                if (isMobileNumberExist != null)
                {
                    return new SignupResponseDTO
                    {
                        Message = "Mobile Number Already Exist",
                        StatusCode = 400,
                        Token = ""
                    };
                }

                string hashedPassword = string.Join("", MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(signupRequest.Password))
                        .Select(x => x.ToString("x2")));

                var userType = await _context.UserType.FirstOrDefaultAsync(x => x.Type == signupRequest.UserType);

                if (userType == null)
                {
                    userType = new UserType
                    {
                        Type = signupRequest.UserType
                    };

                    await _context.UserType.AddAsync(userType);
                    await _context.SaveChangesAsync();
                }

                var user = new User
                {
                    DisplayName = signupRequest.DisplayName,
                    Email = signupRequest.Email,
                    MobileNumber = signupRequest.MobileNumber,
                    Password = hashedPassword,
                    UserTypeId = userType.Id,
                };

                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();

                user.UserType = userType;
                var token = GenerateJWTToken(user);

                return new SignupResponseDTO
                {
                    Message = "Sign up successfully",
                    StatusCode = 201,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SignupResponseDTO 
                { 
                    Message = ex.Message,
                    StatusCode = 500,
                    Token = ""
                };
            }
        }

        public async Task<UserProfileResponseDTO> UserProfile(Guid id)
        {
            try
            {
                var user = await _context.User.Include(u => u.UserType).FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    return new UserProfileResponseDTO
                    {
                        Message = "User Profile Not Found",
                        StatusCode = 404
                    };
                }

                return new UserProfileResponseDTO
                {
                    Message = "User Profile Found",
                    StatusCode = 200,
                    UserProfile = new UserProfile
                    {
                        Id = id,
                        DisplayName = user.DisplayName,
                        Email = user.Email ?? String.Empty,
                        MobileNumber = user.MobileNumber,
                        UserType = user.UserType.Type
                    }
                };

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                
                return new UserProfileResponseDTO 
                { 
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("mobile", user.MobileNumber ?? string.Empty),
                new Claim("type", user.UserType.Type ?? string.Empty)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(4),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
