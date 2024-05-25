using System.Data;

namespace ProductManagment
{
    public interface IDataUtility
    {
        Task ExecuteCommandAsync(string command, Dictionary<string, object> parameters
           /*CommandType commandType*/);
        //Task ExecuteCommandAsync(string sql, Dictionary<string, object> parameters);
        Task<List<Dictionary<string, object>>> GetDataAsync(string command,
            Dictionary<string, object> parameters/*, CommandType commandType*/);
    }
}
