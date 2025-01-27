using Backend.Data.Consts;
using Backend.Data.Context;
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
using Backend.Data.Models.MaterialPrices.Balcony;
using Backend.Data.Models.MaterialPrices.Celling;
using Backend.Data.Models.MaterialPrices.Doors;
using Backend.Data.Models.MaterialPrices.Facade;
using Backend.Data.Models.MaterialPrices.Floor;
using Backend.Data.Models.MaterialPrices.Insulation;
using Backend.Data.Models.MaterialPrices.Painting;
using Backend.Data.Models.MaterialPrices.Plastering;
using Backend.Data.Models.MaterialPrices.ShellOpen;
using Backend.Data.Models.MaterialPrices.Stairs;
using Backend.Data.Models.MaterialPrices.Windows;
using Backend.Data.Models.Price;
using Backend.Data.Models.Suppliers;
using Backend.Middlewares;
using Backend.services.Calculator.Mappers;
using Backend.Validatiors.Calculator;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend.services.Calculator
{
    public class CalculatorService : ICalculatorService
    {
        private readonly PrediBudDBContext _context;

        public CalculatorService(PrediBudDBContext context)
        {
            _context = context;
        }

        public async Task<CalculatedPrice> CalculatePriceAsync(ConstructionSpecification specification)
        {
            switch (specification)
            {
                case BalconySpecification balconySpecification:
                    return await CalculateBalconyPriceAsync(balconySpecification);

                case CeilingSpecification ceilingSpecification:
                    return await CalculateCeilingPriceAsync(ceilingSpecification);

                case SuspendedCeilingSpecification suspendedCeilingSpecification:
                    return await CalculateSuspendedCeilingPriceAsync(suspendedCeilingSpecification);

                case ChimneySpecification chimneySpecification:
                    return await CalculateChimneyPriceAsync(chimneySpecification);

                case DoorsSpecification doorsSpecification:
                    return await CalculateDoorsPriceAsync(doorsSpecification);

                case FacadeSpecification facadeSpecification:
                    return await CalculateFacadePriceAsync(facadeSpecification);

                case FlooringSpecification flooringSpecification:
                    return await CalculateFlooringPriceAsync(flooringSpecification);

                case FoundationSpecification foundationSpecification:
                    return await CalculateFoundationPriceAsync(foundationSpecification);

                case InsulationOfAtticSpecification insulationSpecification:
                    return await CalculateInsulationOfAtticPriceAsync(insulationSpecification);

                case PaintingSpecification paintingSpecification:
                    return await CalculatePaintingPriceAsync(paintingSpecification);

                case PlasteringSpecification plasteringSpecification:
                    return await CalculatePlasteringPriceAsync(plasteringSpecification);

                case StaircaseSpecification staircaseSpecification:
                    return await CalculateStaircasePriceAsync(staircaseSpecification);

                case VentilationSystemSpecification ventilationSystemSpecification:
                    return await CalculateVentilationSystemPriceAsync(ventilationSystemSpecification);

                case LoadBearingWallSpecification loadBearingWallSpecification:
                    return await CalculateLoadBearingWallPriceAsync(loadBearingWallSpecification);

                case PartitionWallSpecification partitionWallSpecification:
                    return await CalculatePartitionWallPriceAsync(partitionWallSpecification);

                case WindowsSpecification windowsSpecification:
                    return await CalculateWindowsPriceAsync(windowsSpecification);

                case ShellOpenSpecification shellOpenSpecification:
                    return await CalculateShellOpenPriceAsync(shellOpenSpecification);

                case RoofSpecification roofSpecification:
                    return await CalculateRoofPriceAsync(roofSpecification);

                default:
                    throw new ApiException($"Specification type {specification.GetType()} is not supported.", StatusCodes.Status401Unauthorized);
            }
        }

        private async Task<CalculatedPrice> CalculateBalconyPriceAsync(BalconySpecification spec)
        {
            BalconySpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapRailingMaterialToMaterialType(spec.RailingMaterial);
            var materialPrice = await _context.MaterialPrices
           .OfType<BalconyMaterialPrice>()
           .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);


            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.MaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var perimeter = 2 * (spec.Length + spec.Width);
            var priceWithoutTax = perimeter * materialPrice.PricePerMeter;
            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }

        private async Task<CalculatedPrice> CalculateCeilingPriceAsync(CeilingSpecification spec)
        {
            CeilingSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapCeilingMaterialToMaterialType(spec.Material);
            var materialPrice = await _context.MaterialPrices
                .OfType<CeilingMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.MaterialPriceNotFound, StatusCodes.Status404NotFound);
            }


            var priceWithoutTax = spec.Area * materialPrice.PricePerSquareMeter;
            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }

        private async Task<CalculatedPrice> CalculateSuspendedCeilingPriceAsync(SuspendedCeilingSpecification spec)
        {
            SuspendedCeilingSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapSuspendedCeilingMaterialToMaterialType(spec.Material);
            var materialPrice = await _context.MaterialPrices
                .OfType<SuspendedCeilingMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.MaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var priceWithoutTax = spec.Area * materialPrice.PricePerSquareMeter;
            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }
        private async Task<CalculatedPrice> CalculateChimneyPriceAsync(ChimneySpecification spec)
        {
            ChimneySpecificationValidator.Validate(spec);

            var materialPrice = await _context.MaterialPrices
                .OfType<ChimneyMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.MaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var priceWithoutTax = spec.Count * materialPrice.PricePerCubicMeter;
            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }

        private async Task<CalculatedPrice> CalculateDoorsPriceAsync(DoorsSpecification spec)
        {
            DoorsSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapDoorMaterialToMaterialType(spec.Material);

            var materialPrices = await _context.MaterialPrices
                .OfType<DoorsMaterialPrice>()
                .Where(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type)
                .ToListAsync();

            if (materialPrices == null || !materialPrices.Any())
            {
                throw new ApiException(ErrorMessages.DoorsMaterialPricesNotFound, StatusCodes.Status404NotFound);
            }

            var closestPrice = materialPrices
                .OrderBy(mp => Math.Abs(mp.Height - spec.Height)
                             + Math.Abs(mp.Width - spec.Width))
                .FirstOrDefault();

            if (closestPrice == null)
            {
                throw new ApiException(ErrorMessages.ClosestMaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var heightDifference = (spec.Height - closestPrice.Height);
            var widthDifference = (spec.Width - closestPrice.Width);

            var baseArea = closestPrice.Height * closestPrice.Width;
            var userArea = spec.Height * spec.Width;

            var priceAdjustmentFactor = userArea / baseArea;
            var adjustedPricePerDoor = closestPrice.PricePerDoor * priceAdjustmentFactor;

            var priceWithoutTax = spec.Amount * adjustedPricePerDoor;
            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }

        private async Task<CalculatedPrice> CalculateFacadePriceAsync(FacadeSpecification spec)
        {
            FacadeSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapInsulationTypeFacadeToMaterialType(spec.InsulationType);

            var insulationPrice = await _context.MaterialPrices
                .OfType<FacadeMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            var finishMaterialType = MaterialTypeMapper.MapFinishMaterialFacadeToMaterialType(spec.FinishMaterial);

            var finishPrice = await _context.MaterialPrices
                .OfType<FacadeMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == finishMaterialType && mp.MaterialCategory == spec.Type);

            if (insulationPrice == null)
            {
                throw new ApiException(ErrorMessages.InsulationPriceNotFound, StatusCodes.Status404NotFound);
            }

            if (finishPrice == null)
            {
                throw new ApiException(ErrorMessages.FinishPriceNotFound, StatusCodes.Status404NotFound);
            }

            var insulationCostWithoutTax = spec.SurfaceArea * insulationPrice.PricePerSquareMeter;
            var insulationCostWithTax = insulationCostWithoutTax * 1.23m;

            var finishCostWithoutTax = spec.SurfaceArea * finishPrice.PricePerSquareMeterFinish;
            var finishCostWithTax = finishCostWithoutTax * 1.23m;

            var totalCostWithoutTax = insulationCostWithoutTax + finishCostWithoutTax;
            var totalCostWithTax = insulationCostWithTax + finishCostWithTax;

            return new CalculatedPrice
            {
                PriceWithoutTax = totalCostWithoutTax,
                PriceWithTax = totalCostWithTax
            };
        }

        private async Task<CalculatedPrice> CalculateFlooringPriceAsync(FlooringSpecification spec)
        {
            FlooringSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapFlooringMaterialToMaterialType(spec.Material);

            var materialPrice = await _context.MaterialPrices
                .OfType<FlooringMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.FlooringMaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var costWithoutTax = spec.Area * materialPrice.PricePerSquareMeter;
            var costWithTax = costWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = costWithoutTax,
                PriceWithTax = costWithTax
            };
        }

        private async Task<CalculatedPrice> CalculateFoundationPriceAsync(FoundationSpecification spec)
        {
            FoundationSpecificationValidator.Validate(spec);

            var materialPrice = await _context.MaterialPrices
                .OfType<FoundationMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == MaterialType.Concrete && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.FoundationMaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var volume = spec.Length * spec.Width * spec.Depth;

            var costWithoutTax = volume * materialPrice.PricePerCubicMeter;
            var costWithTax = costWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = costWithoutTax,
                PriceWithTax = costWithTax
            };
        }

        private async Task<CalculatedPrice> CalculateInsulationOfAtticPriceAsync(InsulationOfAtticSpecification spec)
        {
            InsulationOfAtticSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapInsulationInsulationOfAtticMaterialToMaterialType(spec.Material);

            var materialPrices = await _context.MaterialPrices
                .OfType<InsulationOfAtticMaterialPrice>()
                .Where(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type)
                .ToListAsync();

            if (!materialPrices.Any())
            {
                throw new ApiException(ErrorMessages.InsulationMaterialPricesNotFound, StatusCodes.Status404NotFound);
            }

            var closestPrice = materialPrices
                .OrderBy(mp => Math.Abs(mp.Thickness - spec.Thickness))
                .FirstOrDefault();

            if (closestPrice == null)
            {
                throw new ApiException(ErrorMessages.ClosestInsulationMaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var priceAdjustmentFactor = spec.Thickness / closestPrice.Thickness;

            var priceWithoutTax = spec.Area * closestPrice.PricePerSquareMeter * priceAdjustmentFactor;
            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }

        private async Task<CalculatedPrice> CalculatePaintingPriceAsync(PaintingSpecification spec)
        {
            PaintingSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapPaintTypeToMaterialType(spec.PaintType);

            var materialPrice = await _context.MaterialPrices
                .OfType<PaintingMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.PaintingMaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var totalPaintRequired = (spec.WallSurfaceArea * spec.NumberOfCoats) / materialPrice.CoveragePerLiter;

            var priceWithoutTax = totalPaintRequired * materialPrice.PricePerLiter;

            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }

        private async Task<CalculatedPrice> CalculatePlasteringPriceAsync(PlasteringSpecification spec)
        {
            PlasteringSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapPlasterTypeToMaterialType(spec.PlasterType);

            var materialPrice = await _context.MaterialPrices
                .OfType<PlasteringMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.PlasteringMaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var priceWithoutTax = spec.WallSurfaceArea * materialPrice.PricePerSquareMeter;

            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }

        private async Task<CalculatedPrice> CalculateStaircasePriceAsync(StaircaseSpecification spec)
        {
            StaircaseSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapStaircaseMaterialToMaterialType(spec.Material);

            var materialPrice = await _context.MaterialPrices
                .OfType<StaircaseMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.StaircaseMaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var heightFactor = materialPrice.StandardStepHeight.HasValue
                ? spec.Height / materialPrice.StandardStepHeight.Value
                : 1m;

            var widthFactor = materialPrice.StandardStepWidth.HasValue
                ? spec.Width / materialPrice.StandardStepWidth.Value
                : 1m;

            var adjustedPricePerStep = materialPrice.PricePerStep * heightFactor * widthFactor;

            var priceWithoutTax = spec.NumberOfSteps * adjustedPricePerStep;
            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }

        private async Task<CalculatedPrice> CalculateVentilationSystemPriceAsync(VentilationSystemSpecification spec)
        {
            VentilationSystemSpecificationValidator.Validate(spec);

            var materialPrice = await _context.MaterialPrices
                .OfType<VentilationSystemMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.VentilationSystemMaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var priceWithoutTax = spec.Count * materialPrice.PricePerUnit;
            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }

        private async Task<CalculatedPrice> CalculateLoadBearingWallPriceAsync(LoadBearingWallSpecification spec)
        {
            LoadBearingWallSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapLoadBearingWallMaterialToMaterialType(spec.Material);

            var materialPrice = await _context.MaterialPrices
                .OfType<LoadBearingWallMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.LoadBearingWallMaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var wallArea = spec.Height * spec.Width;

            var priceWithoutTax = wallArea * materialPrice.PricePerSquareMeter;
            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }

        private async Task<CalculatedPrice> CalculatePartitionWallPriceAsync(PartitionWallSpecification spec)
        {
            PartitionWallSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapPartitionWallMaterialToMaterialType(spec.Material);

            var materialPrice = await _context.MaterialPrices
                .OfType<PartitionWallMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.PartitionWallMaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var wallArea = spec.Height.Value * spec.Width.Value;

            var priceWithoutTax = wallArea * materialPrice.PricePerSquareMeter;
            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }

        private async Task<CalculatedPrice> CalculateWindowsPriceAsync(WindowsSpecification spec)
        {
            WindowsSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapWindowsMaterialToMaterialType(spec.Material);

            var materialPrices = await _context.MaterialPrices
                .OfType<WindowsMaterialPrice>()
                .Where(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type)
                .ToListAsync();

            if (!materialPrices.Any())
            {
                throw new ApiException(ErrorMessages.WindowsMaterialPricesNotFound, StatusCodes.Status404NotFound);
            }

            var closestPrice = materialPrices
                .OrderBy(mp => Math.Abs(mp.StandardHeight - spec.Height) + Math.Abs(mp.StandardWidth - spec.Width))
                .FirstOrDefault();

            if (closestPrice == null)
            {
                throw new ApiException(ErrorMessages.ClosestMaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var standardArea = closestPrice.StandardHeight * closestPrice.StandardWidth;
            var customArea = spec.Height * spec.Width;
            var areaDifference = customArea - standardArea;

            var basePriceWithoutTax = closestPrice.PricePerSquareMeter * standardArea;
            var additionalCost = areaDifference > 0
                ? closestPrice.PricePerSquareMeter * areaDifference
                : 0;

            var priceWithoutTax = (basePriceWithoutTax + additionalCost) * spec.Amount;
            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }

        private async Task<CalculatedPrice> CalculateRoofPriceAsync(RoofSpecification spec)
        {
            RoofSpecificationValidator.Validate(spec);

            var materialType = MaterialTypeMapper.MapRoofMaterialToMaterialType(spec.Material);

            var materialPrice = await _context.MaterialPrices
                .OfType<RoofMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.RoofMaterialPriceNotFound, StatusCodes.Status404NotFound);
            }

            var pitchFactor = 1 + (decimal)Math.Tan((double)(spec.Pitch) * Math.PI / 180.0);
            var effectiveArea = spec.Area * pitchFactor;

            var priceWithoutTax = effectiveArea * materialPrice.PricePerSquareMeter;
            var priceWithTax = priceWithoutTax * 1.23m;

            return new CalculatedPrice
            {
                PriceWithoutTax = priceWithoutTax,
                PriceWithTax = priceWithTax
            };
        }


        private async Task<CalculatedPrice> CalculateShellOpenPriceAsync(ShellOpenSpecification spec)
        {
            ShellOpenSpecificationValidator.Validate(spec);

            decimal totalPriceWithoutTax = 0;
            decimal totalPriceWithTax = 0;

            var foundationPrice = await CalculateFoundationPriceAsync(spec.FoundationSpecification);
            totalPriceWithoutTax += foundationPrice.PriceWithoutTax;
            totalPriceWithTax += foundationPrice.PriceWithTax;

            var loadBearingWallPrice = await CalculateLoadBearingWallPriceAsync(spec.LoadBearingWallMaterial);
            totalPriceWithoutTax += loadBearingWallPrice.PriceWithoutTax;
            totalPriceWithTax += loadBearingWallPrice.PriceWithTax;

            var partitionWallPrice = await CalculatePartitionWallPriceAsync(spec.PartitionWall);
            totalPriceWithoutTax += partitionWallPrice.PriceWithoutTax;
            totalPriceWithTax += partitionWallPrice.PriceWithTax;

            var chimneyPrice = await CalculateChimneyPriceAsync(spec.Chimney);
            totalPriceWithoutTax += chimneyPrice.PriceWithoutTax;
            totalPriceWithTax += chimneyPrice.PriceWithTax;

            var ventilationPrice = await CalculateVentilationSystemPriceAsync(spec.Ventilation);
            totalPriceWithoutTax += ventilationPrice.PriceWithoutTax;
            totalPriceWithTax += ventilationPrice.PriceWithTax;

            var ceilingPrice = await CalculateCeilingPriceAsync(spec.Celling);
            totalPriceWithoutTax += ceilingPrice.PriceWithoutTax;
            totalPriceWithTax += ceilingPrice.PriceWithTax;

            var roofPrice = await CalculateRoofPriceAsync(spec.Roof);
            totalPriceWithoutTax += roofPrice.PriceWithoutTax;
            totalPriceWithTax += roofPrice.PriceWithTax;

            return new CalculatedPrice
            {
                PriceWithoutTax = totalPriceWithoutTax,
                PriceWithTax = totalPriceWithTax
            };
        }

    }
}

