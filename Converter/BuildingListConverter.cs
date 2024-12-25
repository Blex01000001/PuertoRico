using PuertoRicoSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

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
            string type = null;
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
                        type = reader.GetString();
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
            if (name == "QuarryFarm")
            {
                building = new QuarryFarm();
            }
            else if (name == "CoffeeFarm")
            {
                building = new CoffeeFarm();
            }
            else if (name == "TobaccoFarm")
            {
                building = new TobaccoFarm();
            }
            else if (name == "CornFarm")
            {
                building = new CornFarm();
            }
            else if (name == "SugarFarm")
            {
                building = new SugarFarm();
            }
            else if (name == "IndigoFarm")
            {
                building = new IndigoFarm();
            }
            else if (name == "IndigoPlant(Small)")
            {
                building = new IndigoPlant_Small();
            }
            else if (name == "IndigoPlant(Large)")
            {
                building = new IndigoPlant_Large();
            }
            else if (name == "SugarMill(Small)")
            {
                building = new SugarMill_Small();
            }
            else if (name == "SugarMill(Large)")
            {
                building = new SugarMill_Large();
            }
            else if (name == "TobaccoStorage")
            {
                building = new TobaccoStorage();
            }
            else if (name == "CoffeeRoaster")
            {
                building = new CoffeeRoaster();
            }
            else if (name == "Smallmarket")
            {
                building = new Smallmarket();
            }
            else if (name == "Largemarket")
            {
                building = new Largemarket();
            }
            else if (name == "Hacienda")
            {
                building = new Hacienda();
            }
            else if (name == "Constructionhut")
            {
                building = new Constructionhut();
            }
            else if (name == "Smallwarehouse")
            {
                building = new Smallwarehouse();
            }
            else if (name == "Largewarehouse")
            {
                building = new Largewarehouse();
            }
            else if (name == "Hospice")
            {
                building = new Hospice();
            }
            else if (name == "Office")
            {
                building = new Office();
            }
            else if (name == "Factory")
            {
                building = new Factory();
            }
            else if (name == "University")
            {
                building = new University();
            }
            else if (name == "Harbor")
            {
                building = new Harbor();
            }
            else if (name == "Wharf")
            {
                building = new Wharf();
            }
            else if (name == "Guildhall")
            {
                building = new Guildhall();
            }
            else if (name == "Residence")
            {
                building = new Residence();
            }
            else if (name == "Fortress")
            {
                building = new Fortress();
            }
            else if (name == "Customshouse")
            {
                building = new Customshouse();
            }
            else if (name == "Cityhall")
            {
                building = new Cityhall();
            }
            else if (name == "PassBuilding")
            {
                building = new PassBuilding();
            }
            else
            {
                throw new JsonException($"Unknown cargo type: {name}");
            }

            building.SetJson(
                name:name, 
                industry: industry, 
                type:type, 
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
