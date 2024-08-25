using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mouse.NET.Auth.Models;
using Mouse.NET.Data.Models;
using Mouse.NET.Invites.Data;
using Mouse.NET.Storage;
using Mouse.NET.Users.Data;
using Mouse.NET.Users.Models;
using Mouse.Stick.Controllers.Auth;

namespace Mouse.NET.Auth.Services;

public class AuthService: IAuthService
{     
    private readonly IMapper mapper;
    private readonly JwtService jwtService;
    private readonly IInviteRepository inviteRepository;
    private readonly IUserRepository userRepository;

    public AuthService(IMapper mapper, IUserRepository userRepository, JwtService jwtService, IInviteRepository inviteRepository)
    {
        this.mapper = mapper;
        this.jwtService = jwtService;
        this.inviteRepository = inviteRepository;
        this.userRepository = userRepository;
    }

    public int? GetAuthorizedUserId()
    {
        return this.jwtService.GetUserId();
    }
    
    public async Task<Account> RegisterAccount (RegisterAccountRequest registerAccountRequest)
    {

        var inviteExists = await this.inviteRepository.GetWorkedInvite(registerAccountRequest.InviteToken);
        if (inviteExists == null)
        {
            throw new BadHttpRequestException("Приглашение с таким номером не найдено или срок его действия истек");
        }
        
        var userExists = await this.userRepository.GetUserByUserName(registerAccountRequest.UserName);
        if (userExists != null)
        {
            throw new BadHttpRequestException("Пользователь с таким именем уже зарегистрирован");
        }

        var hashSalt = AuthUtils.GetHashPassword(registerAccountRequest.Password);

        var newUser = await this.userRepository.CreateUser(new UserEntity
        {
            UserName = registerAccountRequest.UserName,
            PasswordHash = hashSalt.Hash,
            Salt = hashSalt.Salt,
        });
        
        await this.inviteRepository.UseInvite(registerAccountRequest.InviteToken);
        
        return new Account 
        {
            User = this.mapper.Map<UserEntity, User>(newUser),
            AccessToken = JwtUtils.GenerateJwtToken(AuthUtils.GetUserClaims(newUser.Id, newUser.UserName, newUser.Email))
        };
    }
    
    public async Task<Account> LoginAccount(LoginAccountRequest loginAccountRequest)
    {
        var userExists = await this.userRepository.GetUserByUserName(loginAccountRequest.UserName);
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

    public async Task ChangePassword(ChangePasswordAccountRequest changePasswordAccountRequest)
    {
        var user = await this.userRepository.GetUser(this.GetAuthorizedUserId().GetValueOrDefault());
        if (user == null)
        {
            throw new BadHttpRequestException("Пользователь не найден");
        }
        var hashSalt = AuthUtils.GetHashPassword(changePasswordAccountRequest.NewPassword);
        user.PasswordHash = hashSalt.Hash;
        user.Salt = hashSalt.Salt;

        await this.userRepository.UpdateUser(user);
    }
}