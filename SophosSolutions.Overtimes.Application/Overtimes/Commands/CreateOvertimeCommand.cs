using AutoMapper;
using MediatR;
using SophosSolutions.Overtimes.Application.Common.Exceptions;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Services;
using SophosSolutions.Overtimes.Models.Entities;
using SophosSolutions.Overtimes.Models.Enums;

namespace SophosSolutions.Overtimes.Application.Overtimes.Commands;

public class CreateOvertimeCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public int Hours { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
}

internal class CreateOvertimeCommandHandle : IRequestHandler<CreateOvertimeCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateOvertimeCommandHandle(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateOvertimeCommand request, CancellationToken cancellationToken)
    {
        var OvertimesInMoth = await _unitOfWork.OvertimeRepository.FindAsync(overtime =>
                                        overtime.Date.Month == request.Date.Month &&
                                        overtime.UserId == _currentUserService.UserId
                                    );
        var hours = OvertimesInMoth.Sum(over => over.Hours);

        if (hours >= 40)
        {
            throw new DomainEventException("Ya exediste el numero limite de horas extras!");
        }

        var overtime = _mapper.Map<Overtime>(request);

        _unitOfWork.OvertimeRepository.Add(overtime);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return overtime.Id;
    }
}
