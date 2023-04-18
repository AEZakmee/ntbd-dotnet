using Microsoft.AspNetCore.Mvc;
using MyApp.Data;

namespace MyProject.Controllers
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

        [HttpGet]
        public IEnumerable<DataEntity> Get()
        {
            return _dataContext.dataentities.ToList();
        }
    }
}
