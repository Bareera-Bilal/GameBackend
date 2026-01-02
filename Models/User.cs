using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using P10_WebApi.Models.AbstractClasses;

namespace P10_WebApi.Models;

// public class User : BaseEntity
// {
//     public required string Username { get; set; }
//     public required string Email { get; set; }
//     public required string Password { get; set; }
//     public string? PhoneNumber { get; set; }
//     public required string OTP { get; set; }
//     public DateTime? OTPExpiry { get; set; }
//     public bool IsPhoneVerified { get; set; }
// }
// public class User : BaseEntity
// {
//    public required string Username { get; set; }
//     public required string Email { get; set; }
//     public required string Password { get; set; }
//     public required string PhoneNumber { get; set; }
//     public bool IsPhoneVerified { get; set; }
//     public DateTime CreatedAt { get; set; }
// }


    public class User : BaseEntity
    {
        [BsonElement("username")]
        public required string Username { get; set; }

        [BsonElement("email")]
        public required string Email { get; set; }

        [BsonElement("password")]
        public required string Password { get; set; }

        [BsonElement("phoneNumber")]
        public required string PhoneNumber { get; set; }

        [BsonElement("isPhoneVerified")]
        public bool IsPhoneVerified { get; set; } = false;

    }

