using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeN5
{
    [Table("PermissionTypes")]
    public class PermissionType
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
