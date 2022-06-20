using System;
using System.ComponentModel.DataAnnotations;

namespace Revix.Core.Domain.Entities;

public class Tag
{
    [Key]
    public Guid TagId { get; set; }
    public string Name { get; set; }
}