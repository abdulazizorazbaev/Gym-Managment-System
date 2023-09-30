using Gym.Desktop.Entities.Instructors;
using Gym.Desktop.Interfaces;
using Gym.Desktop.Interfaces.Instructors;
using Gym.Desktop.Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Desktop.Repositories.Instructors;

public class InstructorRepository : BaseRepository, IInstructorRepository
{
    public async Task<int> CreateAsync(Instructor obj)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.instructors (" +
                "package_id, fname, lname, dob, is_male, salary, email, title, phone_number, image_path, description, created_at, updated_at) " +
                "VALUES (@package_id, @fname, @lname, @dob, @is_male, @salary, @email, @title, @phone_number, @image_path, @description, @created_at, @updated_at);";

            await using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("package_id", obj.PackageId);
                command.Parameters.AddWithValue("fname", obj.FirstName);
                command.Parameters.AddWithValue("lname", obj.LastName);
                command.Parameters.AddWithValue("dob", obj.Date_of_birth);
                command.Parameters.AddWithValue("is_male", obj.IsMale);
                command.Parameters.AddWithValue("salary", obj.Salary);
                command.Parameters.AddWithValue("email", obj.Email);
                command.Parameters.AddWithValue("title", obj.Title);
                command.Parameters.AddWithValue("phone_number", obj.PhoneNumber);
                command.Parameters.AddWithValue("image_path", obj.ImagePath);
                command.Parameters.AddWithValue("description", obj.Description);
                command.Parameters.AddWithValue("created_at", obj.CreatedAt);
                command.Parameters.AddWithValue("updated_at", obj.UpdatedAt);

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

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"DELETE FROM instructors WHERE id = {id};";
            await using(var command = new NpgsqlCommand(query, _connection))
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

    public async Task<IList<Instructor>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            var list = new List<Instructor>();
            string query = $"SELECT * FROM instructors ORDER BY id OFFSET {@params.SkipCount} LIMIT {@params.PageSize}";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var instructor = new Instructor();
                        instructor.Id = reader.GetInt64(0);
                        instructor.PackageId = reader.GetInt64(1);
                        instructor.FirstName = reader.GetString(2);
                        instructor.LastName = reader.GetString(3);
                        instructor.Date_of_birth = reader.GetFieldValue<DateOnly>(4);
                        instructor.IsMale = reader.GetBoolean(5);
                        instructor.Salary = reader.GetFloat(6);
                        instructor.Email = reader.GetString(7);
                        instructor.Title = reader.GetString(8);
                        instructor.PhoneNumber = reader.GetString(9);
                        instructor.ImagePath = reader.GetString(10);
                        instructor.Description = reader.GetString(11);
                        instructor.CreatedAt = reader.GetDateTime(12);
                        instructor.UpdatedAt = reader.GetDateTime(13);
                        list.Add(instructor);
                    }
                }
            }
            return list;
        }
        catch
        {
            return new List<Instructor>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<List<Instructor>> GetAllInstructorsAsync()
    {
        try
        {
            await _connection.OpenAsync();
            var list = new List<Instructor>();
            string query = $"SELECT * FROM instructors ORDER BY id;";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var instructor = new Instructor();
                        instructor.Id = reader.GetInt64(0);
                        instructor.PackageId = reader.GetInt64(1);
                        instructor.FirstName = reader.GetString(2);
                        instructor.LastName = reader.GetString(3);
                        instructor.Date_of_birth = reader.GetFieldValue<DateOnly>(4);
                        instructor.IsMale = reader.GetBoolean(5);
                        instructor.Salary = reader.GetFloat(6);
                        instructor.Email = reader.GetString(7);
                        instructor.Title = reader.GetString(8);
                        instructor.PhoneNumber = reader.GetString(9);
                        instructor.ImagePath = reader.GetString(10);
                        instructor.Description = reader.GetString(11);
                        instructor.CreatedAt = reader.GetDateTime(12);
                        instructor.UpdatedAt = reader.GetDateTime(13);
                        list.Add(instructor);
                    }
                }
            }
            return list;
        }
        catch
        {
            return new List<Instructor>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<Instructor>> GetAllByPackageIdAsync(long packageId)
    {
        try
        {
            await _connection.OpenAsync();
            var list = new List<Instructor>();
            string query = $"SELECT * FROM instructors WHERE package_id = {packageId} ORDER BY id";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var instructor = new Instructor();
                        instructor.Id = reader.GetInt64(0);
                        instructor.PackageId = reader.GetInt64(1);
                        instructor.FirstName = reader.GetString(2);
                        instructor.LastName = reader.GetString(3);
                        instructor.Date_of_birth = reader.GetFieldValue<DateOnly>(4);
                        instructor.IsMale = reader.GetBoolean(5);
                        instructor.Salary = reader.GetFloat(6);
                        instructor.Email = reader.GetString(7);
                        instructor.Title = reader.GetString(8);
                        instructor.PhoneNumber = reader.GetString(9);
                        instructor.ImagePath = reader.GetString(10);
                        instructor.Description = reader.GetString(11);
                        instructor.CreatedAt = reader.GetDateTime(12);
                        instructor.UpdatedAt = reader.GetDateTime(13);
                        list.Add(instructor);
                    }
                }
            }
            return list;
        }
        catch
        {
            return new List<Instructor>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Instructor editedObj)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE instructors SET package_id = @package_id, " +
                $" fname = @fname, lname = @lname, dob = @dob, is_male = @is_male, salary = @salary, " +
                $" email = @email, title = @title, phone_number = @phone_number, image_path = @image_path, description = @description, " +
                $" created_at = @created_at, updated_at = @updated_at WHERE id = {id};";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("package_id", editedObj.PackageId);
                command.Parameters.AddWithValue("fname", editedObj.FirstName);
                command.Parameters.AddWithValue("lname", editedObj.LastName);
                command.Parameters.AddWithValue("dob", editedObj.Date_of_birth);
                command.Parameters.AddWithValue("is_male", editedObj.IsMale);
                command.Parameters.AddWithValue("salary", editedObj.Salary);
                command.Parameters.AddWithValue("email", editedObj.Email);
                command.Parameters.AddWithValue("title", editedObj.Title);
                command.Parameters.AddWithValue("phone_number", editedObj.PhoneNumber);
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

    Task<Instructor> IRepository<Instructor, Instructor>.GetAsync(long id)
    {
        throw new NotImplementedException();
    }
}