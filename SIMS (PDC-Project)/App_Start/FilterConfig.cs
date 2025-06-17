using System.Web;
using System.Web.Mvc;

namespace SIMS__PDC_Project_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
