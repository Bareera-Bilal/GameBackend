

// using System;
// using System.Security.Cryptography;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using MongoDB.Driver;
// using P10_WebApi.Interfaces;
// using P10_WebApi.Models;
// using P10_WebApi.Services;

// namespace P10_WebApi.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class UserController : ControllerBase
//     {
//         private readonly MongoDbService db;
//         private readonly ITokenService tokenService;
//         // private readonly ISmsService smsService;

//         public UserController(
//             MongoDbService mongoDb,
//             ITokenService tokenService)
//         // ISmsService smsService)
//         {
//             db = mongoDb;
//             this.tokenService = tokenService;
//             // this.smsService = smsService;
//         }

//         // ========================= REGISTER =========================

//         // [HttpPost("Register")]
//         // public async Task<IActionResult> Register([FromBody] RegisterDto dto)
//         // {
//         //     if (dto == null ||
//         //         string.IsNullOrWhiteSpace(dto.Username) ||
//         //         string.IsNullOrWhiteSpace(dto.Email) ||
//         //         string.IsNullOrWhiteSpace(dto.Password) ||
//         //         string.IsNullOrWhiteSpace(dto.PhoneNumber))
//         //     {
//         //         return BadRequest(new { message = "All fields are required!" });
//         //     }

//         //     if (dto.Password.Length < 8)
//         //     {
//         //         return BadRequest(new { message = "Password must be at least 8 characters" });
//         //     }

//         //     // Normalize
//         //     dto.Email = dto.Email.Trim().ToLower();
//         //     dto.Username = dto.Username.Trim().ToLower();
//         //     dto.PhoneNumber = dto.PhoneNumber.Trim();

//         //     // Check duplicates
//         //     var existingUser = await db.Users.Find(u =>
//         //         u.Email == dto.Email ||
//         //         u.PhoneNumber == dto.PhoneNumber ||
//         //         u.Username == dto.Username
//         //     ).FirstOrDefaultAsync();

//         //     if (existingUser != null)
//         //     {
//         //         if (existingUser.Email == dto.Email)
//         //             return BadRequest(new { message = "Email already registered" });

//         //         if (existingUser.PhoneNumber == dto.PhoneNumber)
//         //             return BadRequest(new { message = "Phone number already registered" });

//         //         if (existingUser.Username == dto.Username)
//         //             return BadRequest(new { message = "Username already taken" });
//         //     }

//         //     // Generate OTP
//         //     var otp = RandomNumberGenerator.GetInt32(100000, 1000000);

//         //     var user = new User
//         //     {
//         //         Username = dto.Username,
//         //         Email = dto.Email,
//         //         PhoneNumber = dto.PhoneNumber,
//         //         Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
//         //         OTP = BCrypt.Net.BCrypt.HashPassword(otp.ToString()),
//         //         OTPExpiry = DateTime.UtcNow.AddMinutes(5),
//         //         IsPhoneVerified = false,
//         //         CreatedAt = DateTime.UtcNow
//         //     };

//         //     await db.Users.InsertOneAsync(user);

//         //     // SMS should not crash API
//         //     try
//         //     {
//         //         await smsService.SendSmsAsync(
//         //             dto.PhoneNumber,
//         //             $"Your verification OTP is {otp}. Valid for 5 minutes."
//         //         );
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         Console.WriteLine("SMS ERROR: " + ex.Message);
//         //     }

//         //     return Ok(new
//         //     {
//         //         message = "User registered successfully. OTP sent to phone."
//         //     });
//         // }


//         // ========================= VERIFY OTP =========================

//         //     [HttpPost("verify-otp")]
//         //     public async Task<IActionResult> VerifyOtp(
//         //         [FromQuery] string phoneNumber,
//         //         [FromQuery] int otp)
//         //     {
//         //         var user = await db.Users
//         //             .Find(u => u.PhoneNumber == phoneNumber)
//         //             .FirstOrDefaultAsync();

//         //         if (user == null)
//         //             return BadRequest(new { message = "User not found" });

//         //         if (user.OTPExpiry < DateTime.UtcNow)
//         //             return BadRequest(new { message = "OTP expired" });

//         //         if (!BCrypt.Net.BCrypt.Verify(otp.ToString(), user.OTP))
//         //             return BadRequest(new { message = "Invalid OTP" });

//         //         var update = Builders<User>.Update
//         //             .Set(u => u.IsPhoneVerified, true)
//         //             .Unset(u => u.OTP)
//         //             .Unset(u => u.OTPExpiry);

//         //         await db.Users.UpdateOneAsync(u => u.PhoneNumber == phoneNumber, update);

//         //         return Ok(new { message = "Phone number verified successfully" });
//         //     }



//         // ========================= REGISTER (NO OTP) =========================

//         // [HttpPost("Register")]
//         // public async Task<IActionResult> Register([FromBody] RegisterDto Dtos)
//         // {
//         //     if (Dtos == null ||
//         //         string.IsNullOrWhiteSpace(Dtos.Username) ||
//         //         string.IsNullOrWhiteSpace(Dtos.Email) ||
//         //         string.IsNullOrWhiteSpace(Dtos.Password) ||
//         //         string.IsNullOrWhiteSpace(Dtos.PhoneNumber))
//         //     {
//         //         return BadRequest(new { message = "All fields are required!" });
//         //     }

//         //     if (Dtos.Password.Length < 8)
//         //     {
//         //         return BadRequest(new { message = "Password must be at least 8 characters" });
//         //     }

