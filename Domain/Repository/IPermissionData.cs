using ChallengeN5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IPermissionData
    {
        Task<Permission> RequestPermission(Permission model);
        Task<Permission> ModifyPermission(Permission model);
        Task<List<Permission>> GetPermissions();
    }
}
