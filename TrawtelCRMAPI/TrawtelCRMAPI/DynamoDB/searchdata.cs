using Amazon.DynamoDBv2.DataModel;

namespace TrawtelCRMAPI.DynamoDB
{
    [DynamoDBTable("APISearchData")]
    public class APISearchData
    {
        [DynamoDBHashKey("id")]
        public Guid Id { get; set; }

        [DynamoDBProperty("data")]
        public string? Data { get; set; }

        [DynamoDBProperty("ttl")]
        public DateTime TTL { get; set; }
    }
}
