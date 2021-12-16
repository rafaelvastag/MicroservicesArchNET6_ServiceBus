using ProductAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vastag.Web.Models;

namespace Vastag.Web.Services
{
    public interface IBaseService : IDisposable
    {
        ResponseDTO response { get; set; }
        Task<T> SendAsync<T>(ApiRequest request);
    }
}
