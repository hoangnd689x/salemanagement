//using Core.Entities.HCEpg;
using Core.Entities.BatTrangModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.BatTrang
{
    public interface IProductRepository : IBatTrangRepository<Product>
    {
        //Task<List<VideoGenre>> GetGenreVideos(List<int> videoids);
        //Task<List<Genre>> GetGenres(int projectID);
        //Task<List<Genre>> GetGenresByVideo(int videoid, int projectID);
        //Task<List<Genre>> GetGenres(List<int> genreIDs, int projectID);
        //Task<Genre> GetGenreByID(int genreID, int projectID);
    }


}
