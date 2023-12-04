using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeN5
{
    [Table("Permissions")]
    public class Permission
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string NameEmployee { get; set; }
        public virtual string SurinameEmployee { get; set; }
        public virtual int IdPermissionType { get; set; }
        public virtual DateTime PermissionDate { get; set; }
        [ForeignKey("IdPermissionType")]
        public virtual PermissionType? PermissionType { get; set; }
    }
}
