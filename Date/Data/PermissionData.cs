using ChallengeN5.Data.Context;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace ChallengeN5.Data
{
    public class PermissionData : IPermissionData
    {
        private readonly AppDbContext Context;


        public PermissionData(AppDbContext context)
        {
            this.Context = context;
        }

        public async Task<List<Permission>> GetPermissions()
        {
            try
            {
                return await Context.Permission.Include(x=>x.PermissionType).OrderBy(x => x.NameEmployee).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Permission> RequestPermission(Permission model)
        {
            try
            {
                Context.Entry(model).State = EntityState.Added;
                await Context.SaveChangesAsync();
                return model; 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Permission> ModifyPermission(Permission model)
        {
            try
            {
                Context.Entry(model).State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
