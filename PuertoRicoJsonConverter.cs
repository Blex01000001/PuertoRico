using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PuertoRicoSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{
    internal class PuertoRicoJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(PuertoRico).IsAssignableFrom(objectType);
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            Console.WriteLine(jsonObject);

            // 檢查 Type 屬性是否存在
            if (!jsonObject.TryGetValue("Type", out var typeToken))
                throw new JsonSerializationException("Missing 'Type' property in JSON.");

            var type = typeToken.ToString();

            // 根據 Type 和 Name 決定具體的類別
            if (type == "CargoAbstract")
            {
                var name = jsonObject["Name"]?.ToString();
                if (name == "Coffee")
                    return new Coffee();
                if (name == "Tobacco")
                    return new Tobacco();
                if (name == "Corn")
                    return new Corn();
                if (name == "Sugar")
                    return new Sugar();
                if (name == "Indigo")
                    return new Indigo();
            }
            else if (type == "RoleAbstract")
            {
                var name = jsonObject["Name"]?.ToString();
                if (name == "Builder")
                    return new Builder();
                if (name == "Captain")
                    return new Captain();
                if (name == "Craftsman")
                    return new Craftsman();
                if (name == "Mayor")
                    return new Mayor();
                if (name == "Prospector")
                    return new Prospector();
                if (name == "Settler")
                    return new Settler();
                if (name == "Trader")
                    return new Trader();
            }
            else
            {
                throw new JsonSerializationException("Unknown Type.");
            }
            throw new JsonSerializationException("Unknown Type.");
            //if (type == "Farm")
            //{
            //    var name = jsonObject["Name"]?.ToString();
            //    if (name == "CornFarm")
            //        return new CornFarm();
            //    if (name == "CoffeeFarm")
            //        return new CoffeeFarm();
            //    throw new JsonSerializationException("Unknown Farm type.");
            //}
            //else if (type == "Plant")
            //{
            //    var name = jsonObject["Name"]?.ToString();
            //    if (name == "CornPlant")
            //        return new CornPlant();
            //    if (name == "CoffeePlant")
            //        return new CoffeePlant();
            //    if (name == "House")
            //        return new House();
            //    throw new JsonSerializationException("Unknown Plant type.");
            //}
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is PuertoRico game)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Type");
                writer.WriteValue(game.ToString());
                writer.WritePropertyName("Name");
                writer.WriteValue(game.GetType().Name);
                writer.WriteEndObject();
            }
            else
            {
                throw new JsonSerializationException("Invalid object type for BuildingJsonConverter.");
            }
        }
    }
}
