namespace GrpcService.Repositories;

public abstract class BaseRepository(AppDbContext context)
{
    protected AppDbContext _context = context;

    protected async Task<bool> IsSuccessfullSavedAsync()
    {
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}
