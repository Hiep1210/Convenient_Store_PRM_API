using Flurl.Http.Newtonsoft;

namespace ConvenientStoreAPI.Common
{
    public class Serializer
    {
        public static NewtonsoftJsonSerializer newtonsoft = new NewtonsoftJsonSerializer();
    }
}
