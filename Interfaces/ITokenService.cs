using System;

namespace P10_WebApi.Interfaces;

public interface ITokenService
{


    public string CreateToken(Guid userId, string email, string username, int timeInDays);

    public string CreateToken(string userId, string email, string username, int timeInDays);

    public string VerifyTokenAndGetId(string token);


}
