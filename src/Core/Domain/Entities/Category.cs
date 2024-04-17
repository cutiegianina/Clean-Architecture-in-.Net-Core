using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.ValueObjects;

namespace Domain.Entities;

[Table("Categories")]
public class Category : AuditableEntity
{
    public CategoryId Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
}