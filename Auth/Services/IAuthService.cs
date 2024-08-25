using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mouse.NET.Auth.Models;
using Mouse.NET.Data.Models;
using Mouse.NET.Users.Data;
using Mouse.NET.Users.Models;

namespace Mouse.Stick.Controllers.Auth;

public interface IAuthService
{
    public Task<Account> RegisterAccount(RegisterAccountRequest registerAccountRequest);
    
    public Task<Account> LoginAccount(LoginAccountRequest loginAccountRequest);

    public Task ChangePassword(ChangePasswordAccountRequest changePasswordAccountRequest);

    public int? GetAuthorizedUserId();
}