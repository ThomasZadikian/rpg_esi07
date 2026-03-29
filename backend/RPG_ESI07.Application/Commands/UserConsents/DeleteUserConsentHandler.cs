using MediatR;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Commands.UserConsents;

public class DeleteUserConsentHandler : IRequestHandler<DeleteUserConsentCommand, DeleteUserConsentResponse>
{
    private readonly IUserConsentRepository _repository;

    public DeleteUserConsentHandler(IUserConsentRepository repository) => _repository = repository;

    public async Task<DeleteUserConsentResponse> Handle(DeleteUserConsentCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        return new DeleteUserConsentResponse(true, "UserConsent deleted successfully");
    }
}
