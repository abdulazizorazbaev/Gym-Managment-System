using Gym.Desktop.Entities.Packages;
using Gym.Desktop.Interfaces.Packages;
using Gym.Desktop.Utilities;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Desktop.Repositories.Packages;

public class PackageRepository : BaseRepository, IPackageRepository
{
    public async Task<int> CreateAsync(Package obj)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.packages (" +
                "package_name, duration, price, days, image_path, description, created_at, updated_at) " +
                "VALUES (@package_name, @duration, @price, @days, @image_path, @description, @created_at, @updated_at);";

            await using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("package_name", obj.PackageName);
                command.Parameters.AddWithValue("duration", obj.Duration);
                command.Parameters.AddWithValue("price", obj.Price);
                command.Parameters.AddWithValue("days", obj.Days);
                command.Parameters.AddWithValue("image_path", obj.ImagePath);
                command.Parameters.AddWithValue("description", obj.Description);
                command.Parameters.AddWithValue("created_at", obj.CreatedAt);
                command.Parameters.AddWithValue("updated_at", obj.UpdatedAt);

                var dbResult = await command.ExecuteNonQueryAsync();
                return dbResult;
            }
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"DELETE FROM packages WHERE id = {id};";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                var dbResult = await command.ExecuteNonQueryAsync();
                return dbResult;
            }
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<Package>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            var list = new List<Package>();
            string query = $"SELECT * FROM packages ORDER BY id " +
                $"OFFSET {(@params.PageNumber - 1) * @params.PageSize} " +
                $"LIMIT {@params.PageSize}";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var package = new Package();
                        package.Id = reader.GetInt64(0);
                        package.PackageName = reader.GetString(1);
                        package.Duration = reader.GetString(2);
                        package.Price = reader.GetFloat(3);
                        package.Days = reader.GetInt64(4);
                        package.ImagePath = reader.GetString(5);
                        package.Description = reader.GetString(6);
                        package.CreatedAt = reader.GetDateTime(7);
                        package.UpdatedAt = reader.GetDateTime(8);
                        list.Add(package);
                    }
                }
            }
            return list;
        }
        catch
        {
            return new List<Package>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<List<Package>> GetAllPackagesAsync()
    {
        try
        {
            await _connection.OpenAsync();
            var list = new List<Package>();
            string query = $"SELECT * FROM packages ORDER BY id;";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var package = new Package();
                        package.Id = reader.GetInt64(0);
                        package.PackageName = reader.GetString(1);
                        package.Duration = reader.GetString(2);
                        package.Price = reader.GetFloat(3);
                        package.Days = reader.GetInt64(4);
                        package.ImagePath = reader.GetString(5);
                        package.Description = reader.GetString(6);
                        package.CreatedAt = reader.GetDateTime(7);
                        package.UpdatedAt = reader.GetDateTime(8);
                        list.Add(package);
                    }
                }
            }
            return list;
        }
        catch
        {
            return new List<Package>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Package editedObj)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE packages SET package_name = @package_name, " +
                $" duration = @duration, price = @price, days = @days, image_path = @image_path, description = @description, " +
                $" created_at = @created_at, updated_at = @updated_at WHERE id = {id};";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("package_name", editedObj.PackageName);
                command.Parameters.AddWithValue("duration", editedObj.Duration);
                command.Parameters.AddWithValue("price", editedObj.Price);
                command.Parameters.AddWithValue("days", editedObj.Days);
                command.Parameters.AddWithValue("image_path", editedObj.ImagePath);
                command.Parameters.AddWithValue("description", editedObj.Description);
                command.Parameters.AddWithValue("created_at", editedObj.CreatedAt);
                command.Parameters.AddWithValue("updated_at", editedObj.UpdatedAt);

                return await command.ExecuteNonQueryAsync();
            }
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
    
    public async Task<int> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            var list = new List<Package>();
            string query = $"SELECT * FROM packages ORDER BY id;";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var package = new Package();
                        package.Id = reader.GetInt64(0);
                        package.PackageName = reader.GetString(1);
                        package.Duration = reader.GetString(2);
                        package.Price = reader.GetFloat(3);
                        package.Days = reader.GetInt64(4);
                        package.ImagePath = reader.GetString(5);
                        package.Description = reader.GetString(6);
                        package.CreatedAt = reader.GetDateTime(7);
                        package.UpdatedAt = reader.GetDateTime(8);
                        list.Add(package);
                    }
                }
            }
            return list.Count;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
    
    public Task<Package> GetAsync(long id)
    {
        throw new System.NotImplementedException();
    }
}