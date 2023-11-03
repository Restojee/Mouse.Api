using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mouse.NET.Auth.Models;
using Mouse.NET.Data.Models;
using Mouse.NET.Storage;
using Mouse.NET.Users.Data;
using Mouse.NET.Users.Models;
using Mouse.Stick.Controllers.Auth;

namespace Mouse.NET.Auth.Services;

public class AuthService: IAuthService
{     
    private readonly IMapper mapper;
    private readonly JwtService jwtService;
    private readonly IUserRepository usersRepository;

    public AuthService(IMapper mapper, IUserRepository usersRepository, JwtService jwtService)
    {
        this.mapper = mapper;
        this.jwtService = jwtService;
        this.usersRepository = usersRepository;

    }

    public int GetAuthorizedUserId()
    {
        return this.jwtService.GetUserId();
    }
    
    public async Task<Account> RegisterAccount (RegisterAccountRequest registerAccountRequest)
    {
   
        var userExists = await this.usersRepository.GetUserByUserName(registerAccountRequest.UserName);
        if (userExists != null)
        {
            throw new BadHttpRequestException("Пользователь с таким именем уже зарегистрирован");
        }

        var hashSalt = AuthUtils.GetHashPassword(registerAccountRequest.Password);

        var newUser = await this.usersRepository.CreateUser(new UserEntity
        {
            UserName = registerAccountRequest.UserName,
            PasswordHash = hashSalt.Hash,
            Salt = hashSalt.Salt,
        });

        return new Account 
        {
            User = this.mapper.Map<UserEntity, User>(newUser),
            AccessToken = JwtUtils.GenerateJwtToken(AuthUtils.GetUserClaims(newUser.Id, newUser.UserName, newUser.Email))
        };
    }
    
    public async Task<Account> LoginAccount([FromBody] LoginAccountRequest loginAccountRequest)
    {
        var userExists = await this.usersRepository.GetUserByUserName(loginAccountRequest.UserName);
        if (userExists == null)
        {
            throw new BadHttpRequestException("Пользователь с таким именем уже зарегистрирован");
        }
                
        var isAccountVerify = AuthUtils.VerifyHashPassword(userExists.PasswordHash, loginAccountRequest.Password, userExists.Salt);
        if (!isAccountVerify)
        {
            throw new BadHttpRequestException("Неправильный логин или пароль");
        }

        return new Account
        {
            User = this.mapper.Map<UserEntity, User>(userExists),
            AccessToken = JwtUtils.GenerateJwtToken(AuthUtils.GetUserClaims(userExists.Id, userExists.UserName, userExists.Email))
        };
    }
}