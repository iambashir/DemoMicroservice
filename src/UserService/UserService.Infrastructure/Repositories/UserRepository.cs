using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistence;

namespace UserService.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public Task<AppUser?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        return _context.Users.FirstOrDefaultAsync(x => x.UserName == userName.Trim(), cancellationToken);
    }

    public Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        return _context.Users.AnyAsync(x => x.UserName == userName.Trim(), cancellationToken);
    }

    public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var normalized = email.Trim().ToLowerInvariant();
        return _context.Users.AnyAsync(x => x.Email == normalized, cancellationToken);
    }

    public Task AddAsync(AppUser user, CancellationToken cancellationToken)
    {
        return _context.Users.AddAsync(user, cancellationToken).AsTask();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
