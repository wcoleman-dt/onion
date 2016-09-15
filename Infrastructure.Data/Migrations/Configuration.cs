using Domain.Entities;

namespace Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.Data.MonthlyReportingModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Infrastructure.Data.MonthlyReportingModel context)
        {

            context.ApiKeyTypes.AddOrUpdate(new ApiKeyType()
            {
                EndpointType = "Insert Key",
                Description = "Insert keys let you insert events into your account with just a simple HTTPS request."
            });

            context.ApiKeyTypes.AddOrUpdate(new ApiKeyType()
            {
                EndpointType = "Query Key",
                Description = "Query keys let you run the same queries that you can in Insights via an HTTPS request. Query keys are not query-specific - any query key in your account will run any query."
            });

            context.SaveChanges();

            context.ApiEndpoints.AddOrUpdate(new ApiEndpoint()
            {
                AccountId = 1158760,
                ApiKey = "cF-W4vT94on1RaoFieppgOo5R6EYKTNP",
                Title = "",
                Endpoint = "https://insights-collector.newrelic.com/v1/accounts/1158760/events",
                Curl = String.Empty,
                NRSQLSyntax = null,
                ApiKeyType = 1
            });

            context.ApiEndpoints.AddOrUpdate(new ApiEndpoint()
            {
                AccountId = 1158760,
                ApiKey = "nV2tOJ-8_xXMwH4yp_jjYL3h1Wp6HFBe",
                Title = "Generating Monthly Reports",
                Endpoint = "https://insights-api.newrelic.com/v1/accounts/1158760/query?nrql=",
                Curl = String.Empty,
                NRSQLSyntax = "SELECT * from Transaction SINCE {0} minutes ago LIMIT {1}",
                ApiKeyType = 2
            });

            context.SaveChanges();

            //context.Database.ExecuteSqlCommand("truncate table event");
            //context.SaveChanges();
        }
    }
}
