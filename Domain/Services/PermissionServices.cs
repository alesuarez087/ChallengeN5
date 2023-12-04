

using Domain.Repository;

namespace ChallengeN5.Domain
{
    public interface IPermissionServices
    {
        Task<Permission> RequestPermission(Permission model);
        Task<Permission> ModifyPermission(Permission model);
        Task<List<Permission>> GetPermissions();
    }

    public  class PermissionServices : IPermissionServices
    {
        private readonly IPermissionData _permissionData;

        public PermissionServices(IPermissionData permissionData) {
            _permissionData = permissionData;
        }

        public async Task<List<Permission>> GetPermissions()
        {
            return await this._permissionData.GetPermissions();
        }

        public async Task<Permission> ModifyPermission(Permission model)
        {
            return await this._permissionData.ModifyPermission(model);
        }

        public async Task<Permission> RequestPermission(Permission model)
        {
            return await _permissionData.RequestPermission(model);
        }
    }
}
