using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;

namespace Business
{
    public class ProjectModel : BaseModel<Project>
    {
        public IQueryable<Project> GetProjectByEID(int eid)
        {
            return List().Where(a => a.EnterpriseID == eid);
        }
    }
}