//         //     // Normalize input
//         //     Dtos.Email = Dtos.Email.Trim().ToLower();
//         //     Dtos.Username = Dtos.Username.Trim().ToLower();
//         //     Dtos.PhoneNumber = Dtos.PhoneNumber.Trim();

//         //     // Check duplicates
//         //     var existingUser = await db.Users.Find(u =>
//         //         u.Email == Dtos.Email ||
//         //         u.PhoneNumber == Dtos.PhoneNumber ||
//         //         u.Username == Dtos.Username
//         //     ).FirstOrDefaultAsync();

//         //     if (existingUser != null)
//         //     {
//         //         if (existingUser.Email == Dtos.Email)
//         //             return BadRequest(new { message = "Email already registered" });

//         //         if (existingUser.PhoneNumber == Dtos.PhoneNumber)
//         //             return BadRequest(new { message = "Phone number already registered" });

//         //         if (existingUser.Username == Dtos.Username)
//         //             return BadRequest(new { message = "Username already taken" });
//         //     }

//         //     var user = new User
//         //     {
//         //         Username = Dtos.Username,
//         //         Email = Dtos.Email,
//         //         PhoneNumber = Dtos.PhoneNumber,
//         //         Password = BCrypt.Net.BCrypt.HashPassword(Dtos.Password),
//         //         IsPhoneVerified = true, // ✅ directly verified
//         //         CreatedAt = DateTime.UtcNow
//         //     };

//         //     await db.Users.InsertOneAsync(user);

//         //     return Ok(new
//         //     {
//         //         message = "User registered successfully"
//         //     });
//         // }





//         // ========================= REGISTER =========================

//         [HttpPost("Register")]
//         public async Task<IActionResult> Register([FromBody] RegisterDto dto)
//         {
//             if (dto == null ||
//                 string.IsNullOrWhiteSpace(dto.Username) ||
//                 string.IsNullOrWhiteSpace(dto.Email) ||
//                 string.IsNullOrWhiteSpace(dto.Password) ||
//                 string.IsNullOrWhiteSpace(dto.PhoneNumber))
//             {
//                 return BadRequest(new { message = "All fields are required!" });
//             }

//             if (dto.Password.Length < 8)
//             {
//                 return BadRequest(new { message = "Password must be at least 8 characters" });
//             }

//             // Normalize
//             dto.Email = dto.Email.Trim().ToLower();
//             dto.Username = dto.Username.Trim().ToLower();
//             dto.PhoneNumber = dto.PhoneNumber.Trim();

//             // Check duplicates
//             var existingUser = await db.Users.Find(u =>
//                 u.Email == dto.Email ||
//                 u.PhoneNumber == dto.PhoneNumber ||
//                 u.Username == dto.Username
//             ).FirstOrDefaultAsync();

//             if (existingUser != null)
//             {
//                 if (existingUser.Email == dto.Email)
//                     return BadRequest(new { message = "Email already registered" });

//                 if (existingUser.PhoneNumber == dto.PhoneNumber)
//                     return BadRequest(new { message = "Phone number already registered" });

//                 if (existingUser.Username == dto.Username)
//                     return BadRequest(new { message = "Username already taken" });
//             }

//             var user = new User
//             {
//                 Username = dto.Username,
//                 Email = dto.Email,
//                 PhoneNumber = dto.PhoneNumber,
//                 Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
//                 IsPhoneVerified = true, // Directly mark as verified
//                 CreatedAt = DateTime.UtcNow
//             };

//             await db.Users.InsertOneAsync(user);

//             return Ok(new
//             {
//                 message = "User registered successfully."
//             });
//         }

//     }
// }

using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using P10_WebApi.Interfaces;
using P10_WebApi.Models;
using P10_WebApi.Services;
using System.Threading.Tasks;

namespace P10_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MongoDbService _db;
        private readonly ITokenService _tokenService;

        public UserController(MongoDbService mongoDb, ITokenService tokenService)
        {
            _db = mongoDb ?? throw new ArgumentNullException(nameof(mongoDb));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        // ========================= REGISTER (NO OTP) =========================
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.Username) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password) ||
                string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                return BadRequest(new { message = "All fields are required!" });
            }

            if (dto.Password.Length < 8)
                return BadRequest(new { message = "Password must be at least 8 characters." });

            // Normalize input
            dto.Email = dto.Email.Trim().ToLower();
            dto.Username = dto.Username.Trim().ToLower();
            dto.PhoneNumber = dto.PhoneNumber.Trim();

            // Check duplicates
            var existingUser = await _db.Users.Find(u =>
                u.Email == dto.Email ||
                u.PhoneNumber == dto.PhoneNumber ||
                u.Username == dto.Username
            ).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                if (existingUser.Email == dto.Email)
                    return BadRequest(new { message = "Email already registered." });

                if (existingUser.PhoneNumber == dto.PhoneNumber)
                    return BadRequest(new { message = "Phone number already registered." });

                if (existingUser.Username == dto.Username)
                    return BadRequest(new { message = "Username already taken." });
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsPhoneVerified = true, // Directly verified
                CreatedAt = DateTime.UtcNow
            };

            await _db.Users.InsertOneAsync(user);

            return Ok(new
            {
                message = "User registered successfully.",
                userId = user.Id.ToString() // Optional: return user ID only, not password
            });
        }
    }
}
