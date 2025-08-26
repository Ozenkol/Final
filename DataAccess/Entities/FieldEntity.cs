using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccess.Entities
{
    public class FieldEntity
    {
        public int FieldId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int InventoryId { get; set; }
        public InventoryEntity ?Inventory { get; set; }
        public ICollection<ValueEntity> Values = new List<ValueEntity>();


    }
}