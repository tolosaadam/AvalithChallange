using Microsoft.Extensions.Configuration;
using N5Company.Business.Interfaces;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Business
{
    public class ElasticSearchBusiness<T> : IElasticSearchBusiness<T> where T : class
    {
        private readonly ElasticClient _client;
        private readonly string _indexName;

        public ElasticSearchBusiness(IConfiguration configuration)
        {
            var settings = new ConnectionSettings(new Uri(configuration["ElasticSearch:Url"]))
                .DefaultIndex(configuration["ElasticSearch:IndexName"])
                .DefaultMappingFor<T>(m => m.IndexName(_indexName));

            _client = new ElasticClient(settings);
            _indexName = configuration["ElasticSearch:IndexName"];
        }

        public async Task<bool> IndexAsync(T document)
        {
            var response = await _client.IndexAsync(document, idx => idx.Index(_indexName));
            return response.IsValid;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var response = await _client.DeleteAsync<T>(id, idx => idx.Index(_indexName));
            return response.IsValid;
        }

        public async Task<IEnumerable<T>> SearchAsync(string query)
        {
            var response = await _client.SearchAsync<T>(s => s
                .Index(_indexName)
                .Query(q => q
                    .MultiMatch(m => m
                        .Fields(f => f.Field("*"))
                        .Type(TextQueryType.BestFields)
                        .Operator(Operator.Or)
                        .Query(query)
                    )
                )
            );

            if (!response.IsValid)
            {
                throw new Exception($"Failed to execute search: {response.DebugInformation}");
            }

            return response.Documents;
        }
    }
}
