using AutoMapper;
using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.Tips.Data;
using Mouse.NET.Tips.Models;

namespace Mouse.NET.Tips.services;

public class TipService : ITipService
{
    
    private readonly IMapper mapper;
    
    private ITipRepository tipRepository;

    public TipService(IMapper mapper, ITipRepository tipRepository) {
        this.tipRepository = tipRepository;
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
        return mapper.Map<TipEntity, Tip>(await this.tipRepository.CreateTip(mapper.Map<TipCreateRequest, TipEntity>(request)));
    }

    public async Task<Tip> UpdateTip(TipUpdateRequest request)
    {
        var tipExists = await this.tipRepository.GetTip(request.Id);
        if (tipExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая информация не найдена");
        }
        return mapper.Map<TipEntity, Tip>(await this.tipRepository.UpdateTip(mapper.Map(request, tipExists)));
    }

    public async Task<string> DeleteTip(int tipId)
    {
        var tipExists = await this.tipRepository.GetTip(tipId);
        if (tipExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая информация не найдена");
        }
        await this.tipRepository.DeleteTip(tipExists);
        return "Ok";
    }
}