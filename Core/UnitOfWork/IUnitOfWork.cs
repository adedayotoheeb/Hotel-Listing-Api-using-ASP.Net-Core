using HotelListing.Core.Repository;
using HotelListing.Models;
using System;
using System.Threading.Tasks;

namespace HotelListing.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels{ get; }
        Task SaveAsync();
    }
}
