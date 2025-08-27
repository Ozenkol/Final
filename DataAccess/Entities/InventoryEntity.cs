using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql.Internal.Postgres;

namespace DataAccess.Entities
{
    [Owned]
    public class InventoryEntity
    {
        public Guid InventoryId {get;set;}
        public string InventoryName { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public UserEntity User = null!;
        public ICollection<ProductEntity> Products = new List<ProductEntity>();
        public ICollection<FieldEntity> Fields = new List<FieldEntity>();

    }
}