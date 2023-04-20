using App.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [ApiController]
    [Route("data")]
    public class DataController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public DataController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

       [HttpGet("filter")]
        public DataResponse Get([FromQuery] string name)
        {
            var query = _dataContext.dataentities
                .Where(data => data.name == name)
                .Select(data => new DataEntity {
                    id = data.id,
                    name = data.name
                });

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var data = query.ToList();
            stopwatch.Stop();

            string rawQuery = query.ToQueryString().Replace("\n", " ");

            var queryData = saveQuery(rawQuery, stopwatch.ElapsedMilliseconds);
            var response = exportAnswer(queryData);
            response.data = data;

            return response;
        }

        [HttpGet("all")]
        public DataResponse Get()
        {
            var query = _dataContext.dataentities
                .Select(data => new DataEntity {
                    id = data.id,
                    name = data.name
                });

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var data = query.ToList();

            stopwatch.Stop();

            string rawQuery = query.ToQueryString().Replace("\n", " ");

            var queryData = saveQuery(rawQuery, stopwatch.ElapsedMilliseconds);
            var response = exportAnswer(queryData);
            response.data = data;

            return response;
        }

        private QueryEntity saveQuery(String rawQuery, long executiontime) {

            var queryData = new QueryEntity {
                query = rawQuery.ToString() ?? "",
                executiontime = executiontime,
            };

            _dataContext.queries.Add(queryData);
            _dataContext.SaveChanges();
    
            return queryData;
        }

        private DataResponse exportAnswer(QueryEntity queryData) {
            List<QueryEntity> allQueries = _dataContext.queries.ToList();
            List<QueryEntity> similarQueries = allQueries.Where(q => QueryComparer.AreQueriesSimilar(q.query, queryData.query)).ToList();

            double totalExecutionTime = 0.0;
            foreach (QueryEntity queryEntity in similarQueries)
            {
                totalExecutionTime += queryEntity.executiontime;
            }

            double averageExecutionTime = totalExecutionTime / similarQueries.Count;
            
            DataResponse response = new DataResponse();
            response.similarQueries = similarQueries;
            response.avarageExecutionTime = averageExecutionTime;

            return response;
        }
    }

    public class QueryComparer
    {
        public static bool AreQueriesSimilar(string query1, string query2)
        {
            query1 = query1.ToLower();
            query2 = query2.ToLower();
            
            int distance = LevenshteinDistance(query1, query2);
            
            int maxLength = Math.Max(query1.Length, query2.Length);
            
            double similarity = 100 * (1 - (double)distance / maxLength);
            
            return similarity > 50;
        }

        private static int LevenshteinDistance(string s, string t)
        {
            int[,] distance = new int[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++)
            {
                distance[i, 0] = i;
            }

            for (int j = 0; j <= t.Length; j++)
            {
                distance[0, j] = j;
            }

            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;

                    distance[i, j] = Math.Min(
                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[s.Length, t.Length];
        }
    }
}
