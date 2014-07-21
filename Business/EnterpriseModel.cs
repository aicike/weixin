using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;

namespace Business
{
    public class EnterpriseModel : BaseModel<Enterprise>
    {
        public IQueryable<Enterprise> GetList()
        {
            return List();
        }
    }
}
