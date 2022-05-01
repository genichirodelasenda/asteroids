using System.Collections.Generic;

namespace AsteroidsApi
{
    public interface IAsteroidsService
    {
        Task<string> GetInfoFromApi(string planet);
    }
}
