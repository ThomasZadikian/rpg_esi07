using MediatR;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Queries; 

public class GetAllCharactersHandler: IRequestHandler<GetAllCharactersQuery, GetAllCharactersResponse>
{
    private readonly IPlayerProfileRepository _repository; 

    public GetAllCharactersHandler(IPlayerProfileRepository repository)
    {
        _repository = repository; 
    }

    public async Task<GetAllCharactersResponse> Handle(GetAllCharactersQuery request, CancellationToken cancellationToken)
    {
        var characters = await _repository.GetAllAsync();

        return new GetAllCharactersResponse(characters); 
    }
}