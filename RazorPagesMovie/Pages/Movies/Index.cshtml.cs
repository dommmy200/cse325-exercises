using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }
        [BindProperty(SupportsGet = true)]
        [Range(1900, 2100, ErrorMessage = "Please enter a valid year")]
        public int? SearchYear { get; set; }

        [BindProperty(SupportsGet = true)]
        [Range(1900, 2100, ErrorMessage = "Start year must be between 1900 and 2100")]
        public int? YearFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        [Range(1900, 2100, ErrorMessage = "End year must be between 1900 and 2100")]
        public int? YearTo { get; set; }

        public SelectList? Years { get; set; }

        public async Task OnGetAsync()
        {
            // <snippet_search_linqQuery>
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;
            IQueryable<int> yearQuery = from m in _context.Movie
                                        orderby m.ReleaseDate.Year
                                        select m.ReleaseDate.Year;
            var movies = from m in _context.Movie
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title != null && s.Title.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }
            if (SearchYear.HasValue)
            {
                movies = movies.Where(m => m.ReleaseDate.Year == SearchYear.Value);
            }
            // Year range filter
            if (YearFrom.HasValue)
            {
                movies = movies.Where(m => m.ReleaseDate.Year >= YearFrom.Value);
            }

            if (YearTo.HasValue)
            {
                movies = movies.Where(m => m.ReleaseDate.Year <= YearTo.Value);
            }
            // <snippet_search_selectList>
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            // </snippet_search_selectList>
            Movie = await movies.ToListAsync();
        }
    }
}
