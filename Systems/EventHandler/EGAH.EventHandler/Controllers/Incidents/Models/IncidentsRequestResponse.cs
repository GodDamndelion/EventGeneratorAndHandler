namespace EGAH.EventHandler.Controllers.Models;

using AutoMapper;
using EGAH.Context.Entities;
using EGAH.Services.Incidents;
using FluentValidation;

public class IncidentRequestResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public IncidentTypeEnum Type { get; set; }

    public DateTime Time { get; set; } // Дата создания инцидента

    public Guid FirstEventId { get; set; }

    public Guid? SecondEventId { get; set; }
}

public class IncidentRequestResponseValidator : AbstractValidator<IncidentRequestResponse>
{
    public IncidentRequestResponseValidator() { }
}

public class IncidentRequestResponseProfile : Profile
{
    public IncidentRequestResponseProfile()
    {
        CreateMap<IncidentRequestResponse, IncidentModel>();
        CreateMap<IncidentModel, IncidentRequestResponse>();
    }
}
