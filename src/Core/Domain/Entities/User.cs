﻿using Domain.Entities.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public partial class User : AuditableEntity
{
	public UserId Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public string Salt { get; set; }
	public int Iterations { get; set; }
	public int GenderId { get; set; }
	public string Address { get; set; }
	public int RoleId { get; set; }
	public int? UserStatusId { get; set; }
	public bool? IsDeleted { get; set; }
	public DateTime? DateDeleted { get; set; }
	public DateTime? DateLastLoggedIn { get; set; }
	public DateTime DateOfBirth { get; set; }
}

public partial class User
{
	public Gender Gender { get; set; }
	public Role Role { get; set; }
}
