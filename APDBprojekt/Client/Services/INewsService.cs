using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APDBprojekt.Client.Models;

namespace APDBprojekt.Client.Services
{
    public interface INewsService
    {
        public Task<List<News>> GetNews(string ticker);
    }
}
