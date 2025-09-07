using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccess.Entities
{
    public class ValueEntity
    {
        public Guid ValueId { get; set; }
        public string Value { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public ProductEntity? Product { get; set; }
        public Guid FieldId { get; set; }
        public FieldEntity? Field { get; set; }

    }
}