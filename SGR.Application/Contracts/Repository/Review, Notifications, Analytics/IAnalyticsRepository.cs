using SGR.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Contracts.Repository
{
    public interface IAnalyticsRepository
    {
        Task<OperationResult> GetRestaurantMetricsAsync(int restaurantId);
        Task<OperationResult> GetSystemSummaryAsync();
    }
}
