using ConvenientStoreAPI.Common;
using ConvenientStoreAPI.Models;
using Flurl;
using Flurl.Http;
using Flurl.Http.Newtonsoft;

namespace ConvenientStoreMVC.CallService
{
    public class SupplierService
    {
        public string link = APIEnum.BASE_URL.GetDescription() + APIEnum.SUPPLIER.GetDescription();
        private NewtonsoftJsonSerializer serializer;
        public SupplierService()
        {
            serializer = new Flurl.Http.Newtonsoft.NewtonsoftJsonSerializer();
        }
        public async Task<List<Supplier>> getSuppliers()
        {
            return await link.GetJsonAsync<List<Supplier>>();
        }
        public async Task<List<Supplier>> getSuppliersInSale()
        {
            return await link.AppendPathSegment("InSale").GetJsonAsync<List<Supplier>>();
        }

    }
}
