using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace ConvenientStoreAPI.Serializer
{
    public class IgnoreVirtualContractResolver : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        //protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        //{
        //    JsonProperty prop = base.CreateProperty(member, memberSerialization);
        //    var propInfo = member as PropertyInfo;
        //    if (propInfo != null)
        //    {
        //        if (propInfo.GetMethod.IsVirtual && !propInfo.GetMethod.IsFinal)
        //        {
        //            prop.ShouldSerialize = obj => false;
        //        }
        //    }
        //    return prop;
        //}
        public override object? ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var target = Activator.CreateInstance(objectType);

            foreach (var property in objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!property.GetMethod.IsVirtual || property.GetMethod.IsFinal)
                {
                    var jsonProperty = jsonObject[property.Name];
                    if (jsonProperty != null)
                    {
                        var value = jsonProperty.ToObject(property.PropertyType, serializer);
                        property.SetValue(target, value);
                    }
                }
            }

            return target;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
