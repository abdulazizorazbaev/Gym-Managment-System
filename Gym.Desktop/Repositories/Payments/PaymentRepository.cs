using Gym.Desktop.Entities.Instructors;
using Gym.Desktop.Entities.Payments;
using Gym.Desktop.Interfaces.Payments;
using Gym.Desktop.Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Desktop.Repositories.Payments;

public class PaymentRepository : BaseRepository, IPaymentRepository
{
    public Task<int> CreateAsync(Payment obj)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Payment>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            var list = new List<Payment>();
            string query = $"SELECT * FROM payments ORDER BY id OFFSET {@params.SkipCount} LIMIT {@params.PageSize}";
            await using (var command = new NpgsqlCommand(query, _connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var payment = new Payment();
                        payment.Id = reader.GetInt64(0);
                        payment.ClientId = reader.GetInt64(1);
                        payment.PaymentType = reader.GetString(2);
                        payment.Description = reader.GetString(3);
                        payment.CreatedAt = reader.GetDateTime(4);
                        payment.UpdatedAt = reader.GetDateTime(5);
                        list.Add(payment);
                    }
                }
            }
            return list;
        }
        catch
        {
            return new List<Payment>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<Payment> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(long id, Payment editedObj)
    {
        throw new NotImplementedException();
    }
}