using System;
using Domain.Model.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Views
{
    public static class QuantityViewQueue
    {
        [FunctionName("QuantityViewQueue")]
        public static void Run([QueueTrigger("quantity-view", Connection = "AzureWebJobsStorage")]  Donation donation, ILogger log)
        {
            log.LogInformation($"starting function, yeahhh! Quantity views: {donation.QuantityView}");

            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var textSql = $@"UPDATE [dbo].[Donation] SET [QuantityView] = [QuantityView] + 1 WHERE [Id] = {donation.Id};";

                using (SqlCommand cmd = new SqlCommand(textSql, conn))
                {
                    var rowsAffected = cmd.ExecuteNonQuery();
                    log.LogInformation($"rowsAffected: {rowsAffected}");
                }
            }
        }
    }
}

