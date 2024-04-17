using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.ValueObjects;

namespace Domain.Entities;

[Table("Customers")]
public partial class Customer : AuditableEntity
{
    public CustomerId Id { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}