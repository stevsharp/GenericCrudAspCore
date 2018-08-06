using System;

namespace WebApiDemo.Models
{
    internal interface IAuditable
    {
        DateTime CreatedOn { get; set; }
        DateTime UpdatedOn { get; set; }

        DateTime UpdatedBy { get; set; }
    }

}
