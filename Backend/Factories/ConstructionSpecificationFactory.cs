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
using Backend.Data.Models.Constructions.Specyfication.Chimney;
using Backend.Data.Models.Constructions.Specyfication.Roof;
using Backend.Data.Models.Constructions.Specyfication.Ventilation;
using Backend.Data.Models.Constructions.Specyfication.Walls;
using Backend.Data.Models.Constructions.Specyfication.Windows;
using Backend.Data.Models.Constructions.Specyfication.Foundation;

namespace Backend.Factories
{
    public class ConstructionSpecificationFactory : IConstructionSpecificationFactory
    {
        public ConstructionSpecification CreateSpecification(ConstructionType type, object details)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
                PropertyNameCaseInsensitive = true 
            };

            switch (type)
            {
                case ConstructionType.Balcony:
                    var balconyDetails = JsonSerializer.Deserialize<BalconySpecificationDetails>(details.ToString(), options);
                    return CreateBalconySpecification(balconyDetails);

                case ConstructionType.SuspendedCeiling:
                    var suspendedCeilingDetails = JsonSerializer.Deserialize<SuspendedCeilingSpecificationDetails>(details.ToString(), options);
                    return CreateSuspendedCeilingSpecification(suspendedCeilingDetails);

                case ConstructionType.Doors:
                    var doorsDetails = JsonSerializer.Deserialize<DoorsSpecificationDetails>(details.ToString(), options);
                    return CreateDoorsSpecification(doorsDetails);

                case ConstructionType.Facade:
                    var facadeDetails = JsonSerializer.Deserialize<FacadeSpecificationDetails>(details.ToString(), options);
                    return CreateFacadeSpecification(facadeDetails);

                case ConstructionType.Flooring:
                    var flooringDetails = JsonSerializer.Deserialize<FlooringSpecificationDetails>(details.ToString(), options);
                    return CreateFlooringSpecification(flooringDetails);

                case ConstructionType.InsulationOfAttic:
                    var insulationDetails = JsonSerializer.Deserialize<InsulationOfAtticSpecificationDetails>(details.ToString(), options);
                    return CreateInsulationOfAtticSpecification(insulationDetails);

                case ConstructionType.Painting:
                    var paintingDetails = JsonSerializer.Deserialize<PaintingSpecificationDetails>(details.ToString(), options);
                    return CreatePaintingSpecification(paintingDetails);

                case ConstructionType.Plastering:
                    var plasteringDetails = JsonSerializer.Deserialize<PlasteringSpecificationDetails>(details.ToString(), options);
                    return CreatePlasteringSpecification(plasteringDetails);

                case ConstructionType.ShellOpen:
                    var shellOpenDetails = JsonSerializer.Deserialize<ShellOpenSpecificationDetails>(details.ToString(), options);
                    return CreateShellOpenSpecification(shellOpenDetails);

                case ConstructionType.Staircase:
                    var staircaseDetails = JsonSerializer.Deserialize<StaircaseSpecificationDetails>(details.ToString(), options);
                    return CreateStaircaseSpecification(staircaseDetails);

                case ConstructionType.Windows:
                    var windowsDetails = JsonSerializer.Deserialize<WindowsSpecificationDetails>(details.ToString(), options);
                    return CreateWindowsSpecification(windowsDetails);

                case ConstructionType.Foundation:
                    var foundationDetails = JsonSerializer.Deserialize<FoundationSpecificationDetails>(details.ToString(), options);
                    return CreateFoundationSpecification(foundationDetails);

                default:
                    throw new NotSupportedException($"Construction type {type} is not supported.");
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
        private FoundationSpecification CreateFoundationSpecification(FoundationSpecificationDetails details)
        {
            if (details == null) throw new ArgumentException("Invalid details for Foundation");

            return new FoundationSpecification
            {
                Length = details.Length,
                Width = details.Width,
                Depth = details.Depth
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
                FoundationSpecification = new FoundationSpecification
                {
                    Length = details.FoundationLength,
                    Width = details.FoundationWidth,
                    Depth = details.FoundationDepth
                },
                LoadBearingWallMaterial = new LoadBearingWallSpecification
                {
                    Height = details.LoadBearingWallHeight,
                    Width = details.LoadBearingWallWidth,
                    Thickness = details.LoadBearingWallThickness,
                    Material = details.LoadBearingWallMaterial
                },
                PartitionWall = new PartitionWallSpecification
                {
                    Height = details.PartitionWallHeight,
                    Width = details.PartitionWallWidth,
                    Thickness = details.PartitionWallThickness,
                    Material = details.PartitionWallMaterial
                },
                Chimney = new ChimneySpecification
                {
                    Count = details.ChimneyCount
                },
                Ventilation = new VentilationSystemSpecification
                {
                    Count = details.VentilationSystemCount
                },
                Celling = new CeilingSpecification
                {
                    Area = details.CeilingArea,
                    Material = details.CeilingMaterial
                },
                Roof = new RoofSpecification
                {
                    Area = details.RoofArea,
                    Material = details.RoofMaterial,
                    Pitch = details.RoofPitch
                },
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
