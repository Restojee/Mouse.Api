using AutoMapper;
using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.Tags.Data;
using Mouse.NET.Tips.Data;
using Mouse.NET.Tips.Models;
using Mouse.Stick.Controllers.Auth;

namespace Mouse.NET.Tips.services;

public class TipService : ITipService
{

    private readonly IMapper mapper;
    private readonly IAuthService authService;
    private readonly ITipRepository tipRepository;

    public TipService(IMapper mapper, ITipRepository tipRepository, IAuthService authService)
    {
        this.tipRepository = tipRepository;
        this.authService = authService;
        this.mapper = mapper;
    }
    
    public async Task<PagedResult<Tip>> GetTipCollection(PaginateRequest request)
    {
        return mapper.Map<PagedResult<TipEntity>, PagedResult<Tip>>(await this.tipRepository.GetTipCollection(request));
    }

    public async Task<Tip> GetTip(int tipId)
    {
        return mapper.Map<TipEntity, Tip>(await this.tipRepository.GetTip(tipId));
    }

    public async Task<Tip> CreateTip(TipCreateRequest request)
    {
        return mapper.Map<TipEntity, Tip>(await this.tipRepository.CreateTip(new TipEntity
        {
            UserId = this.authService.GetAuthorizedUserId(),
            Title = request.Title,
            Text = request.Text,
        }));
    }

    public async Task<Tip> UpdateTip(TipUpdateRequest request)
    {
        var tipExists = await this.tipRepository.GetTip(request.Id);
        if (tipExists.User.Id != this.authService.GetAuthorizedUserId())
        {
            throw new BadHttpRequestException("Запрашиваемая информация не найдена");
        }
        return mapper.Map<TipEntity, Tip>(await this.tipRepository.UpdateTip(mapper.Map(request, tipExists)));
    }

    public async Task<string> DeleteTip(int tipId)
    {
        var tipExists = await this.tipRepository.GetTip(tipId);
        if (tipExists.User.Id != this.authService.GetAuthorizedUserId())
        {
            throw new BadHttpRequestException("Запрашиваемая информация не найдена");
        }
        await this.tipRepository.DeleteTip(tipExists);
        return "Ok";
    }
}