//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace PuertoRicoSpace
{
    //public class CargoConverter : JsonConverter<CargoAbstract>
    //{
    //    public override CargoAbstract Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        Console.WriteLine("200");
    //        if (reader.TokenType != JsonTokenType.StartObject)
    //        {
    //            throw new JsonException("Expected start of object.");
    //        }

    //        string name = null;
    //        int qty = 0;

    //        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
    //        {
    //            if (reader.TokenType == JsonTokenType.PropertyName)
    //            {
    //                var propertyName = reader.GetString();
    //                reader.Read(); // 移動到屬性值

    //                if (propertyName == "Name")
    //                {
    //                    name = reader.GetString();
    //                }
    //                else if (propertyName == "Qty")
    //                {
    //                    qty = reader.GetInt32();
    //                }
    //            }
    //        }

    //        if (string.IsNullOrEmpty(name))
    //        {
    //            throw new JsonException("Missing required property: Name.");
    //        }

    //        CargoAbstract cargo;
    //        if (name == "Coffee")
    //        {
    //            cargo = new Coffee();
    //        }
    //        else if (name == "Tobacco")
    //        {
    //            cargo = new Tobacco();
    //        }
    //        else if (name == "Corn")
    //        {
    //            cargo = new Corn();
    //        }
    //        else if (name == "Sugar")
    //        {
    //            cargo = new Sugar();
    //        }
    //        else if (name == "Indigo")
    //        {
    //            cargo = new Indigo();
    //        }
    //        else
    //        {
    //            throw new JsonException($"Unknown cargo type: {name}");
    //        }
    //        cargo.Add(qty);
    //        //cargo.Qty = qty;
    //        return cargo;
    //    }

    //    public override void Write(Utf8JsonWriter writer, CargoAbstract value, JsonSerializerOptions options)
    //    {
    //        writer.WriteStartObject();
    //        writer.WriteString("Name", value.Name);
    //        writer.WriteNumber("Qty", value.Qty);
    //        writer.WriteEndObject();
    //    }
    //}
    //public class CargoContainer
    //{
    //    [JsonConverter(typeof(CargoListConverter))]
    //    public List<CargoAbstract> Cargos { get; private set; }

    //    public CargoContainer()
    //    {
    //        Cargos = new List<CargoAbstract>();
    //    }
    //}
    public class CargoListConverter : JsonConverter<List<CargoAbstract>>
    {
        public override List<CargoAbstract> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var cargos = new List<CargoAbstract>();

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException("Expected start of array.");
            }

            reader.Read(); // 進入陣列內

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                var cargo = ReadSingleCargo(ref reader, options);
                cargos.Add(cargo);
                reader.Read();
            }

            return cargos;
        }

        private CargoAbstract ReadSingleCargo(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected start of object.");
            }

            string name = null;
            int qty = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read(); // 移動到屬性值

                    if (propertyName == "Name")
                    {
                        name = reader.GetString();
                    }
                    else if (propertyName == "Qty")
                    {
                        qty = reader.GetInt32();
                    }
                }
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new JsonException("Missing required property: Name.");
            }

            CargoAbstract cargo;
            if (name == "Coffee")
            {
                cargo = new Coffee();
            }
            else if (name == "Tobacco")
            {
                cargo = new Tobacco();
            }
            else if (name == "Corn")
            {
                cargo = new Corn();
            }
            else if (name == "Sugar")
            {
                cargo = new Sugar();
            }
            else if (name == "Indigo")
            {
                cargo = new Indigo();
            }
            else
            {
                throw new JsonException($"Unknown cargo type: {name}");
            }

            cargo.Add(qty);
            //cargo.Qty = qty;
            return cargo;
        }

        public override void Write(Utf8JsonWriter writer, List<CargoAbstract> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var cargo in value)
            {
                writer.WriteStartObject();
                writer.WriteString("Name", cargo.Name);
                writer.WriteNumber("Qty", cargo.Qty);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }
    }
}
