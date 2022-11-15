using Amazon.DynamoDBv2.DataModel;

namespace TrawtelCRMAPI.DynamoDB
{
    [DynamoDBTable("searchdata")]
    public class searchdata
    {
        [DynamoDBHashKey("id")]
        public Guid Id { get; set; }

        [DynamoDBProperty("data")]
        public string? data { get; set; }

        [DynamoDBProperty("ttl")]
        public DateTime ttl { get; set; }
    }
}
