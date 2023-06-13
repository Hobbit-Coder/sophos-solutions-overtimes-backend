using AutoMapper;
using MediatR;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Application.Areas.Commands;

public class CreateAreaCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

internal class CreateAreaCommandHandler : IRequestHandler<CreateAreaCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateAreaCommand area, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Area>(area);
        entity.Id = Guid.NewGuid();

        _unitOfWork.AreaRepository.Add(entity);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return entity.Id;
    }
}
