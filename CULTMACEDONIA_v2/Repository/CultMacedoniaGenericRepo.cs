using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CULTMACEDONIA_v2.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class CultMacedoniaGenericRepo<TEntity> : ICultMacedoniaGenericRepo<TEntity> where TEntity : class
    {

        protected DbSet<TEntity> DbSet;

        private readonly DbContext _dbContext;


        public CultMacedoniaGenericRepo(DbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Default Empty constructor για το CultMacedoniaGeneric Repository
        /// </summary>
        public CultMacedoniaGenericRepo()
        {
        }

        /// <summary>
        /// Υλοποίηση της συνάρτησης του Generic Repository Interface για την
        /// εύρεση ενός entity με βάση το Primary Key του
        /// </summary>
        /// <param name="id">το id για αναζήτηση</param>
        /// <returns></returns>
        public async Task<TEntity> GetEntityByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        /// <summary>
        /// Υλοποίηση της συνάρτησης του Generic Repository Interface για την
        /// επιστροφή όλου του συνόλου entities ενός DbSet του Context
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }



        /// <summary>
        /// Υλοποίηση της συνάρτησης του Generic Repository Interface για την
        /// εισαγωγή ενός entity αντικειμένο στο συσγκεκριμένο DbSet του context 
        /// </summary>
        /// <param name="entity">το προς αποθήκευση entity</param>
        /// <returns></returns>
        public async Task<int> InsertEntityAsync(TEntity entity)
        {
            DbSet.Add(entity);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> EditEntityAsync(TEntity entity)
        {
            
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteEntityAsync(TEntity entity)
        {
         
            DbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        
        }




        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {

            if (!this.disposed)
                if (disposing)
                    _dbContext.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}