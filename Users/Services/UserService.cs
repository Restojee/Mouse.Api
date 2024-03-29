﻿using AutoMapper;
using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.Storage;
using Mouse.NET.Users.Data;
using Mouse.NET.Users.Models;
using Mouse.Stick.Controllers.Auth;

namespace Mouse.NET.Users.services;

public class UserService : IUserService
{
    
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;
    private readonly IStorageService storageService;
    private readonly IAuthService authService;

    public UserService(IMapper mapper, IUserRepository userRepository, IStorageService storageService, IAuthService authService) {
        this.userRepository = userRepository;
        this.storageService = storageService;
        this.mapper = mapper;
        this.authService = authService;
    }
    
    public async Task<PagedResult<User>> GetUserCollection(UserCollectionGetRequest request)
    {
        return mapper.Map<PagedResult<UserEntity>, PagedResult<User>>(await this.userRepository.GetUserCollection(request));
    }

    public async Task<User> GetUser(int userId)
    {
        return mapper.Map<UserEntity, User>(await this.userRepository.GetUser(userId));
    }

    public async Task<User> CreateUser(UserCreateRequest request)
    {
        var userExists = await this.userRepository.GetUserByUserName(request.UserName);
        if (userExists != null)
        {
            throw new BadHttpRequestException("Пользователь с таким логином уже существует");
        }
        return mapper.Map<UserEntity, User>(await this.userRepository.CreateUser(mapper.Map<UserCreateRequest, UserEntity>(request)));
    }

    public async Task<User> UpdateUser(UserUpdateRequest request)
    {
        var userExists = await this.userRepository.GetUser(request.Id);
        if (userExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемый пользователь не найден");
        }
        return mapper.Map<UserEntity, User>(await this.userRepository.UpdateUser(mapper.Map<UserUpdateRequest, UserEntity>(request)));
    }

    public async Task<string> DeleteUser(int userId)
    {
        var userExists = await this.userRepository.GetUser(userId);
        if (userExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемый пользователь не найден");
        }
        await this.userRepository.DeleteUser(userExists);
        return "Ok";
    }
    
    public async Task<User> UpdateMyAvatar(IFormFile file)
    {
        var user = await this.userRepository.GetUser(this.authService.GetAuthorizedUserId().GetValueOrDefault());
        user.Avatar = await this.storageService.Upload(file);
        return this.mapper.Map<UserEntity, User>(await this.userRepository.UpdateUser(user));
    }
}