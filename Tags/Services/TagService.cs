using AutoMapper;
using Mouse.NET.Data.Models;
using Mouse.NET.Tags.Data;
using Mouse.NET.Tags.Models;
using Mouse.Stick.Controllers.Auth;

namespace Mouse.NET.Tags.services;

public class TagService : ITagService
{
    
    private readonly IMapper mapper;
    private readonly IAuthService authService;
    private readonly ITagRepository tagRepository;

    public TagService(IMapper mapper, ITagRepository tagRepository, IAuthService authService)
    {
        this.tagRepository = tagRepository;
        this.authService = authService;
        this.mapper = mapper;
    }
    
    public async Task<ICollection<Tag>> GetTagCollection()
    {
        return mapper.Map<ICollection<TagEntity>, ICollection<Tag>>(await this.tagRepository.GetTagCollection());
    }

    public async Task<Tag> GetTag(int tagId)
    {
        return mapper.Map<TagEntity, Tag>(await this.tagRepository.GetTag(tagId));
    }

    public async Task<Tag> CreateTag(TagCreateRequest request)
    {
        var tagExists = await this.tagRepository.GetTagByName(request.Name);
        if (tagExists != null)
        {
            throw new BadHttpRequestException("Тег с таким именем уже существует");
        }

        var newTag = new TagEntity
        {
            Description = request.Description,
            Name = request.Name,
            UserId = this.authService.GetAuthorizedUserId()
        };
        return mapper.Map<TagEntity, Tag>(await this.tagRepository.CreateTag(newTag));
    }

    public async Task<Tag> UpdateTag(TagUpdateRequest request)
    {
        var tagExists = await this.tagRepository.GetTag(request.Id);
        if (tagExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемый тег не найден");
        }
        return this.mapper.Map<TagEntity, Tag>(await this.tagRepository.UpdateTag(this.mapper.Map(request, tagExists)));
    }

    public async Task<string> DeleteTag(int tagId)
    {
        var tagExists = await this.tagRepository.GetTag(tagId);
        if (tagExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемый тег не найден");
        }
        await this.tagRepository.DeleteTag(tagExists);
        return "Ok";
    }
}