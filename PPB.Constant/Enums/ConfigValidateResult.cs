using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.Constant.Enums
{
    public enum ConfigValidateResult
    {
        Valid, //'Task is valid
        NotFound, //No Records Found for Search Criteria - RED X
        Invalid, //Records found for Specific Instance of Task but Task_Number not recorded. YELLOW X
        NotFoundValidationField //Don't contains the field to validate, such as Task_Number or others
    }
}
