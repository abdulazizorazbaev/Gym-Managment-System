using Gym.Desktop.Entities.Clients;
using Gym.Desktop.Entities.Memberships;
using Gym.Desktop.Interfaces.Memberships;
using Gym.Desktop.Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Desktop.Repositories.Memberships;

class MembershipRepository : BaseRepository, IMembershipRepository
{
    public async Task<int> CreateAsync(Membership obj)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.memberships(client_id, package_id, instructor_id, status, start_date, end_date, payment_date, is_paid, description, created_at, updated_at) " +
                "VALUES (@client_id, @package_id, @instructor_id, @status, @start_date, @end_date, @payment_date, @is_paid, @description, @created_at, @updated_at);";

            await using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("client_id", obj.ClientId);
                command.Parameters.AddWithValue("package_id", obj.PackageId);
                command.Parameters.AddWithValue("instructor_id", obj.InstructorId);
                command.Parameters.AddWithValue("status", obj.MembershipStatus.ToString());
                command.Parameters.AddWithValue("start_date", obj.StartDate);
                command.Parameters.AddWithValue("end_date", obj.EndDate);
                command.Parameters.AddWithValue("payment_date", obj.PaymentDate);
                command.Parameters.AddWithValue("is_paid", obj.IsPaid);
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
            string query = $"DELETE FROM memberships WHERE id = {id};";
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

    public async Task<IList<Membership>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            var list = new List<Membership>();
            string query = $"SELECT * FROM memberships ORDER BY id " +
                $"OFFSET {(@params.PageNumber - 1) * @params.PageSize} " +
                $"LIMIT {@params.PageSize}";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Membership membership = new Membership();
                        membership.Id = reader.GetInt64(0);
                        membership.ClientId = reader.GetInt64(1);
                        membership.PackageId = reader.GetInt64(2);
                        membership.InstructorId = reader.GetInt64(3);
                        membership.MembershipStatus = reader.GetString(4);
                        membership.StartDate = reader.GetFieldValue<DateOnly>(5);
                        membership.EndDate = reader.GetFieldValue<DateOnly>(6);
                        membership.PaymentDate = reader.GetFieldValue<DateTime>(7);
                        membership.IsPaid = reader.GetBoolean(8);
                        membership.Description = reader.GetString(9);
                        membership.CreatedAt = reader.GetDateTime(10);
                        membership.UpdatedAt = reader.GetDateTime(11);
                        list.Add(membership);
                    }
                }
            }
            return list;
        }
        catch
        {
            return new List<Membership>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
    
    public async Task<int> UpdateAsync(long id, Membership editedObj)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE memberships SET client_id = @client_id, " +
                $" package_id = @package_id, instructor_id = @instructor_id, status = @status, start_date = @start_date, " +
                $" end_date = @end_date, payment_date = @payment_date, is_paid = @is_paid, description = @description, " +
                $" created_at = @created_at, updated_at = @updated_at WHERE id = {id};";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("client_id", editedObj.ClientId);
                command.Parameters.AddWithValue("package_id", editedObj.PackageId);
                command.Parameters.AddWithValue("instructor_id", editedObj.InstructorId);
                command.Parameters.AddWithValue("status", editedObj.MembershipStatus);
                command.Parameters.AddWithValue("start_date", editedObj.StartDate);
                command.Parameters.AddWithValue("end_date", editedObj.EndDate);
                command.Parameters.AddWithValue("payment_date", editedObj.PaymentDate);
                command.Parameters.AddWithValue("is_paid", editedObj.IsPaid);
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
            var list = new List<Membership>();
            string query = $"SELECT * FROM memberships ORDER BY id;";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Membership membership = new Membership();
                        membership.Id = reader.GetInt64(0);
                        membership.ClientId = reader.GetInt64(1);
                        membership.PackageId = reader.GetInt64(2);
                        membership.InstructorId = reader.GetInt64(3);
                        membership.MembershipStatus = reader.GetString(4);
                        membership.StartDate = reader.GetFieldValue<DateOnly>(5);
                        membership.EndDate = reader.GetFieldValue<DateOnly>(6);
                        membership.PaymentDate = reader.GetFieldValue<DateTime>(7);
                        membership.IsPaid = reader.GetBoolean(8);
                        membership.Description = reader.GetString(9);
                        membership.CreatedAt = reader.GetDateTime(10);
                        membership.UpdatedAt = reader.GetDateTime(11);
                        list.Add(membership);
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

    public async Task<int> CountActiveAsync()
    {
        try
        {
            await _connection.OpenAsync();
            var list = new List<Membership>();
            string query = $"SELECT * FROM memberships ORDER BY id;";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Membership membership = new Membership();
                        membership.Id = reader.GetInt64(0);
                        membership.ClientId = reader.GetInt64(1);
                        membership.PackageId = reader.GetInt64(2);
                        membership.InstructorId = reader.GetInt64(3);
                        membership.MembershipStatus = reader.GetString(4);
                        membership.StartDate = reader.GetFieldValue<DateOnly>(5);
                        membership.EndDate = reader.GetFieldValue<DateOnly>(6);
                        membership.PaymentDate = reader.GetFieldValue<DateTime>(7);
                        membership.IsPaid = reader.GetBoolean(8);
                        membership.Description = reader.GetString(9);
                        membership.CreatedAt = reader.GetDateTime(10);
                        membership.UpdatedAt = reader.GetDateTime(11);
                        if (membership.MembershipStatus == "Expired")
                            list.Add(membership);
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

    public async Task<List<Membership>> CountRevenueAsync()
    {
        try
        {
            await _connection.OpenAsync();
            var list = new List<Membership>();
            string query = $"SELECT * FROM memberships ORDER BY id;";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Membership membership = new Membership();
                        membership.Id = reader.GetInt64(0);
                        membership.ClientId = reader.GetInt64(1);
                        membership.PackageId = reader.GetInt64(2);
                        membership.InstructorId = reader.GetInt64(3);
                        membership.MembershipStatus = reader.GetString(4);
                        membership.StartDate = reader.GetFieldValue<DateOnly>(5);
                        membership.EndDate = reader.GetFieldValue<DateOnly>(6);
                        membership.PaymentDate = reader.GetFieldValue<DateTime>(7);
                        membership.IsPaid = reader.GetBoolean(8);
                        membership.Description = reader.GetString(9);
                        membership.CreatedAt = reader.GetDateTime(10);
                        membership.UpdatedAt = reader.GetDateTime(11);
                        if (membership.IsPaid == true)
                            list.Add(membership);
                    }
                }
            }
            return list;
        }
        catch
        {
            return new List<Membership>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<Membership> GetAsync(long id)
    {
        throw new System.NotImplementedException();
    }
}