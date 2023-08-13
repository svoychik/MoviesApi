using FluentValidation;

namespace MoviesApi.Common;

public class MovieValidator : AbstractValidator<Movie>
{
    public MovieValidator()
    {
        RuleFor(movie => movie.Title).NotEmpty().MaximumLength(100);
        RuleFor(movie => movie.Description).NotEmpty();
        RuleFor(movie => movie.ReleaseDate).NotEmpty();
        RuleFor(movie => movie.Genre).NotEmpty().MaximumLength(50);
        RuleFor(movie => movie.Rating).InclusiveBetween(0, 10);
    }
}