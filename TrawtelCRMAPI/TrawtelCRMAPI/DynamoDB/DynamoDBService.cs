using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.Internal.Transform;
using Entities.Models;
using Newtonsoft.Json;

namespace TrawtelCRMAPI.DynamoDB
{
    public class DynamoDBService
    {
        private readonly IDynamoDBContext _context;
        public DynamoDBService(IDynamoDBContext context)
        {
            _context = context;
        }
        private static string tableName = "APISearchData";
        public bool SaveSearchData(List<CommonFlightDetails> commonFlightDetails)
        {
            for (int i = 0; i < commonFlightDetails.Count; i++)
            {
                CreateItem(commonFlightDetails[i]);
            }
            return true;
        }
        public async Task CreateItem(CommonFlightDetails commonFlightDetails)
        {
            var request = new PutItemRequest
            {
                TableName = tableName,
                Item = new Dictionary<string, AttributeValue>()
            {
                { "Id", new AttributeValue {
                      N = commonFlightDetails.TFId.ToString()
                  }},
                { "Data", new AttributeValue {
                      S = JsonConvert.SerializeObject(commonFlightDetails)
                  }}
            }
            };
            await _context.SaveAsync(request);
        }
    }
}
