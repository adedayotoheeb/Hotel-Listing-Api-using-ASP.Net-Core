﻿using HotelListing.Core.Repository;
using HotelListing.DbContexts;
using HotelListing.Models;
using System;
using System.Threading.Tasks;

namespace HotelListing.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Country> _countries;
        private IGenericRepository<Hotel> _hotels;
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;  
        }
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_context);

        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);  
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
