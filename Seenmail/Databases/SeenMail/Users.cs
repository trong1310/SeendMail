using System;
using System.Collections.Generic;

namespace Seenmail.Databases.SeenMail;

public partial class Users
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? FullName { get; set; }

    public string? PassWord { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Status { get; set; }
}
