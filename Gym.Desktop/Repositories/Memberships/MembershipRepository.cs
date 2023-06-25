using Gym.Desktop.Entities.Memberships;
using Gym.Desktop.Interfaces.Memberships;
using Gym.Desktop.Utilities;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Desktop.Repositories.Memberships;

class MembershipRepository : BaseRepository, IMembershipRepository
{
    public Task<int> CountAsync()
    {
        throw new System.NotImplementedException();
    }

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

    public Task<int> DeleteAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public Task<IList<Membership>> GetAllAsync(PaginationParams @params)
    {
        throw new System.NotImplementedException();
    }

    public Task<Membership> GetAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public Task<int> UpdateAsync(long id, Membership editedObj)
    {
        throw new System.NotImplementedException();
    }
}