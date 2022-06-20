using System;
using System.ComponentModel.DataAnnotations;

namespace Revix.Core.Domain.Entities;

public class Platform
{
    [Key]
    public Guid PlatformId { get; set; }
    public long Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Slug { get; set; }
}