using MediatR;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Commands.Skills;

public class CreateSkillHandler : IRequestHandler<CreateSkillCommand, CreateSkillResponse>
{
    private readonly ISkillRepository _repository;

    public CreateSkillHandler(ISkillRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateSkillResponse> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = new Skill();
        await _repository.AddAsync(entity);
        return new CreateSkillResponse(entity.Id, "Skill created successfully");
    }
}
