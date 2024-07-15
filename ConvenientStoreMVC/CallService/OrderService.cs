using ConvenientStoreAPI.Common;
using ConvenientStoreAPI.Mapper.DTO;
using ConvenientStoreAPI.Models;
using Flurl;
using Flurl.Http;
using Flurl.Http.Newtonsoft;

namespace ConvenientStoreMVC.CallService
{
    public class OrderService
    {
        public string link = APIEnum.BASE_URL.GetDescription() + APIEnum.ORDER.GetDescription();
        private NewtonsoftJsonSerializer serializer;
        public OrderService()
        {
            serializer = new Flurl.Http.Newtonsoft.NewtonsoftJsonSerializer();
        }
        public async Task<List<Order>> getAll()
        {
            return await link.GetJsonAsync<List<Order>>();
        }
        public async Task<Order> getById(int id)
        {
            List<Order> orders = await link.AppendQueryParam($"$filter=id eq {id}").GetJsonAsync<List<Order>>();
            return orders.First();
        }

        public async Task updateOrder(int id)
        {
            await link.AppendPathSegments("Process",id).GetJsonAsync<Order>();
        }
    }
}
