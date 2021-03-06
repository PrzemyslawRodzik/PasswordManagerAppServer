﻿using Microsoft.EntityFrameworkCore;
using PasswordManagerAppResourceServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace PasswordManagerAppResourceServer.Data
{
    public class RepositoryBase : IRepositoryBase
    {
        protected readonly DbContext Context;
        
        
        public RepositoryBase(DbContext context)
        {
            Context = context;
        }


        public  TEntity GetById<TEntity>(int id) where TEntity : class
        {
           return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>().ToList();
        }
       

        public IEnumerable<TEntity> FindByCondition<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return Context.Set<TEntity>().Where(predicate);
        }
        public TEntity Find<TEntity>(int id) where TEntity : class
        {
             return Context.Set<TEntity>().Find(id);
        }


        public TEntity SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Context.Set<TEntity>().AddRange(entities);
        }

    public void Update<TEntity>(TEntity entity) where TEntity : class
    {

            Context.Set<TEntity>().Update(entity);
            
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        


    }
}
