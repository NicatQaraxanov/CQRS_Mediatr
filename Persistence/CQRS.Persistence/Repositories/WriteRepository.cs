﻿using CQRS.Application.Repositories;
using CQRS.Domain.Entities.Common;
using CQRS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly CQRS_DbContext context;

        public WriteRepository(CQRS_DbContext context)
        {
            this.context = context;
        }

        public DbSet<T> Table => context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            var entry = await Table.AddAsync(entity);
            return entry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public bool Remove(T entity)
        {
            var entry = Table.Remove(entity);
            return entry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            T model = await Table.FindAsync(id);
            var entry = Table.Remove(model);
            return entry.State == EntityState.Deleted;
        }

        public async Task<int> SaveChangesAsync()
            => await context.SaveChangesAsync();

        public bool Update(T entity)
        {
            var entry = Table.Update(entity);
            return entry.State == EntityState.Modified;
        }
    }
}
