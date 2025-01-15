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
                _ => throw new ArgumentException($"Unsupported RailingMaterial: {railingMaterial}")
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
                _ => throw new ArgumentException($"Unsupported CeilingMaterial: {ceilingMaterial}")
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
                _ => throw new ArgumentException($"Unsupported SuspendedCeilingMaterial: {suspendedCeilingMaterial}")
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
                _ => throw new ArgumentException($"Unsupported DoorMaterial: {doorMaterial}")
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
                _ => throw new ArgumentException($"Unsupported InsulationType: {insulationType}")
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
                _ => throw new ArgumentException($"Unsupported FinishMaterial: {finishMaterial}")
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
                _ => throw new ArgumentException($"Unsupported InsulationMaterial: {insulationMaterial}")
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
                _ => throw new ArgumentException($"Unsupported FlooringMaterial: {flooringMaterial}")
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
                _ => throw new ArgumentException($"Unsupported PaintType: {paintType}")
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
                _ => throw new ArgumentException($"Unsupported PlasterType: {plasterType}")
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
                _ => throw new ArgumentException($"Unsupported StaircaseMaterial: {material}")
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
                _ => throw new ArgumentException($"Unsupported LoadBearingWallMaterial: {material}")
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
                _ => throw new ArgumentException($"Unsupported PartitionWallMaterial: {material}")
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
                _ => throw new ArgumentException($"Unsupported WindowsMaterial: {material}")
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
                _ => throw new ArgumentException($"Unsupported RoofMaterial: {material}")
            };
        }

    }
}
