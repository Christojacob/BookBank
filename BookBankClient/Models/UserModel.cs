using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBankClient.Models
{
    [DynamoDBTable("BookBankClientAdmin")]
    public class UserModel
    {
        [DynamoDBHashKey("UserId")]
        public String UserId { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }
    }
}
