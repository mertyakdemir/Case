using CaseApp.DataLayer.Abstract;
using CaseApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseApp.DataLayer.Concrete
{
    public class ContentRepository : RepositoryBase<ContentModel, DatabaseContext>, IContentRepository
    {
    }
}
