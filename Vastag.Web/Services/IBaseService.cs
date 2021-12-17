using System;
using System.Threading.Tasks;
using Vastag.Web.Models;
using Vastag.Web.Models.DTOs;

namespace Vastag.Web.Services
{
    public interface IBaseService : IDisposable
    {
        ResponseDTO response { get; set; }
        Task<T> SendAsync<T>(ApiRequest request);
    }
}
