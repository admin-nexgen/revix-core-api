using System;
using Revix.Core.Application.Common.Interfaces;

namespace Revix.Core.Application.Common.Models;

public class MachineDateTime : IDateTime
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;

    public int CurrentYear => DateTime.Now.Year;
}