using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using Repository.Data;

namespace Repository.MovieRepository;

public class MovieRepository : BaseRepository<Movie>, IMovieRepository
{
    public MovieRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public IEnumerable<Movie?> GetMovies(
        bool sortedByTitle = false,
        bool sortedByRating = false,
        string? title = null,
        string? actor = null,
        string? director = null,
        string? genre = null,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken cancellationToken = default
        )
    {
        var movies = _entities.AsQueryable();

        if (title != null)
        {
            movies = movies.Where(m => m.Title.Contains(title));
        }

        if (actor != null)
        {
            movies = movies.Include(m => m.Cast).ThenInclude(p => p.Participant).Where(m => m.Cast.Any(p => p.Participant.Name.Contains(actor)));
        }

        if (director != null)
        {
            movies = movies.Include(m => m.Direction).ThenInclude(p => p.Participant).Where(m => m.Direction.Any(p => p.Participant.Name.Contains(director)));
        }

        if (genre != null)
        {
            movies = movies.Where(m => m.Genres.Any(g => g.Title.Contains(genre)));
        }

        if (pageNumber != null && pageSize != null)
        {
            movies = movies.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        if (sortedByTitle)
        {
            movies = movies.OrderBy(m => m.Title);
        }

        if (sortedByRating)
        {
            movies = movies.Include(m => m.Votes).ThenInclude(v => v.User).OrderByDescending(m => m.Votes.Average(v => v.Rating));
        }

        return movies.AsEnumerable();
    }

    public override Task<Movie?> Get(int id, CancellationToken cancellationToken)
    {
        return _entities
            .Include(m => m.Cast)
            .ThenInclude(c => c.Participant)
            .Include(m => m.Genres)
            .Include(m => m.Direction)
            .ThenInclude(d => d.Participant)
            .Include(m => m.Votes)
            .ThenInclude(v => v.User)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

}