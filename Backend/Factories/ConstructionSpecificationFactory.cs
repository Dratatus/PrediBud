﻿using Backend.Data.Models.Constructions.Dimensions.Balcony;
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
using Backend.Validatiors.ConstructionOrder.Specyfication;
using Backend.Data.Consts;
using Backend.Middlewares;
using Backend.DTO.Specyfication.Backend.DTO.Specifications;
using Backend.Validatiors.Orders.Specyfication;

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

                case ConstructionType.PartitionWall:
                    return CreatePartitionWallSpecification(JsonSerializer.Deserialize<PartitionWallSpecificationDetails>(details.ToString(), options));

                case ConstructionType.LoadBearingWall:
                    return CreateLoadBearingWallSpecification(JsonSerializer.Deserialize<LoadBearingWallSpecificationDetails>(details.ToString(), options));

                case ConstructionType.VentilationSystem:
                    return CreateVentilationSystemSpecification(JsonSerializer.Deserialize<VentilationSystemSpecificationDetails>(details.ToString(), options));

                case ConstructionType.Roof:
                    return CreateRoofSpecification(JsonSerializer.Deserialize<RoofSpecificationDetails>(details.ToString(), options));

                case ConstructionType.Ceiling:
                    return CreateCeilingSpecification(JsonSerializer.Deserialize<CeilingSpecificationDetails>(details.ToString(), options));

                case ConstructionType.Chimney:
                    return CreateChimneySpecification(JsonSerializer.Deserialize<ChimneySpecificationDetails>(details.ToString(), options));

                default:
                    throw new ApiException($"Construction type {type} is not supported.", StatusCodes.Status400BadRequest);
            }
        }

        private BalconySpecification CreateBalconySpecification(BalconySpecificationDetails details)
        {
            BalconySpecificationValidator.Validate(details);

            return new BalconySpecification
            {
                Length = details.Length,
                Width = details.Width,
                RailingMaterial = details.RailingMaterial.Value
            };
        }

        private SuspendedCeilingSpecification CreateSuspendedCeilingSpecification(SuspendedCeilingSpecificationDetails details)
        {
            SuspendedCeilingSpecificationValidator.Validate(details);

            return new SuspendedCeilingSpecification
            {
                Area = details.Area,
                Height = details.Height,
                Material = details.Material.Value
            };
        }

        private DoorsSpecification CreateDoorsSpecification(DoorsSpecificationDetails details)
        {
            DoorsSpecificationValidator.Validate(details);

            return new DoorsSpecification
            {
                Amount = details.Amount,
                Height = details.Height,
                Width = details.Width,
                Material = details.Material.Value
            };
        }

        private FacadeSpecification CreateFacadeSpecification(FacadeSpecificationDetails details)
        {
            FacadeSpecificationValidator.Validate(details);

            return new FacadeSpecification
            {
                SurfaceArea = details.SurfaceArea,
                InsulationType = details.InsulationType.Value,
                FinishMaterial = details.FinishMaterial.Value
            };
        }

        private FlooringSpecification CreateFlooringSpecification(FlooringSpecificationDetails details)
        {
            FlooringSpecificationValidator.Validate(details);

            return new FlooringSpecification
            {
                Area = details.Area,
                Material = details.Material.Value
            };
        }
        private FoundationSpecification CreateFoundationSpecification(FoundationSpecificationDetails details)
        {
            FoundationSpecificationValidator.Validate(details);

            return new FoundationSpecification
            {
                Length = details.Length,
                Width = details.Width,
                Depth = details.Depth
            };
        }

        private InsulationOfAtticSpecification CreateInsulationOfAtticSpecification(InsulationOfAtticSpecificationDetails details)
        {
            InsulationOfAtticSpecificationValidator.Validate(details);

            return new InsulationOfAtticSpecification
            {
                Area = details.Area,
                Material = details.Material.Value,
                Thickness = details.Thickness
            };
        }

        private PaintingSpecification CreatePaintingSpecification(PaintingSpecificationDetails details)
        {
            PaintingSpecificationValidator.Validate(details);

            return new PaintingSpecification
            {
                WallSurfaceArea = details.WallSurfaceArea,
                PaintType = details.PaintType.Value,
                NumberOfCoats = details.NumberOfCoats
            };
        }

        private PlasteringSpecification CreatePlasteringSpecification(PlasteringSpecificationDetails details)
        {
            PlasteringSpecificationValidator.Validate(details);

            return new PlasteringSpecification
            {
                WallSurfaceArea = details.WallSurfaceArea,
                PlasterType = details.PlasterType.Value
            };
        }

        private ShellOpenSpecification CreateShellOpenSpecification(ShellOpenSpecificationDetails details)
        {
            ShellOpenSpecificationValidator.Validate(details);

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
                    Material = details.LoadBearingWallMaterial.Value
                },
                PartitionWall = new PartitionWallSpecification
                {
                    Height = details.PartitionWallHeight,
                    Width = details.PartitionWallWidth,
                    Thickness = details.PartitionWallThickness,
                    Material = details.PartitionWallMaterial.Value
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
                    Material = details.CeilingMaterial.Value
                },
                Roof = new RoofSpecification
                {
                    Area = details.RoofArea,
                    Material = details.RoofMaterial.Value,
                    Pitch = details.RoofPitch
                },
                ImagesUrl = details.ImagesUrl
            };
        }

        private StaircaseSpecification CreateStaircaseSpecification(StaircaseSpecificationDetails details)
        {
            StaircaseSpecificationValidator.Validate(details);

            return new StaircaseSpecification
            {
                NumberOfSteps = details.NumberOfSteps,
                Height = details.Height,
                Width = details.Width,
                Material = details.Material.Value
            };
        }

        private WindowsSpecification CreateWindowsSpecification(WindowsSpecificationDetails details)
        {
            WindowsSpecificationValidator.Validate(details);

            return new WindowsSpecification
            {
                Amount = details.Amount,
                Height = details.Height,
                Width = details.Width,
                Material = details.Material.Value
            };
        }

        private PartitionWallSpecification CreatePartitionWallSpecification(PartitionWallSpecificationDetails details)
        {
            PartitionWallSpecificationValidator.Validate(details);

            return new PartitionWallSpecification
            {
                Height = details.Height,
                Width = details.Width,
                Thickness = details.Thickness,
                Material = details.Material.Value
            };
        }

        private LoadBearingWallSpecification CreateLoadBearingWallSpecification(LoadBearingWallSpecificationDetails details)
        {
            LoadBearingWallSpecificationValidator.Validate(details);

            return new LoadBearingWallSpecification
            {
                Height = details.Height,
                Width = details.Width,
                Thickness = details.Thickness,
                Material = details.Material.Value
            };
        }

        private VentilationSystemSpecification CreateVentilationSystemSpecification(VentilationSystemSpecificationDetails details)
        {
            VentilationSystemSpecificationValidator.Validate(details);

            return new VentilationSystemSpecification
            {
                Count = details.Count
            };
        }

        private RoofSpecification CreateRoofSpecification(RoofSpecificationDetails details)
        {
            RoofSpecificationValidator.Validate(details);

            return new RoofSpecification
            {
                Area = details.Area,
                Pitch = details.Pitch,
                Material = details.Material.Value
            };
        }

        private CeilingSpecification CreateCeilingSpecification(CeilingSpecificationDetails details)
        {
            CeilingSpecificationValidator.Validate(details);

            return new CeilingSpecification
            {
                Area = details.Area,
                Material = details.Material.Value
            };
        }

        private ChimneySpecification CreateChimneySpecification(ChimneySpecificationDetails details)
        {
            ChimneySpecificationValidator.Validate(details);

            return new ChimneySpecification
            {
                Count = details.Count
            };
        }

    }
}
