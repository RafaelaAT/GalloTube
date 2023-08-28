using System.Data;
using GalloTube.Interfaces;
using GalloTube.Models;
using MySql.Data.MySqlClient;

namespace GalloTube.Repositories;

public class VideoTagRepository : IVideoTagRepository
{
    readonly string connectionString = "server=localhost;port=3306;database=GalloTubedb;uid=root;pwd=''";

    public void Create(int VideoId, byte TagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "insert into VideoTag(VideoId, TagId) values (@VideoId, @TagId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        command.Parameters.AddWithValue("@TagId", TagId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int VideoId, byte TagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from VideoTag where VideoId = @VideoId and TagId = @TagId";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        command.Parameters.AddWithValue("@TagId", TagId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int VideoId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from VideoTag where VideoId = @VideoId";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<Tag> ReadTagsByVideo(int VideoId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from tag where id in "
                   + "(select TagId from VideoTag where VideoId = @VideoId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        
        List<Tag> tags = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Tag tag = new()
            {
                Id = reader.GetByte("id"),
                Name = reader.GetString("name")
            };
            tags.Add(tag);
        }
        connection.Close();
        return tags;
    }

    public List<VideoTag> ReadMovieGenre()
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from VideoTag";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        
        List<VideoTag> videoTags = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            VideoTag videoTag = new()
            {
                VideoId = reader.GetInt32("VideoId"),
                TagId = reader.GetByte("TagId")
            };
            videoTags.Add(videoTag);
        }
        connection.Close();
        return videoTags;
    }

    public List<Video> ReadMoviesByGenre(byte TagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from video where id in "
                   + "(select VideoId from videotag where TagId = @TagId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@TagId", TagId);
        
        List<Video> videos = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Video video = new()
            {
                Id = reader.GetInt32("id"),
                Name = reader.GetString("name"),
                Description = reader.GetString("description"),
                UploadDate = reader.GetDateTime("uploaddate"),
                Duration = reader.GetInt16("duration"),
                Thumbnail = reader.GetString("thumbnail"),
                VideoFile = reader.GetString("videofile")
            };
            videos.Add(video);
        }
        connection.Close();
        return videos;
    }

    void IVideoTagRepository.Create(int VideoId, byte TagId)
    {
        throw new NotImplementedException();
    }

    void IVideoTagRepository.Delete(int VideoId, byte TagId)
    {
        throw new NotImplementedException();
    }

    void IVideoTagRepository.Delete(int VideoId)
    {
        throw new NotImplementedException();
    }

    List<VideoTag> IVideoTagRepository.ReadVideoTag()
    {
        throw new NotImplementedException();
    }

    List<Video> IVideoTagRepository.ReadVideosByTag(byte TagId)
    {
        throw new NotImplementedException();
    }

    List<Tag> IVideoTagRepository.ReadTagsByVideo(int VideoId)
    {
        throw new NotImplementedException();
    }
}
