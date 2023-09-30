using Gym.Desktop.Entities.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Desktop.Interfaces.Payments;

public interface IPaymentRepository : IRepository<Payment, Payment>
{
}