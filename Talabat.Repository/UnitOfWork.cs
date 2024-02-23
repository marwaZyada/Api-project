﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _repositories = new Hashtable();
        }
        public async Task<int> Complete()
        => await _storeContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        =>await _storeContext.DisposeAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(type))
            {
                var repo=new GenericRepository<TEntity>(_storeContext);
                _repositories.Add(type, repo);
            }
            return _repositories[type] as IGenericRepository<TEntity>;
            
        }
    }
}
