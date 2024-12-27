using PuertoRicoSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Reflection;

namespace PuertoRicoSpace
{
    public class BuildingListConverter : JsonConverter<List<BuildingAbstract>>
    {
        public override List<BuildingAbstract> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var buildings = new List<BuildingAbstract>();

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException("Expected start of array.");
            }

            reader.Read(); // 進入陣列內

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                var building = ReadSingleCargo(ref reader, options);
                buildings.Add(building);
                reader.Read();
            }

            return buildings;
        }

        private BuildingAbstract ReadSingleCargo(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected start of object.");
            }

            string name = null;
            string industry = null;
            string buildingType = null;
            string scale = null;
            int worker = 0;
            int maxworker = 0;
            int cost = 0;
            int score = 0;
            int level = 0;
            int priority = 0;

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
                    else if (propertyName == "Industry")
                    {
                        industry = reader.GetString();
                    }
                    else if (propertyName == "Type")
                    {
                        buildingType = reader.GetString();
                    }
                    else if (propertyName == "Scale")
                    {
                        scale = reader.GetString();
                    }
                    else if (propertyName == "Worker")
                    {
                        worker = reader.GetInt32();
                    }
                    else if (propertyName == "MaxWorker")
                    {
                        maxworker = reader.GetInt32();
                    }
                    else if (propertyName == "Cost")
                    {
                        cost = reader.GetInt32();
                    }
                    else if (propertyName == "Score")
                    {
                        score = reader.GetInt32();
                    }
                    else if (propertyName == "Level")
                    {
                        level = reader.GetInt32();
                    }
                    else if (propertyName == "Priority")
                    {
                        priority = reader.GetInt32();
                    }
                }
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new JsonException("Missing required property: Name.");
            }

            BuildingAbstract building;
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = assembly.GetType($"PuertoRicoSpace.{name}");
            building = (BuildingAbstract)Activator.CreateInstance(type);

            building.SetJson(
                name:name, 
                industry: industry, 
                type: buildingType, 
                scale: scale, 
                worker: worker, 
                maxworker: maxworker, 
                cost: cost, score: 
                score, level: level, 
                priority: priority
                );
            return building;
        }

        public override void Write(Utf8JsonWriter writer, List<BuildingAbstract> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var buildings in value)
            {
                writer.WriteStartObject();
                writer.WriteString("Name", buildings.Name);
                writer.WriteString("Industry", buildings.Industry);
                writer.WriteString("Type", buildings.Type);
                writer.WriteString("Scale", buildings.Scale);
                writer.WriteNumber("Worker", buildings.Worker);
                writer.WriteNumber("MaxWorker", buildings.MaxWorker);
                writer.WriteNumber("Cost", buildings.Cost);
                writer.WriteNumber("Score", buildings.Score);
                writer.WriteNumber("Level", buildings.Level);
                writer.WriteNumber("Priority", buildings.Priority);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
