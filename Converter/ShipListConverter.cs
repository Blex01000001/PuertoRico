using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PuertoRicoSpace
{
    public class ShipListConverter : JsonConverter<List<Ship>>
    {
        public override List<Ship> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var ships = new List<Ship>();

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException("Expected start of array.");
            }

            reader.Read(); // 進入陣列內

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                var ship = ReadSingleCargo(ref reader, options);
                ships.Add(ship);
                reader.Read();
            }

            return ships;
        }

        private Ship ReadSingleCargo(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected start of object.");
            }

            string type = null;
            string cargo = null;

            int qantity = 0;
            int maxCargoQuantity = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read(); // 移動到屬性值

                    if (propertyName == "Type")
                    {
                        type = reader.GetString();
                    }
                    else if (propertyName == "Cargo")
                    {
                        cargo = reader.GetString();
                    }
                    else if (propertyName == "Quantity")
                    {
                        qantity = reader.GetInt32();
                    }
                    else if (propertyName == "MaxCargoQuantity")
                    {
                        maxCargoQuantity = reader.GetInt32();
                    }
                }
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new JsonException("Missing required property: Name.");
            }

            Ship ship = new Ship(maxCargoQuantity, type);
            //if (name == "Coffee")
            //{
            //    ship = new Coffee();
            //}
            //else if (name == "Tobacco")
            //{
            //    ship = new Tobacco();
            //}
            //else if (name == "Corn")
            //{
            //    ship = new Corn();
            //}
            //else if (name == "Sugar")
            //{
            //    ship = new Sugar();
            //}
            //else if (name == "Indigo")
            //{
            //    ship = new Indigo();
            //}
            //else
            //{
            //    throw new JsonException($"Unknown ship type: {name}");
            //}

            ship.JsonSet(type: type, cargo: cargo, qty: qantity, maxqty: maxCargoQuantity);
            //cargo.Qty = qty;
            return ship;
        }

        public override void Write(Utf8JsonWriter writer, List<Ship> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var ship in value)
            {
                writer.WriteStartObject();
                writer.WriteString("Type", ship.Type);
                writer.WriteString("Cargo", ship.Cargo);
                writer.WriteNumber("Quantity", ship.Quantity);
                writer.WriteNumber("MaxCargoQuantity", ship.MaxCargoQuantity);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }
    }
}
