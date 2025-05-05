using System;
using API.Dtos;
using API.Entities;
using API.helpers;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(AppUser appUser);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser>> GetUsersAsync();

    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?> GetUserByUserNameAsync(string username);

    Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParms);
    Task<MemberDto?> GetMemberAsync(string username);
}
