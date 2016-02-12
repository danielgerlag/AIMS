using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public enum AttributeType : int
    {
        Text = 1,
        WholeNumber = 2,
        Decimal = 3,
        Percentage = 4,
        Date = 5,
        List = 6,
        Year = 7,
        Boolean = 8,
        CSIOList = 9

    };
}
