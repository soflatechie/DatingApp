using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }

    [Required]
    public string UserName { get; set; } =  null!;

    [Required]
    public Byte[] PasswordHash { get; set; } = null!;
    
    [Required]
    public Byte[] PasswordSalt { get; set; } = null!;
}
