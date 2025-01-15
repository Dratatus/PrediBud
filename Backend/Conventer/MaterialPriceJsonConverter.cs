using Backend.Data.Models.MaterialPrices.Balcony;
using Backend.Data.Models.MaterialPrices.Celling;
using Backend.Data.Models.MaterialPrices.Doors;
using Backend.Data.Models.MaterialPrices.Facade;
using Backend.Data.Models.MaterialPrices.Floor;
using Backend.Data.Models.MaterialPrices.Insulation;
using Backend.Data.Models.MaterialPrices.Painting;
using Backend.Data.Models.MaterialPrices.Plastering;
using Backend.Data.Models.MaterialPrices.ShellOpen;
using Backend.Data.Models.Suppliers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Backend.Data.Models.MaterialPrices.Stairs;
using Backend.Data.Models.MaterialPrices.Windows;
using System.Reflection;

namespace Backend.Conventer
{
    public class MaterialPriceJsonConverter : JsonConverter
    {
        private static readonly Dictionary<string, Type> TypeMap = new Dictionary<string, Type>
        {
            ["Balcony"] = typeof(BalconyMaterialPrice),
            ["SuspendedCeiling"] = typeof(SuspendedCeilingMaterialPrice),
            ["Doors"] = typeof(DoorsMaterialPrice),
            ["Facade"] = typeof(FacadeMaterialPrice),
            ["Flooring"] = typeof(FlooringMaterialPrice),
            ["InsulationOfAttic"] = typeof(InsulationOfAtticMaterialPrice),
            ["Painting"] = typeof(PaintingMaterialPrice),
            ["Plastering"] = typeof(PlasteringMaterialPrice),
            ["Staircase"] = typeof(StaircaseMaterialPrice),
            ["Roof"] = typeof(RoofMaterialPrice),
            ["Ceiling"] = typeof(CeilingMaterialPrice),
            ["Chimney"] = typeof(ChimneyMaterialPrice),
            ["PartitionWall"] = typeof(PartitionWallMaterialPrice),
            ["LoadBearingWall"] = typeof(LoadBearingWallMaterialPrice),
            ["Foundation"] = typeof(FoundationMaterialPrice),
            ["VentilationSystem"] = typeof(VentilationSystemMaterialPrice),
            ["Windows"] = typeof(WindowsMaterialPrice)
        };

        public override bool CanConvert(Type objectType)
        {
            return typeof(MaterialPrice).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            var typeDiscriminator = (string?)jObject["MaterialPriceType"];
            if (string.IsNullOrEmpty(typeDiscriminator))
            {
                throw new JsonSerializationException($"Missing 'MaterialPriceType' field in JSON object: {jObject}");
            }

            if (!TypeMap.TryGetValue(typeDiscriminator, out var targetType))
            {
                throw new JsonSerializationException(
                    $"Unsupported type (MaterialPriceType): '{typeDiscriminator}'");
            }

            serializer.Converters.Remove(this);

            try
            {
                var typedObject = jObject.ToObject(targetType, serializer);
                serializer.Converters.Add(this);

                return typedObject ?? throw new JsonSerializationException(
                    $"Deserialization to type {targetType.Name} returned null");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value, serializer);

            if (value is MaterialPrice mp)
            {
                var foundEntry = TypeMap.FirstOrDefault(kv => kv.Value == value.GetType());
                if (!string.IsNullOrEmpty(foundEntry.Key))
                {
                    t["MaterialPriceType"] = foundEntry.Key;
                }
                else
                {
                    throw new JsonSerializationException(
                        $"No mapping of type {value.GetType().Name} found in MaterialPriceJsonConverter.");
                }
            }

            t.WriteTo(writer);
        }
    }
}
