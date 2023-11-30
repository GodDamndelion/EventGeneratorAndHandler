namespace EGAH.Services.Incidents;

using AutoMapper;
using EGAH.Context.Entities;
using FluentValidation;

public class IncidentModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public IncidentTypeEnum Type { get; set; }

    public DateTime Time { get; set; } // Дата создания инцидента

    public Guid FirstEventId { get; set; }

    public Guid? SecondEventId { get; set; }
}

public class IncidentModelValidator : AbstractValidator<IncidentModel>
{
    public IncidentModelValidator() { }
}

public class IncidentModelProfile : Profile
{
    public IncidentModelProfile()
    {
        CreateMap<IncidentModel, Incident>();
        CreateMap<Incident, IncidentModel>();
    }
}
