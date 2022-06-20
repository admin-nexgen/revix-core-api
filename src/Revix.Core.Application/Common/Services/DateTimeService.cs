using System;
using Revix.Core.Application.Common.Interfaces;

namespace Revix.Core.Application.Common.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}