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
                    return CreateWindowsSpecification(details as WindowsSpecificationDetails);

                case ConstructionType.Foundation:
                    return CreateFoundationSpecification(details as FoundationSpecificationDetails);

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
