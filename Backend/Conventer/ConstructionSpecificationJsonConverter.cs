using System.Text.Json;
using System.Text.Json.Serialization;
using Backend.Data.Models.Constructions;
using Backend.Data.Models.Constructions.Dimensions;
using Backend.Data.Models.Constructions.Dimensions.Balcony;
using Backend.Data.Models.Constructions.Dimensions.Doors;
using Backend.Data.Models.Constructions.Dimensions.Facade;
using Backend.Data.Models.Constructions.Dimensions.Floor;
using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Constructions.Specyfication.Ceiling;
using Backend.Data.Models.Constructions.Specyfication.Chimney;
using Backend.Data.Models.Constructions.Specyfication.Foundation;
using Backend.Data.Models.Constructions.Specyfication.Painting;
using Backend.Data.Models.Constructions.Specyfication.Plastering;
using Backend.Data.Models.Constructions.Specyfication.Roof;
using Backend.Data.Models.Constructions.Specyfication.ShellOpen;
using Backend.Data.Models.Constructions.Specyfication.Stairs;
using Backend.Data.Models.Constructions.Specyfication.Ventilation;
using Backend.Data.Models.Constructions.Specyfication.Walls;
using Backend.Data.Models.Constructions.Specyfication.Windows;
using Backend.Middlewares;

namespace Backend.Conventer
{
    public class ConstructionSpecificationJsonConverter : JsonConverter<ConstructionSpecification>
    {
        public override ConstructionSpecification Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                var jsonObject = jsonDoc.RootElement;

                if (jsonObject.TryGetProperty("Type", out var typeProperty))
                {
                    var typeString = typeProperty.GetString();

                    if (!Enum.TryParse(typeString, true, out ConstructionType type))
                    {
                        throw new ApiException($"Unsupported ConstructionType: {typeString}", StatusCodes.Status400BadRequest);
                    }

                    return type switch
                    {
                        ConstructionType.PartitionWall => JsonSerializer.Deserialize<PartitionWallSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.Foundation => JsonSerializer.Deserialize<FoundationSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.Windows => JsonSerializer.Deserialize<WindowsSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.Doors => JsonSerializer.Deserialize<DoorsSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.Facade => JsonSerializer.Deserialize<FacadeSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.Flooring => JsonSerializer.Deserialize<FlooringSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.SuspendedCeiling => JsonSerializer.Deserialize<SuspendedCeilingSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.InsulationOfAttic => JsonSerializer.Deserialize<InsulationOfAtticSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.Plastering => JsonSerializer.Deserialize<PlasteringSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.Painting => JsonSerializer.Deserialize<PaintingSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.Staircase => JsonSerializer.Deserialize<StaircaseSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.Balcony => JsonSerializer.Deserialize<BalconySpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.ShellOpen => JsonSerializer.Deserialize<ShellOpenSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.Chimney => JsonSerializer.Deserialize<ChimneySpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.LoadBearingWall => JsonSerializer.Deserialize<LoadBearingWallSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.VentilationSystem => JsonSerializer.Deserialize<VentilationSystemSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.Roof => JsonSerializer.Deserialize<RoofSpecification>(jsonObject.GetRawText(), options),
                        ConstructionType.Ceiling => JsonSerializer.Deserialize<CeilingSpecification>(jsonObject.GetRawText(), options),
                        _ => throw new ApiException($"Unsupported ConstructionType: {typeString}", StatusCodes.Status400BadRequest)
                    };
                }

                throw new ApiException("Missing required property 'Type'", StatusCodes.Status400BadRequest);
            }
        }


        public override void Write(Utf8JsonWriter writer, ConstructionSpecification value, JsonSerializerOptions options)
        {
            var type = value.Type.ToString();
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
