using AutoMapper;
using RPG_ESI07.Application.Commands;
using RPG_ESI07.Application.Queries;

namespace RPG_ESI07.Application.Mappers; 

public class EnemyMappingProfile : Profile
{
    public EnemyMappingProfile()
    {
        CreateMap<CreateEnemyCommand, EnemyMappingProfile>();

        CreateMap<EnemyMappingProfile, GetEnemyByIdResponse>(); 
        CreateMap<EnemyMappingProfile, GetAllEnemiesResponse>();
    }
}
