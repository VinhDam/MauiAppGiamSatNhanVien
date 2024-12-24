using Shared.Data;
using Shared.DataProvider.IDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataProvider
{
    public class BreadcrumbItemDataProvider : IBreadcrumbItemDataProvider
    {
        public List<UrlItemName> GenerateBreadcrumbItemDataProviderItemDatas()
        {
            return new List<UrlItemName>()
            {
                new UrlItemName() { VietName = "Nhân viên", EngName = "employee", Url="/employee", IsActive = true },
                new UrlItemName() { VietName = "Danh mục", EngName = "danhmuc" , Url = "", IsActive = false },
                new UrlItemName() { VietName = "Phòng ban", EngName = "department" , Url = "/danhmuc/department", IsActive = true },
                new UrlItemName() { VietName = "Ca làm việc", EngName = "shift" , Url = "/danhmuc/shift", IsActive = true },
                new UrlItemName() { VietName = "Vị trí", EngName = "location" , Url = "/danhmuc/location", IsActive = true },
            };
        }
    }
}
