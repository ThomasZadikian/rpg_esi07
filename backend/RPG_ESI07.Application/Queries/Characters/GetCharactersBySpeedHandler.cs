using MediatR;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Queries.Characters; 

public class GetCharactersBySpeedHandler : IRequestHandler<GetCharactersBySpeedQuery, GetCharactersBySpeedResponse>
{
    private readonly IPlayerProfileRepository _repository; 

    public GetCharactersBySpeedHandler(IPlayerProfileRepository repository)
    {
        _repository = repository; 
    }

    public async Task<GetCharactersBySpeedResponse> Handle(GetCharactersBySpeedQuery request, CancellationToken cancellationToken)
    {
        var characters = await _repository.GetBySpeedAsync();

        return new GetCharactersBySpeedResponse(characters); 
    }
}
