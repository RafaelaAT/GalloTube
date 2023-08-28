using GalloTube.Models;

namespace GalloTube.Interfaces;

public interface IMovieRepository : IRepository<Video>
{
    List<Video> ReadAllDetailed();

    Video ReadByIdDetailed(int id);
}
