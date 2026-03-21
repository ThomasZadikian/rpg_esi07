using MediatR;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Queries; 

public class GetAllCharactersByLevelHandler : IRequestHandler<GetCharactersByLevelQuery, GetCharactersByLevelResponse>
{
    private readonly IPlayerProfileRepository _repository; 

    public GetAllCharactersByLevelHandler(IPlayerProfileRepository repository)
    {
        _repository = repository; 
    }

    public async Task<GetCharactersByLevelResponse> Handle(GetCharactersByLevelQuery request, CancellationToken cancellationToken)
    {
        var character = await _repository.GetByLevelAsync(request.Level);

        return new GetCharactersByLevelResponse(character); 
    }
}