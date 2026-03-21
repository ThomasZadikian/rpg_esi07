using MediatR;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Queries; 

public class GetCharactersByClassHandler : IRequestHandler<GetCharactersByClassQuery, GetCharactersByClassResponse>
{
    private readonly IPlayerProfileRepository _repository; 

    public GetCharactersByClassHandler(IPlayerProfileRepository repository)
    {
        _repository = repository; 
    }

    public async Task<GetCharactersByClassResponse> Handle(GetCharactersByClassQuery request, CancellationToken cancellationToken)
    {
        var characters = await _repository.GetByClassAsync(request.Class);

        return new GetCharactersByClassResponse(characters); 
    }
}
