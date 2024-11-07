using Backend.Data.Models.Constructions.Dimensions.Balcony;
using Backend.Data.Models.Constructions.Specyfication.Ceiling;
using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Constructions;
using Backend.Data.Models.Constructions.Dimensions.Doors;
using Backend.Data.Models.Constructions.Dimensions.Facade;
using Backend.Data.Models.Constructions.Dimensions.Floor;
using Backend.Data.Models.Constructions.Dimensions;
using Backend.Data.Models.Constructions.Specyfication.Painting;
using Backend.Data.Models.Constructions.Specyfication.Plastering;
using Backend.Data.Models.Constructions.Specyfication.ShellOpen;
using Backend.Data.Models.Constructions.Specyfication.Stairs;
using Backend.DTO.Specyfication;
using System.Text.Json;

namespace Backend.Factories
{
    public class ConstructionSpecificationFactory : IConstructionSpecificationFactory
    {
        public ConstructionSpecification CreateSpecification(ConstructionType type, object details)
        {
            switch (type)
            {
                case ConstructionType.Balcony:
                    return CreateBalconySpecification(details as BalconySpecificationDetails);

                case ConstructionType.SuspendedCeiling:
                    return CreateSuspendedCeilingSpecification(details as SuspendedCeilingSpecificationDetails);

                case ConstructionType.Doors:
                    return CreateDoorsSpecification(details as DoorsSpecificationDetails);

                case ConstructionType.Facade:
                    return CreateFacadeSpecification(details as FacadeSpecificationDetails);

                case ConstructionType.Flooring:
                    return CreateFlooringSpecification(details as FlooringSpecificationDetails);

                case ConstructionType.InsulationOfAttic:
                    return CreateInsulationOfAtticSpecification(details as InsulationOfAtticSpecificationDetails);

                case ConstructionType.Painting:
                    return CreatePaintingSpecification(details as PaintingSpecificationDetails);

                case ConstructionType.Plastering:
                    return CreatePlasteringSpecification(details as PlasteringSpecificationDetails);

                case ConstructionType.ShellOpen:
                    return CreateShellOpenSpecification(details as ShellOpenSpecificationDetails);

                case ConstructionType.Staircase:
                    return CreateStaircaseSpecification(details as StaircaseSpecificationDetails);

                case ConstructionType.Windows:

                    var windowsDetails = JsonSerializer.Deserialize<WindowsSpecificationDetails>(details.ToString());
                    return CreateWindowsSpecification(windowsDetails);

                default:
                    throw new ArgumentException("Invalid construction type");
            }
        }

        private BalconySpecification CreateBalconySpecification(BalconySpecificationDetails details)
        {
            if (details == null) throw new ArgumentException("Invalid details for Balcony");

            return new BalconySpecification
            {
                Length = details.Length,
                Width = details.Width,
                RailingMaterial = details.RailingMaterial
            };
        }

        private SuspendedCeilingSpecification CreateSuspendedCeilingSpecification(SuspendedCeilingSpecificationDetails details)
        {
            if (details == null) throw new ArgumentException("Invalid details for Suspended Ceiling");

            return new SuspendedCeilingSpecification
            {
                Area = details.Area,
                Height = details.Height,
                Material = details.Material
            };
        }

        private DoorsSpecification CreateDoorsSpecification(DoorsSpecificationDetails details)
        {
            if (details == null) throw new ArgumentException("Invalid details for Doors");

            return new DoorsSpecification
            {
                Amount = details.Amount,
                Height = details.Height,
                Width = details.Width,
                Material = details.Material
            };
        }

        private FacadeSpecification CreateFacadeSpecification(FacadeSpecificationDetails details)
        {
            if (details == null) throw new ArgumentException("Invalid details for Facade");

            return new FacadeSpecification
            {
                SurfaceArea = details.SurfaceArea,
                InsulationType = details.InsulationType,
                FinishMaterial = details.FinishMaterial
            };
        }

        private FlooringSpecification CreateFlooringSpecification(FlooringSpecificationDetails details)
        {
            if (details == null) throw new ArgumentException("Invalid details for Flooring");

            return new FlooringSpecification
            {
                Area = details.Area,
                Material = details.Material
            };
        }

        private InsulationOfAtticSpecification CreateInsulationOfAtticSpecification(InsulationOfAtticSpecificationDetails details)
        {
            if (details == null) throw new ArgumentException("Invalid details for Insulation of Attic");

            return new InsulationOfAtticSpecification
            {
                Area = details.Area,
                Material = details.Material,
                Thickness = details.Thickness
            };
        }

        private PaintingSpecification CreatePaintingSpecification(PaintingSpecificationDetails details)
        {
            if (details == null) throw new ArgumentException("Invalid details for Painting");

            return new PaintingSpecification
            {
                WallSurfaceArea = details.WallSurfaceArea,
                PaintType = details.PaintType,
                NumberOfCoats = details.NumberOfCoats
            };
        }

        private PlasteringSpecification CreatePlasteringSpecification(PlasteringSpecificationDetails details)
        {
            if (details == null) throw new ArgumentException("Invalid details for Plastering");

            return new PlasteringSpecification
            {
                WallSurfaceArea = details.WallSurfaceArea,
                PlasterType = details.PlasterType
            };
        }

        private ShellOpenSpecification CreateShellOpenSpecification(ShellOpenSpecificationDetails details)
        {
            if (details == null) throw new ArgumentException("Invalid details for Shell Open");

            return new ShellOpenSpecification
            {
                FoundationLength = details.FoundationLength,
                FoundationWidth = details.FoundationWidth,
                FoundationDepth = details.FoundationDepth,
                LoadBearingWallHeight = details.LoadBearingWallHeight,
                LoadBearingWallWidth = details.LoadBearingWallWidth,
                LoadBearingWallThickness = details.LoadBearingWallThickness,
                LoadBearingWallMaterial = details.LoadBearingWallMaterial,
                PartitionWallHeight = details.PartitionWallHeight,
                PartitionWallWidth = details.PartitionWallWidth,
                PartitionWallThickness = details.PartitionWallThickness,
                PartitionWallMaterial = details.PartitionWallMaterial,
                ChimneyCount = details.ChimneyCount,
                VentilationSystemCount = details.VentilationSystemCount,
                CeilingArea = details.CeilingArea,
                CeilingMaterial = details.CeilingMaterial,
                RoofArea = details.RoofArea,
                RoofMaterial = details.RoofMaterial,
                RoofPitch = details.RoofPitch,
                ImagesUrl = details.ImagesUrl
            };
        }

        private StaircaseSpecification CreateStaircaseSpecification(StaircaseSpecificationDetails details)
        {
            if (details == null) throw new ArgumentException("Invalid details for Staircase");

            return new StaircaseSpecification
            {
                NumberOfSteps = details.NumberOfSteps,
                Height = details.Height,
                Width = details.Width,
                Material = details.Material
            };
        }

        private WindowsSpecification CreateWindowsSpecification(WindowsSpecificationDetails details)
        {
            if (details == null) throw new ArgumentException("Invalid details for Windows");

            return new WindowsSpecification
            {
                Amount = details.Amount,
                Height = details.Height,
                Width = details.Width
            };
        }
    }
}
