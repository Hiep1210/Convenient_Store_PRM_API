using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConvenientStoreAPI.Common
{
    public enum APIEnum
    {
        [Description("http://localhost:5000/api")]
        BASE_URL,
        [Description("Products")]
        PRODUCT,
        [Description("Suppliers")]
        SUPPLIER,
        [Description("Categories")]
        CATEGORY,
        [Description("Users")]
        USER,
        [Description("Orders")]
        ORDER
    }
}
