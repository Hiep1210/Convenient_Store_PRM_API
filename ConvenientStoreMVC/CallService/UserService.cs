using ConvenientStoreAPI.Common;
using ConvenientStoreAPI.Models;
using Flurl;
using Flurl.Http;
using Flurl.Http.Newtonsoft;

namespace ConvenientStoreMVC.CallService
{
    public class UserService
    {
        public string link = APIEnum.BASE_URL.GetDescription() + APIEnum.USER.GetDescription();
        private NewtonsoftJsonSerializer serializer;
        public UserService()
        {
            serializer = new Flurl.Http.Newtonsoft.NewtonsoftJsonSerializer();
        }
        public async Task<List<User>> getAll()
        {
            return await link.GetJsonAsync<List<User>>();
        }
        public async Task<List<User>> getAllInSale()
        {
            return await link.AppendPathSegment("InSale").GetJsonAsync<List<User>>();
        }

    }
}
