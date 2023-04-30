using ExamApp.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Data.Repositories
{
    public class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ExamAppDbContext examAppDbContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private ExamAppUser cachedUser;

        public BaseRepository(ExamAppDbContext examAppDbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.examAppDbContext = examAppDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await this.examAppDbContext.AddAsync(entity);
            await this.examAppDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities)
        {
            var currentUser = await this.GetCurrentUserAsync();

            await this.examAppDbContext.AddRangeAsync(entities.Select(entity =>
            {
                return entity;
            }).ToList());

            await this.examAppDbContext.SaveChangesAsync();
            return entities;
        }

        public IQueryable<TEntity> RetrieveAll()
        {
            return this.RetrieveAllTracked().AsNoTracking();
        }

        public IQueryable<TEntity> RetrieveAllTracked()
        {
            return this.examAppDbContext.Set<TEntity>();
        }

        public async Task<TEntity> EditAsync(TEntity entity)
        {
            this.examAppDbContext.Update(entity);
            await this.examAppDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> EditWithoutMetadataAsync(TEntity entity)
        {
            this.examAppDbContext.Update(entity);
            await this.examAppDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            this.examAppDbContext.Remove(entity);
            await this.examAppDbContext.SaveChangesAsync();
            return entity;
        }

        protected async Task<ExamAppUser> GetCurrentUserAsync()
        {
            if (this.cachedUser == null)
            {
                string userId = this._httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                this.cachedUser = await this.examAppDbContext.Users
                    .SingleOrDefaultAsync(user => user.Id == userId);
            }

            return this.cachedUser;
        }
    }
}
