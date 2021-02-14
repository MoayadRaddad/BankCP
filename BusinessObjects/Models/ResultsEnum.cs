using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public enum ResultsEnum
    {
        notFound,
        notDeleted,
        inserted,
        notInserted,
        filled,
        notFilled,
        error,
        notAuthorize,
        updated,
        notUpdated,
        deleted
    }
    public enum sqlResultsEnum
    {
        failed = 0,
        success = 1
    }
}
