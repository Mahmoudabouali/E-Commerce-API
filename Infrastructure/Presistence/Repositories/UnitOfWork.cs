﻿using Domain.Contracts;
using Domain.Entities;
using Persistence.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private readonly ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _repositories = new();
        }
        public async Task<int> SaveChangesAsync() => await _storeContext.SaveChangesAsync();
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
             => (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, _ => new GenricRepository<TEntity, TKey>(_storeContext));
            //var typeName = typeof(TEntity).Name;
            //if (_repositories.ContainsKey(typeName))
            //    return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
            //var repo = new GenricRepository<TEntity, TKey>(_storeContext);
            //_repositories.Add(typeName, repo);
            //return repo;


    }
}