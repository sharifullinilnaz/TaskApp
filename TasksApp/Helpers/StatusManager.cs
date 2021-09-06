using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

using TasksApp.Services;

namespace TasksApp.Helpers
{
    public class StatusManager
    {

        public static async Task StatusesInitialization(IServiceProvider serviceProvider)
        {
            var statusManager = serviceProvider.GetRequiredService<IStatusService>();
            string[] statuses = { "Создана", "В работе", "Завершена" };

            foreach (var status in statuses)
            {
                var isStatusExist = statusManager.IsExist(status);
                if (!isStatusExist)
                {
                    await statusManager.CreateAsync(status);
                }
            }
        }
    }
}
