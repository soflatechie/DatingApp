using System;
using System.ComponentModel.DataAnnotations;
using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class AppUser : IdentityUser<int>
{
   
    public DateOnly DateOfBirth { get; set; }
    [Required]
    public string KnownAs { get; set; } =  string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    [Required]
    public string Gender { get; set; } =  string.Empty;
    public string? Introduction { get; set; }
    public string? Interests { get; set; }
    public string? LookingFor { get; set; }
    [Required]
    public string? City { get; set; }

    public string? Country { get; set; }
    public List<Photo> Photos { get; set; } = [];

    public List<UserLike> LikedByUsers { get; set; } = [];
    public List<UserLike> LikedUsers { get; set; } = [];    

    public ICollection<AppUserRole> UserRoles { get; set; } = [];
   
}

