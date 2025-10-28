using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserVault.Domain.Entities
{
    public class BaseEntity
    {
        [Column("CreatedOn", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Column("UpdatedOn", TypeName = "datetime")]
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
