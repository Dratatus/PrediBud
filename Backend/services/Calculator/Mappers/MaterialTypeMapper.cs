using Backend.Data.Models.Constructions.Dimensions.Balcony;
using Backend.Data.Models.Constructions.Dimensions.Doors;
using Backend.Data.Models.Constructions.Dimensions.Facade;
using Backend.Data.Models.Constructions.Dimensions.Floor;
using Backend.Data.Models.Constructions.Specyfication.Ceiling;
using Backend.Data.Models.Constructions.Specyfication.Insulation;
using Backend.Data.Models.Constructions.Specyfication.Painting;
using Backend.Data.Models.Constructions.Specyfication.Plastering;
using Backend.Data.Models.Constructions.Specyfication.Roof;
using Backend.Data.Models.Constructions.Specyfication.Stairs;
using Backend.Data.Models.Constructions.Specyfication.Walls;
using Backend.Data.Models.Constructions.Specyfication.Windows;
using Backend.Data.Models.Suppliers;
using Backend.Middlewares;

namespace Backend.services.Calculator.Mappers
{
    public static class MaterialTypeMapper
    {
        public static MaterialType MapRailingMaterialToMaterialType(RailingMaterial railingMaterial)
        {
            return railingMaterial switch
            {
                RailingMaterial.Steel => MaterialType.Steel,
                RailingMaterial.Wood => MaterialType.Wood,
                RailingMaterial.Glass => MaterialType.Glass,
                RailingMaterial.Aluminum => MaterialType.Aluminum,
                RailingMaterial.WroughtIron => MaterialType.WroughtIron,
                _ => throw new ApiException($"Unsupported RailingMaterial: {railingMaterial}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapCeilingMaterialToMaterialType(CeilingMaterial ceilingMaterial)
        {
            return ceilingMaterial switch
            {
                CeilingMaterial.Concrete => MaterialType.Concrete,
                CeilingMaterial.Wood => MaterialType.Wood,
                CeilingMaterial.Steel => MaterialType.Steel,
                CeilingMaterial.Composite => MaterialType.Composite,
                CeilingMaterial.PrefabricatedConcrete => MaterialType.PrefabricatedConcrete,
                _ => throw new ApiException($"Unsupported CeilingMaterial: {ceilingMaterial}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapSuspendedCeilingMaterialToMaterialType(SuspendedCeilingMaterial suspendedCeilingMaterial)
        {
            return suspendedCeilingMaterial switch
            {
                SuspendedCeilingMaterial.Drywall => MaterialType.Drywall,
                SuspendedCeilingMaterial.MineralFiber => MaterialType.MineralFiber,
                SuspendedCeilingMaterial.Metal => MaterialType.Metal,
                SuspendedCeilingMaterial.PVC => MaterialType.PVC,
                SuspendedCeilingMaterial.Wood => MaterialType.Wood,
                SuspendedCeilingMaterial.GlassFiber => MaterialType.GlassFiber,
                SuspendedCeilingMaterial.Composite => MaterialType.Composite,
                _ => throw new ApiException($"Unsupported SuspendedCeilingMaterial: {suspendedCeilingMaterial}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapDoorMaterialToMaterialType(DoorMaterial doorMaterial)
        {
            return doorMaterial switch
            {
                DoorMaterial.Wood => MaterialType.Wood,
                DoorMaterial.Steel => MaterialType.Steel,
                DoorMaterial.PVC => MaterialType.PVC,
                DoorMaterial.Aluminum => MaterialType.Aluminum,
                DoorMaterial.Glass => MaterialType.Glass,
                _ => throw new ApiException($"Unsupported DoorMaterial: {doorMaterial}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapInsulationTypeFacadeToMaterialType(InsulationType insulationType)
        {
            return insulationType switch
            {
                InsulationType.Styrofoam => MaterialType.Styrofoam,
                InsulationType.MineralWool => MaterialType.MineralWool,
                InsulationType.PolyurethaneFoam => MaterialType.PolyurethaneFoam,
                InsulationType.Fiberglass => MaterialType.Fiberglass,
                _ => throw new ApiException($"Unsupported InsulationType: {insulationType}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapFinishMaterialFacadeToMaterialType(FinishMaterial finishMaterial)
        {
            return finishMaterial switch
            {
                FinishMaterial.Plaster => MaterialType.Plaster,
                FinishMaterial.Brick => MaterialType.Brick,
                FinishMaterial.Stone => MaterialType.Stone,
                FinishMaterial.Wood => MaterialType.Wood,
                FinishMaterial.MetalSiding => MaterialType.MetalSiding,
                _ => throw new ApiException($"Unsupported FinishMaterial: {finishMaterial}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapInsulationInsulationOfAtticMaterialToMaterialType(InsulationMaterial insulationMaterial)
        {
            return insulationMaterial switch
            {
                InsulationMaterial.MineralWool => MaterialType.MineralWool,
                InsulationMaterial.Styrofoam => MaterialType.Styrofoam,
                InsulationMaterial.PolyurethaneFoam => MaterialType.PolyurethaneFoam,
                InsulationMaterial.Cellulose => MaterialType.Cellulose,
                InsulationMaterial.Fiberglass => MaterialType.Fiberglass,
                InsulationMaterial.RockWool => MaterialType.RockWool,
                _ => throw new ApiException($"Unsupported InsulationMaterial: {insulationMaterial}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapFlooringMaterialToMaterialType(FlooringMaterial flooringMaterial)
        {
            return flooringMaterial switch
            {
                FlooringMaterial.Laminate => MaterialType.Laminate,
                FlooringMaterial.Hardwood => MaterialType.Hardwood,
                FlooringMaterial.Vinyl => MaterialType.Vinyl,
                FlooringMaterial.Tile => MaterialType.Tile,
                FlooringMaterial.Carpet => MaterialType.Carpet,
                _ => throw new ApiException($"Unsupported FlooringMaterial: {flooringMaterial}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapPaintTypeToMaterialType(PaintType paintType)
        {
            return paintType switch
            {
                PaintType.Acrylic => MaterialType.Acrylic,
                PaintType.Latex => MaterialType.Latex,
                PaintType.OilBased => MaterialType.OilBased,
                PaintType.WaterBased => MaterialType.WaterBased,
                PaintType.Epoxy => MaterialType.Epoxy,
                PaintType.Enamel => MaterialType.Enamel,
                PaintType.Chalk => MaterialType.Chalk,
                PaintType.Matte => MaterialType.Matte,
                PaintType.Satin => MaterialType.Satin,
                PaintType.Glossy => MaterialType.Glossy,
                _ => throw new ApiException($"Unsupported PaintType: {paintType}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapPlasterTypeToMaterialType(PlasterType plasterType)
        {
            return plasterType switch
            {
                PlasterType.Gypsum => MaterialType.Gypsum,
                PlasterType.Cement => MaterialType.Cement,
                PlasterType.Lime => MaterialType.Lime,
                PlasterType.LimeCement => MaterialType.LimeCement,
                PlasterType.Clay => MaterialType.Clay,
                PlasterType.Acrylic => MaterialType.Acrylic,
                PlasterType.Silicone => MaterialType.Silicone,
                PlasterType.Silicate => MaterialType.Silicate,
                _ => throw new ApiException($"Unsupported PlasterType: {plasterType}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapStaircaseMaterialToMaterialType(StaircaseMaterial material)
        {
            return material switch
            {
                StaircaseMaterial.Wood => MaterialType.Wood,
                StaircaseMaterial.Metal => MaterialType.Metal,
                StaircaseMaterial.Concrete => MaterialType.Concrete,
                StaircaseMaterial.Stone => MaterialType.Stone,
                StaircaseMaterial.Glass => MaterialType.Glass,
                StaircaseMaterial.Composite => MaterialType.Composite,
                StaircaseMaterial.Marble => MaterialType.Marble,
                StaircaseMaterial.Granite => MaterialType.Granite,
                _ => throw new ApiException($"Unsupported StaircaseMaterial: {material}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapLoadBearingWallMaterialToMaterialType(LoadBearingWallMaterial material)
        {
            return material switch
            {
                LoadBearingWallMaterial.Concrete => MaterialType.Concrete,
                LoadBearingWallMaterial.Brick => MaterialType.Brick,
                LoadBearingWallMaterial.AeratedConcrete => MaterialType.AeratedConcrete,
                LoadBearingWallMaterial.Stone => MaterialType.Stone,
                LoadBearingWallMaterial.Wood => MaterialType.Wood,
                _ => throw new ApiException($"Unsupported LoadBearingWallMaterial: {material}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapPartitionWallMaterialToMaterialType(PartitionWallMaterial material)
        {
            return material switch
            {
                PartitionWallMaterial.Drywall => MaterialType.Drywall,
                PartitionWallMaterial.Brick => MaterialType.Brick,
                PartitionWallMaterial.AeratedConcrete => MaterialType.AeratedConcrete,
                PartitionWallMaterial.Wood => MaterialType.Wood,
                PartitionWallMaterial.Glass => MaterialType.Glass,
                _ => throw new ApiException($"Unsupported PartitionWallMaterial: {material}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapWindowsMaterialToMaterialType(WindowsMaterial material)
        {
            return material switch
            {
                WindowsMaterial.Wood => MaterialType.Wood,
                WindowsMaterial.PVC => MaterialType.PVC,
                WindowsMaterial.Aluminum => MaterialType.Aluminum,
                WindowsMaterial.Steel => MaterialType.Steel,
                WindowsMaterial.Composite => MaterialType.Composite,
                _ => throw new ApiException($"Unsupported WindowsMaterial: {material}", StatusCodes.Status400BadRequest)
            };
        }

        public static MaterialType MapRoofMaterialToMaterialType(RoofMaterial material)
        {
            return material switch
            {
                RoofMaterial.Tile => MaterialType.Tile,
                RoofMaterial.MetalSheet => MaterialType.MetalSheet,
                RoofMaterial.AsphaltShingle => MaterialType.AsphaltShingle,
                RoofMaterial.Thatch => MaterialType.Thatch,
                RoofMaterial.Slate => MaterialType.Slate,
                RoofMaterial.PVC => MaterialType.PVC,
                RoofMaterial.Composite => MaterialType.Composite,
                _ => throw new ApiException($"Unsupported RoofMaterial: {material}", StatusCodes.Status400BadRequest)
            };
        }

    }
}
