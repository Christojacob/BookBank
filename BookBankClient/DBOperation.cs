using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using BookBankClient.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookBankClient
{
    public class DBOperation
    {
        private DynamoDBContext context;
        AmazonDynamoDBClient client;
        string tableName = "BookBankClientAdmin";
        Table users;

        public DBOperation()
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("AppSettings.Json", optional: true, reloadOnChange: true);

            var accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;

            var credentials = new BasicAWSCredentials(accessKeyID, secretKey);
            client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            context = new DynamoDBContext(client);


        }


        public Task<List<UserModel>> GetUser(String id)
        {
            List<ScanCondition> ScanConditions = new List<ScanCondition>() { new ScanCondition("UserId", ScanOperator.Equal, id) };
            var userRetrieved = context.ScanAsync<UserModel>(ScanConditions);
            return userRetrieved.GetNextSetAsync();
        }

    }
}
