using Gym.Desktop.Constants;
using Gym.Desktop.Entities.Instructors;
using Gym.Desktop.Interfaces;
using Gym.Desktop.Interfaces.Instructors;
using Gym.Desktop.Utilities;
using Gym.Desktop.ViewModels.Instructors;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Desktop.Repositories.Instructors;

public class InstructorRepository : IInstructorRepository
{
    private readonly NpgsqlConnection _connection;

    public InstructorRepository()
    {
        _connection = new NpgsqlConnection(DbConstants.DB_CONNECTION_STRING);
    }

    public async Task<int> CreateAsync(Instructor obj)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.instructors (" +
                "fname, lname, dob, is_male, salary, email, title, phone_number, image_path, description, created_at, updated_at) " +
                "VALUES (@fname, @lname, @dob, @is_male, @salary, @email, @title, @phone_number, @image_path, @description, @created_at, @updated_at);";

            await using (var command = new NpgsqlCommand(query, _connection))
            {
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

    public Task<int> DeleteAsync(long id)
    {
        throw new NotImplementedException();
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
                        instructor.FirstName = reader.GetString(1);
                        instructor.LastName = reader.GetString(2);
                        instructor.Date_of_birth = reader.GetFieldValue<DateOnly>(3);
                        instructor.IsMale = reader.GetBoolean(4);
                        instructor.Salary = reader.GetFloat(5);
                        instructor.Email = reader.GetString(6);
                        instructor.Title = reader.GetString(7);
                        instructor.PhoneNumber = reader.GetString(8);
                        instructor.ImagePath = reader.GetString(9);
                        instructor.Description = reader.GetString(10);
                        instructor.CreatedAt = reader.GetDateTime(11);
                        instructor.UpdatedAt = reader.GetDateTime(12);
                        instructor.PackageId = reader.GetInt64(13);
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

    public Task<InstructorViewModel> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(long id, Instructor editedObj)
    {
        throw new NotImplementedException();
    }

    Task<IList<InstructorViewModel>> IRepository<Instructor, InstructorViewModel>.GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }
}