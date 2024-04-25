using Domain.Entities.ValueObjects;

namespace Domain.Entities;

public class Category : AuditableEntity
{
    public CategoryId Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
}