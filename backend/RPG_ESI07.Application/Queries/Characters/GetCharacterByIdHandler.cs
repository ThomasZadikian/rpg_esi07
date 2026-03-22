using MediatR;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Queries.Characters; 

public class GetCharacterByIdHandler: IRequestHandler<GetCharacterByIdQuery, GetCharacterByIdResponse>
{
    private readonly IPlayerProfileRepository _repository; 

    public GetCharacterByIdHandler(IPlayerProfileRepository repository)
    {
        _repository = repository; 
    }

    public async Task<GetCharacterByIdResponse> Handle(GetCharacterByIdQuery request, CancellationToken calcellationToken)
    {
        var character = await _repository.GetByIdAsync(request.Id);

        return new GetCharacterByIdResponse(character); 
    }
}