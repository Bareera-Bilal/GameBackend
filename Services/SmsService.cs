// // using P10_WebApi.Interfaces;
// // using Twilio;
// // using Twilio.Rest.Api.V2010.Account;
// // using Microsoft.Extensions.Configuration;

// // namespace P10_WebApi.Services
// // {
// //     public class SmsService : ISmsService
// //     {
// //         private readonly IConfiguration _configuration;

// //         public SmsService(IConfiguration configuration)
// //         {
// //             _configuration = configuration;
// //         }

// //         public async Task SendSmsAsync(string phoneNumber, string message)
// //         {
// //             var accountSid = _configuration["Twilio:AccountSid"];
// //             var authToken = _configuration["Twilio:AuthToken"];
// //             var fromNumber = _configuration["Twilio:FromNumber"];

// //             TwilioClient.Init(accountSid, authToken);

// //             await MessageResource.CreateAsync(
// //                 body: message,
// //                 from: new Twilio.Types.PhoneNumber(fromNumber),
// //                 to: new Twilio.Types.PhoneNumber(phoneNumber)
// //             );
// //         }
// //     }
// // }
// using System;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Configuration;
// using P10_WebApi.Interfaces;
// using Twilio;
// using Twilio.Rest.Api.V2010.Account;
// using Twilio.Types;

// namespace P10_WebApi.Services
// {
//     public class SmsService : ISmsService
//     {
//         private readonly IConfiguration _configuration;

//         public SmsService(IConfiguration configuration)
//         {
//             _configuration = configuration;
//         }

//         public async Task SendSmsAsync(string phoneNumber, string message)
//         {
//             if (string.IsNullOrWhiteSpace(phoneNumber))
//                 throw new ArgumentException("Phone number is required");

//             if (string.IsNullOrWhiteSpace(message))
//                 throw new ArgumentException("Message is required");

//             var accountSid = _configuration["Twilio:AccountSid"];
//             var authToken = _configuration["Twilio:AuthToken"];
//             var fromNumber = _configuration["Twilio:FromNumber"];

//             if (string.IsNullOrWhiteSpace(accountSid) ||
//                 string.IsNullOrWhiteSpace(authToken) ||
//                 string.IsNullOrWhiteSpace(fromNumber))
//             {
//                 throw new InvalidOperationException("Twilio configuration is missing");
//             }

//             try
//             {
//                 TwilioClient.Init(accountSid, authToken);

//                 await MessageResource.CreateAsync(
//                     body: message,
//                     from: new PhoneNumber(fromNumber),
//                     to: new PhoneNumber(phoneNumber)
//                 );
//             }
//             catch (Exception ex)
//             {
//                 // Log but don't expose secrets
//                 Console.WriteLine($"Twilio SMS Error: {ex.Message}");
//                 throw;
//             }
//         }
//     }
// }
