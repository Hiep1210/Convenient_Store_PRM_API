using Flurl.Http.Newtonsoft;

namespace ConvenientStoreMVC.Common
{
    public class Serializer
    {
        public static NewtonsoftJsonSerializer newtonsoft = new NewtonsoftJsonSerializer();
    }
}
