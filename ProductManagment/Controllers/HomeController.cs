using Microsoft.AspNetCore.Mvc;
using ProductManagment.Models;
using System.Data;
using System.Diagnostics;

namespace ProductManagment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataUtility _dataUtility;

        public HomeController(ILogger<HomeController> logger, IDataUtility dataUtility)
        {
            _logger = logger;
            _dataUtility = dataUtility;


        }

        public async Task<IActionResult> Index()
        {
            //string sql = $"insert into CoverTypes (Id,Name) values(@xId, @xName)";

            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("xID", ID.NewId());
           // parameters.Add("xName", "Dahas");
            //parameters.Add("xId", Guid.NewGuid());
            //parameters.Add("xTitle", "UX Design");
            //parameters.Add("xClassStartDate", DateTime.Now.AddDays(30));

            //await _dataUtility.ExecuteCommandAsync(sql, parameters);


            string sql = "Select * from products ";
            var data = await _dataUtility.GetDataAsync(sql, null/*, System.Data.CommandType.StoredProcedure*/);


            return View();
       
        
        
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}