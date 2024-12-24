using Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataProvider.IDataProvider
{
    public interface IBreadcrumbItemDataProvider
    {
        public List<UrlItemName> GenerateBreadcrumbItemDataProviderItemDatas();
    }
}
