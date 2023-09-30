using Gym.Desktop.Entities.Clients;
using Gym.Desktop.Interfaces.Clients;
using Gym.Desktop.Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Desktop.Repositories.Clients;

public class ClientRepository : BaseRepository, IClientRepository
{
    public async Task<int> CreateAsync(Client obj)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.clients (" +
                            "fname, lname, dob, is_male, email, phone_number, password_serial_number, image_path, description, created_at, updated_at) " +
                            "VALUES (@fname, @lname, @dob, @is_male, @email, @phone_number, @password_serial_number, @image_path, @description, @created_at, @updated_at);";

            await using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("fname", obj.FirstName);
                command.Parameters.AddWithValue("lname", obj.LastName);
                command.Parameters.AddWithValue("dob", obj.Date_of_birth);
                command.Parameters.AddWithValue("is_male", obj.IsMale);
                command.Parameters.AddWithValue("email", obj.Email);
                command.Parameters.AddWithValue("phone_number", obj.PhoneNumber);
                command.Parameters.AddWithValue("password_serial_number", obj.PassportSerialNumber);
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
            string query = $"DELETE FROM clients WHERE id = {id};";
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

    public async Task<IList<Client>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            var list = new List<Client>();
            string query = $"SELECT * FROM clients ORDER BY id " +
                $"OFFSET {(@params.PageNumber - 1) * @params.PageSize} " +
                $"LIMIT {@params.PageSize}";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Client client = new Client();
                        client.Id = reader.GetInt64(0);
                        client.FirstName = reader.GetString(1);
                        client.LastName = reader.GetString(2);
                        client.Date_of_birth = reader.GetFieldValue<DateOnly>(3);
                        client.IsMale = reader.GetBoolean(4);
                        client.Email = reader.GetString(5);
                        client.PhoneNumber = reader.GetString(6);
                        client.PassportSerialNumber = reader.GetString(7);
                        client.ImagePath = reader.GetString(8);
                        client.Description = reader.GetString(9);
                        client.CreatedAt = reader.GetDateTime(10);
                        client.UpdatedAt = reader.GetDateTime(11);
                        list.Add(client);
                    }
                }
            }
            return list;
        }
        catch
        {
            return new List<Client>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<List<Client>> GetAllClientsAsync()
    {
        try
        {
            await _connection.OpenAsync();
            var list = new List<Client>();
            string query = $"SELECT * FROM clients ORDER BY id;";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Client client = new Client();
                        client.Id = reader.GetInt64(0);
                        client.FirstName = reader.GetString(1);
                        client.LastName = reader.GetString(2);
                        client.Date_of_birth = reader.GetFieldValue<DateOnly>(3);
                        client.IsMale = reader.GetBoolean(4);
                        client.Email = reader.GetString(5);
                        client.PhoneNumber = reader.GetString(6);
                        client.PassportSerialNumber = reader.GetString(7);
                        client.ImagePath = reader.GetString(8);
                        client.Description = reader.GetString(9);
                        client.CreatedAt = reader.GetDateTime(10);
                        client.UpdatedAt = reader.GetDateTime(11);
                        list.Add(client);
                    }
                }
            }
            return list;
        }
        catch
        {
            return new List<Client>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Client editedObj)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE clients SET fname = @fname, " +
                $" lname = @lname, dob = @dob, is_male = @is_male, email = @email, " +
                $" phone_number = @phone_number, password_serial_number = @password_serial_number, image_path = @image_path, description = @description, " +
                $" created_at = @created_at, updated_at = @updated_at WHERE id = {id};";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("fname", editedObj.FirstName);
                command.Parameters.AddWithValue("lname", editedObj.LastName);
                command.Parameters.AddWithValue("dob", editedObj.Date_of_birth);
                command.Parameters.AddWithValue("is_male", editedObj.IsMale);
                command.Parameters.AddWithValue("email", editedObj.Email);
                command.Parameters.AddWithValue("phone_number", editedObj.PhoneNumber);
                command.Parameters.AddWithValue("password_serial_number", editedObj.PassportSerialNumber);
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

    public Task<Client> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }
}