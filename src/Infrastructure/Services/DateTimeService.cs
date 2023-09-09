using ASE3040.Application.Common.Interfaces;

namespace ASE3040.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}