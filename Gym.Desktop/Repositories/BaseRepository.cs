using Gym.Desktop.Constants;
using Npgsql;

namespace Gym.Desktop.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;

    public BaseRepository()
    {
        _connection = new Npgsql.NpgsqlConnection(DbConstants.DB_CONNECTION_STRING);
    }
}
