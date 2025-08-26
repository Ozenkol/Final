using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts.Value
{
    public record ValueResponse
    (
        int valueId,
        string value
    );
}