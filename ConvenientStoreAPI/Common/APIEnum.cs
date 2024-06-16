using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConvenientStoreAPI.Common
{
    public enum APIEnum
    {
        [Description("https://localhost:7024/api")]
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
