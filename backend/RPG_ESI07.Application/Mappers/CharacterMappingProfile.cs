using AutoMapper;
using RPG_ESI07.Application.Commands.Characters;
using RPG_ESI07.Application.Queries.Characters;
using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Application.Mappers;

public class CharacterMappingProfile : Profile
{
    public CharacterMappingProfile()
    {
        CreateMap<CreateCharacterCommand, CharacterMappingProfile>();
        CreateMap<UpdateCharactersCommand, PlayerProfile>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CharacterMappingProfile, GetCharacterByIdResponse>();
        CreateMap<CharacterMappingProfile, GetAllCharactersResponse>();
    }
}
