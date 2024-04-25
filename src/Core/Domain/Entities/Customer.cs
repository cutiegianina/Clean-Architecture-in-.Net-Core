using Domain.Entities.ValueObjects;

namespace Domain.Entities;

public partial class Customer : AuditableEntity
{
    public CustomerId Id { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}