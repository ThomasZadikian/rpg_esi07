using AutoMapper;
using RPG_ESI07.Application.Commands.Enemies;
using RPG_ESI07.Application.Queries.Enemies;
using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Application.Mappers; 

public class EnemyMappingProfile : Profile
{
    public EnemyMappingProfile()
    {
        CreateMap<CreateEnemyCommand, EnemyMappingProfile>();
        CreateMap<UpdateEnemyCommand, Enemy>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); 

        CreateMap<EnemyMappingProfile, GetEnemyByIdResponse>(); 
        CreateMap<EnemyMappingProfile, GetAllEnemiesResponse>();
    }
}
