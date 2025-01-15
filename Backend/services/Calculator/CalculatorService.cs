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
using Backend.services.Calculator.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Backend.services.Calculator
{
    public class CalculatorService: ICalculatorService
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
                    throw new NotSupportedException($"Specification type {specification.GetType()} is not supported.");
            }
        }

        private async Task<CalculatedPrice> CalculateBalconyPriceAsync(BalconySpecification spec)
        {
            var materialType = MaterialTypeMapper.MapRailingMaterialToMaterialType(spec.RailingMaterial);
            var materialPrice = await _context.MaterialPrices
           .OfType<BalconyMaterialPrice>()
           .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);


            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given type and material.");
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
            var materialType = MaterialTypeMapper.MapCeilingMaterialToMaterialType(spec.Material);
            var materialPrice = await _context.MaterialPrices
                .OfType<CeilingMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given type and material.");
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
            var materialType = MaterialTypeMapper.MapSuspendedCeilingMaterialToMaterialType(spec.Material);
            var materialPrice = await _context.MaterialPrices
                .OfType<SuspendedCeilingMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given type and material.");
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
            if (spec.Count <= 0)
            {
                throw new Exception("Invalid chimney count provided.");
            }

            var materialPrice = await _context.MaterialPrices
                .OfType<ChimneyMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given chimney type.");
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

            if (spec.Amount <= 0 || spec.Height <= 0 || spec.Width <= 0)
            {
                throw new Exception("Invalid doors specification provided.");
            }

            var materialType = MaterialTypeMapper.MapDoorMaterialToMaterialType(spec.Material);

            var materialPrices = await _context.MaterialPrices
                .OfType<DoorsMaterialPrice>()
                .Where(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type)
                .ToListAsync();

            if (materialPrices == null || !materialPrices.Any())
            {
                throw new Exception("Material prices not found for the given door type and material.");
            }

            var closestPrice = materialPrices
                .OrderBy(mp => Math.Abs(mp.Height - spec.Height)
                             + Math.Abs(mp.Width - spec.Width))
                .FirstOrDefault();


            if (closestPrice == null)
            {
                throw new Exception("Closest material price not found for the given dimensions.");
            }

            var heightDifference = (spec.Height - closestPrice.Height);
            var widthDifference = (spec.Width - closestPrice.Width);

            var baseArea = closestPrice.Height * closestPrice.Width;
            var userArea = spec.Height * spec.Width;

            if (baseArea == 0 || userArea == 0)
            {
                throw new Exception("Invalid area calculation for doors.");
            }

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
            if (spec.SurfaceArea <= 0)
            {
                throw new Exception("Invalid facade surface area provided.");
            }
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
                throw new Exception("Insulation price not found for the given type.");
            }

            if (finishPrice == null)
            {
                throw new Exception("Finish price not found for the given type.");
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
            if (spec.Area <= 0)
            {
                throw new Exception("Invalid flooring area provided.");
            }
            var materialType = MaterialTypeMapper.MapFlooringMaterialToMaterialType(spec.Material);

            var materialPrice = await _context.MaterialPrices
                .OfType<FlooringMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given flooring material.");
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
            if (spec.Length <= 0 || spec.Width <= 0 || spec.Depth <= 0)
            {
                throw new Exception("Invalid foundation dimensions provided.");
            }

            var materialPrice = await _context.MaterialPrices
                .OfType<FoundationMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == MaterialType.Concrete && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given foundation type.");
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
            if (spec.Area <= 0 || spec.Thickness <= 0)
            {
                throw new Exception("Invalid insulation specification provided.");
            }

            var materialType = MaterialTypeMapper.MapInsulationInsulationOfAtticMaterialToMaterialType(spec.Material);

            var materialPrices = await _context.MaterialPrices
                .OfType<InsulationOfAtticMaterialPrice>()
                .Where(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type)
                .ToListAsync();

            if (!materialPrices.Any())
            {
                throw new Exception("No material prices found for the given insulation type and material.");
            }

            var closestPrice = materialPrices
                .OrderBy(mp => Math.Abs(mp.Thickness - spec.Thickness))
                .FirstOrDefault();

            if (closestPrice == null)
            {
                throw new Exception("No suitable material price found for the given insulation type and thickness.");
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
            if (spec.WallSurfaceArea <= 0 || spec.NumberOfCoats <= 0)
            {
                throw new Exception("Invalid painting specification provided.");
            }

            var materialType = MaterialTypeMapper.MapPaintTypeToMaterialType(spec.PaintType);

            var materialPrice = await _context.MaterialPrices
                .OfType<PaintingMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given paint type.");
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
            if (spec.WallSurfaceArea <= 0)
            {
                throw new Exception("Invalid plastering specification provided.");
            }

            var materialType = MaterialTypeMapper.MapPlasterTypeToMaterialType(spec.PlasterType);

            var materialPrice = await _context.MaterialPrices
                .OfType<PlasteringMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given plaster type.");
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
            if (spec.NumberOfSteps <= 0 || spec.Height <= 0 || spec.Width <= 0)
            {
                throw new Exception("Invalid staircase specification provided.");
            }

            var materialType = MaterialTypeMapper.MapStaircaseMaterialToMaterialType(spec.Material);

            var materialPrice = await _context.MaterialPrices
                .OfType<StaircaseMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given staircase material.");
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
            if (spec.Count <= 0)
            {
                throw new Exception("Invalid ventilation system specification: Count must be greater than 0.");
            }

            var materialPrice = await _context.MaterialPrices
                .OfType<VentilationSystemMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given ventilation system type.");
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
            if (spec.Height <= 0 || spec.Width <= 0 || spec.Thickness <= 0)
            {
                throw new Exception("Invalid load-bearing wall specification: Dimensions must be greater than 0.");
            }

            var materialType = MaterialTypeMapper.MapLoadBearingWallMaterialToMaterialType(spec.Material);

            var materialPrice = await _context.MaterialPrices
                .OfType<LoadBearingWallMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given wall type and material.");
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
            if (spec.Height <= 0 || spec.Width <= 0 || spec.Thickness <= 0)
            {
                throw new Exception("Invalid partition wall specification: Dimensions must be greater than 0.");
            }
            var materialType = MaterialTypeMapper.MapPartitionWallMaterialToMaterialType(spec.Material);

            var materialPrice = await _context.MaterialPrices
                .OfType<PartitionWallMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given partition wall type and material.");
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
            if (spec.Amount <= 0 || spec.Height <= 0 || spec.Width <= 0)
            {
                throw new Exception("Invalid windows specification: Dimensions and amount must be greater than 0.");
            }
            var materialType = MaterialTypeMapper.MapWindowsMaterialToMaterialType(spec.Material);

            var materialPrices = await _context.MaterialPrices
                .OfType<WindowsMaterialPrice>()
                .Where(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type)
                .ToListAsync();

            if (!materialPrices.Any())
            {
                throw new Exception("Material prices not found for the given window type and material.");
            }

            var closestPrice = materialPrices
                .OrderBy(mp => Math.Abs(mp.StandardHeight - spec.Height) + Math.Abs(mp.StandardWidth - spec.Width))
                .FirstOrDefault();

            if (closestPrice == null)
            {
                throw new Exception("No suitable material price found for the given dimensions.");
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
            if (spec.Area <= 0)
            {
                throw new Exception("Invalid roof specification: Area must be greater than zero.");
            }

            var materialType = MaterialTypeMapper.MapRoofMaterialToMaterialType(spec.Material);

            var materialPrice = await _context.MaterialPrices
                .OfType<RoofMaterialPrice>()
                .FirstOrDefaultAsync(mp => mp.MaterialType == materialType && mp.MaterialCategory == spec.Type);

            if (materialPrice == null)
            {
                throw new Exception("Material price not found for the given roof type and material.");
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
            decimal totalPriceWithoutTax = 0;
            decimal totalPriceWithTax = 0;

            if (spec.FoundationSpecification != null)
            {
                var foundationPrice = await CalculateFoundationPriceAsync(spec.FoundationSpecification);
                totalPriceWithoutTax += foundationPrice.PriceWithoutTax;
                totalPriceWithTax += foundationPrice.PriceWithTax;
            }

            if (spec.LoadBearingWallMaterial != null)
            {
                var loadBearingWallPrice = await CalculateLoadBearingWallPriceAsync(spec.LoadBearingWallMaterial);
                totalPriceWithoutTax += loadBearingWallPrice.PriceWithoutTax;
                totalPriceWithTax += loadBearingWallPrice.PriceWithTax;
            }

            if (spec.PartitionWall != null)
            {
                var partitionWallPrice = await CalculatePartitionWallPriceAsync(spec.PartitionWall);
                totalPriceWithoutTax += partitionWallPrice.PriceWithoutTax;
                totalPriceWithTax += partitionWallPrice.PriceWithTax;
            }

            if (spec.Chimney != null)
            {
                var chimneyPrice = await CalculateChimneyPriceAsync(spec.Chimney);
                totalPriceWithoutTax += chimneyPrice.PriceWithoutTax;
                totalPriceWithTax += chimneyPrice.PriceWithTax;
            }

            if (spec.Ventilation != null)
            {
                var ventilationPrice = await CalculateVentilationSystemPriceAsync(spec.Ventilation);
                totalPriceWithoutTax += ventilationPrice.PriceWithoutTax;
                totalPriceWithTax += ventilationPrice.PriceWithTax;
            }

            if (spec.Celling != null)
            {
                var ceilingPrice = await CalculateCeilingPriceAsync(spec.Celling);
                totalPriceWithoutTax += ceilingPrice.PriceWithoutTax;
                totalPriceWithTax += ceilingPrice.PriceWithTax;
            }

            if (spec.Roof != null)
            {
                var roofPrice = await CalculateRoofPriceAsync(spec.Roof);
                totalPriceWithoutTax += roofPrice.PriceWithoutTax;
                totalPriceWithTax += roofPrice.PriceWithTax;
            }

            return new CalculatedPrice
            {
                PriceWithoutTax = totalPriceWithoutTax,
                PriceWithTax = totalPriceWithTax
            };
        }

    }
}

