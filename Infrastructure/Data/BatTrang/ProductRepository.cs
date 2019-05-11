using Core.Entities.BatTrangModel;
using Core.Entities;
using Core.Interfaces.BatTrang;
using Infrastructure.Data.BatTrang;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.BatTrang
{
    public class ProductRepository : BatTrangRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }


        //public async Task<List<Genre>> GetGenresByVideo(int videoid, int projectID)
        //{

        //    var genreids = _dbContext.VideoGenres.Where(m => m.VideoId == videoid).Select(m => m.GenreId).Distinct();
        //    var query = _dbContext.Genres.Where(g => genreids.Contains(g.Id));
        //    return await FilterByProject(query, projectID);
        //}

        //public async Task<List<VideoGenre>> GetGenreVideos(List<int> videoids)
        //{
        //    return await _dbContext.VideoGenres.Where(m => videoids.Contains(m.VideoId)).ToListAsync();
        //}

        //public async Task<List<Genre>> GetGenres(int projectID)
        //{
        //    var query = _dbContext.Genres.AsQueryable();
        //    return await FilterByProject(query, projectID);
        //}

        //public async Task<List<Genre>> GetGenres(List<int> genreIDs, int projectID)
        //{
        //    if (genreIDs.Count > 0)
        //    {
        //        var query = _dbContext.Genres.Where(g => genreIDs.Contains(g.Id));
        //        return await FilterByProject(query, projectID);
        //        //string stringIDs = "(";
        //        //int count = genreIDs.Count;
        //        //foreach(var genre in genreIDs)
        //        //{
        //        //    stringIDs += genre.ToString();
        //        //    count--;
        //        //    if (count > 0)
        //        //    {
        //        //        stringIDs += ",";
        //        //    }
        //        //    else
        //        //        stringIDs += ")";
        //        //}

        //        //var prj = string.Format("|{0}|", projectID);
        //        //var sql = string.Format("SELECT * FROM [ITV_Video].[dbo].[Genres] WHERE [Published] = {0} AND Projects LIKE %{1}% AND ID in {2}", (int) DataCommon.Enums.Published.Show, prj, stringIDs);
        //        //return context.Database.SqlQuery<Genre>(sql).ToList();
        //    }

        //    return null;
        //}

        //public async Task<Genre> GetGenreByID(int genreID, int projectID)
        //{
        //    var genre = _dbContext.Genres.Where(g => g.Id == genreID);
        //    return (await FilterByProject(genre, projectID)).FirstOrDefault();
        //}
        //private async Task<List<Genre>> FilterByProject(IQueryable<Genre> query, int projectID)
        //{
        //    var prj = string.Format("|{0}|", projectID);
        //    return await query.Where(g => g.Published == Published.Show && !string.IsNullOrEmpty(g.Projects) && g.Projects.Contains(prj)).ToListAsync();
        //}

    }
}
