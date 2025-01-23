using Backend.Conventer;
using Backend.Data.Context;
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
using Backend.Data.Models.Suppliers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.services
{
    public class SupplierService : ISupplierService
    {
        private readonly PrediBudDBContext _context;

        public SupplierService(PrediBudDBContext context)
        {
            _context = context;
        }

        public async Task UpdateSuppliersAsync(string jsonFilePath)
        {
            try
            {
                var json = await File.ReadAllTextAsync(jsonFilePath);
                var settings = new JsonSerializerSettings
                {
                    Converters = { new MaterialPriceJsonConverter() },
                };
                var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(json, settings);

                if (suppliers == null || !suppliers.Any())
                {
                    Console.WriteLine("Suppliers not found.");
                    return;
                }

                foreach (var supplier in suppliers)
                {
                    var existingSupplierLocal = _context.Suppliers.Local
                        .FirstOrDefault(s => s.Name == supplier.Name);

                    var existingSupplier = await _context.Suppliers
                        .Include(s => s.MaterialPrices)
                        .FirstOrDefaultAsync(s => s.Name == supplier.Name);

                    if (existingSupplier != null || existingSupplierLocal != null)
                    {
                        existingSupplier.Address = supplier.Address;
                        existingSupplier.ContactEmail = supplier.ContactEmail;

                        foreach (var materialPrice in supplier.MaterialPrices)
                        {
                            var existingMaterialPrice = existingSupplier.MaterialPrices
                                .FirstOrDefault(mp => mp.MaterialType == materialPrice.MaterialType &&
                                                      mp.MaterialCategory == materialPrice.MaterialCategory);

                            if (existingMaterialPrice != null)
                            {
                                UpdateMaterialPrice(existingMaterialPrice, materialPrice);
                            }
                            else
                            {
                                var newMaterialPrice = CreateMaterialPrice(materialPrice);
                                newMaterialPrice.SupplierId = existingSupplier.ID;
                                existingSupplier.MaterialPrices.Add(newMaterialPrice);
                            }
                        }
                    }
                    else
                    {
                        var newSupplier = new Supplier
                        {
                            Name = supplier.Name,
                            Address = supplier.Address,
                            ContactEmail = supplier.ContactEmail,
                            MaterialPrices = supplier.MaterialPrices.Select(mp =>
                            {
                                var newMaterialPrice = CreateMaterialPrice(mp);
                                newMaterialPrice.SupplierId = 0;
                                return newMaterialPrice;
                            }).ToList()
                        };

                        await _context.Suppliers.AddAsync(newSupplier);
                    }
                }

                await _context.SaveChangesAsync();
                Console.WriteLine("Supplier data has been updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating suppliers: {ex.Message}");
            }
        }

        private void UpdateMaterialPrice(MaterialPrice existingMaterialPrice, MaterialPrice newMaterialPrice)
        {
            switch (existingMaterialPrice)
            {
                case VentilationSystemMaterialPrice ventilationPrice when newMaterialPrice is VentilationSystemMaterialPrice newVentilationPrice:
                    ventilationPrice.PricePerUnit = newVentilationPrice.PricePerUnit;
                    break;

                case RoofMaterialPrice roofPrice when newMaterialPrice is RoofMaterialPrice newRoofPrice:
                    roofPrice.PricePerSquareMeter = newRoofPrice.PricePerSquareMeter;
                    break;

                case PartitionWallMaterialPrice partitionPrice when newMaterialPrice is PartitionWallMaterialPrice newPartitionPrice:
                    partitionPrice.PricePerSquareMeter = newPartitionPrice.PricePerSquareMeter;
                    break;

                case LoadBearingWallMaterialPrice loadBearingPrice when newMaterialPrice is LoadBearingWallMaterialPrice newLoadBearingPrice:
                    loadBearingPrice.PricePerSquareMeter = newLoadBearingPrice.PricePerSquareMeter;
                    break;

                case FoundationMaterialPrice foundationPrice when newMaterialPrice is FoundationMaterialPrice newFoundationPrice:
                    foundationPrice.PricePerCubicMeter = newFoundationPrice.PricePerCubicMeter;
                    break;  

                case ChimneyMaterialPrice chimneyMaterialPrice when newMaterialPrice is ChimneyMaterialPrice newFoundationPrice:
                    chimneyMaterialPrice.PricePerCubicMeter = newFoundationPrice.PricePerCubicMeter;
                    break;
                     
                case CeilingMaterialPrice ceilingMaterialPrice when newMaterialPrice is CeilingMaterialPrice newFoundationPrice:
                    ceilingMaterialPrice.PricePerSquareMeter = newFoundationPrice.PricePerSquareMeter;
                    break;

                case PlasteringMaterialPrice plasteringPrice when newMaterialPrice is PlasteringMaterialPrice newPlasteringPrice:
                    plasteringPrice.PricePerSquareMeter = newPlasteringPrice.PricePerSquareMeter;
                    break;

                case PaintingMaterialPrice paintingPrice when newMaterialPrice is PaintingMaterialPrice newPaintingPrice:
                    paintingPrice.PricePerLiter = newPaintingPrice.PricePerLiter;
                    paintingPrice.CoveragePerLiter = newPaintingPrice.CoveragePerLiter;
                    break;

                case InsulationOfAtticMaterialPrice insulationPrice when newMaterialPrice is InsulationOfAtticMaterialPrice newInsulationPrice:
                    insulationPrice.PricePerSquareMeter = newInsulationPrice.PricePerSquareMeter;
                    insulationPrice.Thickness = newInsulationPrice.Thickness;
                    break;

                case FlooringMaterialPrice flooringPrice when newMaterialPrice is FlooringMaterialPrice newFlooringPrice:
                    flooringPrice.PricePerSquareMeter = newFlooringPrice.PricePerSquareMeter;
                    break;

                case FacadeMaterialPrice facadePrice when newMaterialPrice is FacadeMaterialPrice newFacadePrice:
                    facadePrice.Thickness = newFacadePrice.Thickness;
                    facadePrice.PricePerSquareMeter = newFacadePrice.PricePerSquareMeter;
                    facadePrice.PricePerSquareMeterFinish = newFacadePrice.PricePerSquareMeterFinish;
                    break;

                case DoorsMaterialPrice doorsPrice when newMaterialPrice is DoorsMaterialPrice newDoorsPrice:
                    doorsPrice.Height = newDoorsPrice.Height;
                    doorsPrice.Width = newDoorsPrice.Width;
                    doorsPrice.PricePerDoor = newDoorsPrice.PricePerDoor;
                    break;

                case SuspendedCeilingMaterialPrice ceilingPrice when newMaterialPrice is SuspendedCeilingMaterialPrice newCeilingPrice:
                    ceilingPrice.PricePerSquareMeter = newCeilingPrice.PricePerSquareMeter;
                    ceilingPrice.MaxHeight = newCeilingPrice.MaxHeight;
                    break;

                case BalconyMaterialPrice balconyPrice when newMaterialPrice is BalconyMaterialPrice newBalconyPrice:
                    balconyPrice.Height = newBalconyPrice.Height;
                    balconyPrice.PricePerMeter = newBalconyPrice.PricePerMeter;
                    break;

                case StaircaseMaterialPrice staircaseMaterialPrice when newMaterialPrice is StaircaseMaterialPrice newStaircasePrice:
                    staircaseMaterialPrice.PricePerStep = newStaircasePrice.PricePerStep;
                    staircaseMaterialPrice.StandardStepHeight = newStaircasePrice.StandardStepHeight;
                    staircaseMaterialPrice.StandardStepWidth = newStaircasePrice.StandardStepWidth;
                    break;

                case WindowsMaterialPrice windowsMaterialPrice when newMaterialPrice is WindowsMaterialPrice newWindowsPrice:
                    windowsMaterialPrice.PricePerSquareMeter = newWindowsPrice.PricePerSquareMeter;
                    windowsMaterialPrice.StandardHeight = newWindowsPrice.StandardHeight;
                    windowsMaterialPrice.StandardWidth = newWindowsPrice.StandardWidth;
                    break;

                default:
                    throw new Exception($"Unsupported material price type: {existingMaterialPrice?.GetType().FullName ?? "null"}");
            }

            existingMaterialPrice.PriceWithoutTax = newMaterialPrice.PriceWithoutTax;
        }


        private MaterialPrice CreateMaterialPrice(MaterialPrice materialPrice)
        {
            return materialPrice switch
            {
                VentilationSystemMaterialPrice newVentilationPrice => new VentilationSystemMaterialPrice
                {
                    MaterialType = newVentilationPrice.MaterialType,
                    MaterialCategory = newVentilationPrice.MaterialCategory,
                    PriceWithoutTax = newVentilationPrice.PriceWithoutTax,
                    PricePerUnit = newVentilationPrice.PricePerUnit
                },
                RoofMaterialPrice newRoofPrice => new RoofMaterialPrice
                {
                    MaterialType = newRoofPrice.MaterialType,
                    MaterialCategory = newRoofPrice.MaterialCategory,
                    PriceWithoutTax = newRoofPrice.PriceWithoutTax,
                    PricePerSquareMeter = newRoofPrice.PricePerSquareMeter
                },
                PartitionWallMaterialPrice newPartitionPrice => new PartitionWallMaterialPrice
                {
                    MaterialType = newPartitionPrice.MaterialType,
                    MaterialCategory = newPartitionPrice.MaterialCategory,
                    PriceWithoutTax = newPartitionPrice.PriceWithoutTax,
                    PricePerSquareMeter = newPartitionPrice.PricePerSquareMeter
                },
                ChimneyMaterialPrice newChimneyMaterialPrice => new ChimneyMaterialPrice
                {
                    MaterialType = newChimneyMaterialPrice.MaterialType,
                    MaterialCategory = newChimneyMaterialPrice.MaterialCategory,
                    PriceWithoutTax = newChimneyMaterialPrice.PriceWithoutTax,
                    PricePerCubicMeter = newChimneyMaterialPrice.PricePerCubicMeter
                },
                CeilingMaterialPrice newCeilingMaterialPrice => new CeilingMaterialPrice
                {
                    MaterialType = newCeilingMaterialPrice.MaterialType,
                    MaterialCategory = newCeilingMaterialPrice.MaterialCategory,
                    PriceWithoutTax = newCeilingMaterialPrice.PriceWithoutTax,
                    PricePerSquareMeter = newCeilingMaterialPrice.PricePerSquareMeter
                },
                LoadBearingWallMaterialPrice newLoadBearingPrice => new LoadBearingWallMaterialPrice
                {
                    MaterialType = newLoadBearingPrice.MaterialType,
                    MaterialCategory = newLoadBearingPrice.MaterialCategory,
                    PriceWithoutTax = newLoadBearingPrice.PriceWithoutTax,
                    PricePerSquareMeter = newLoadBearingPrice.PricePerSquareMeter
                },
                FoundationMaterialPrice newFoundationPrice => new FoundationMaterialPrice
                {
                    MaterialType = newFoundationPrice.MaterialType,
                    MaterialCategory = newFoundationPrice.MaterialCategory,
                    PriceWithoutTax = newFoundationPrice.PriceWithoutTax,
                    PricePerCubicMeter = newFoundationPrice.PricePerCubicMeter
                },
                PlasteringMaterialPrice newPlasteringPrice => new PlasteringMaterialPrice
                {
                    MaterialType = newPlasteringPrice.MaterialType,
                    MaterialCategory = newPlasteringPrice.MaterialCategory,
                    PriceWithoutTax = newPlasteringPrice.PriceWithoutTax,
                    PricePerSquareMeter = newPlasteringPrice.PricePerSquareMeter
                },
                PaintingMaterialPrice newPaintingPrice => new PaintingMaterialPrice
                {
                    MaterialType = newPaintingPrice.MaterialType,
                    MaterialCategory = newPaintingPrice.MaterialCategory,
                    PriceWithoutTax = newPaintingPrice.PriceWithoutTax,
                    PricePerLiter = newPaintingPrice.PricePerLiter,
                    CoveragePerLiter = newPaintingPrice.CoveragePerLiter
                },
                InsulationOfAtticMaterialPrice newInsulationPrice => new InsulationOfAtticMaterialPrice
                {
                    MaterialType = newInsulationPrice.MaterialType,
                    MaterialCategory = newInsulationPrice.MaterialCategory,
                    PriceWithoutTax = newInsulationPrice.PriceWithoutTax,
                    PricePerSquareMeter = newInsulationPrice.PricePerSquareMeter,
                    Thickness = newInsulationPrice.Thickness
                },
                FlooringMaterialPrice newFlooringPrice => new FlooringMaterialPrice
                {
                    MaterialType = newFlooringPrice.MaterialType,
                    MaterialCategory = newFlooringPrice.MaterialCategory,
                    PriceWithoutTax = newFlooringPrice.PriceWithoutTax,
                    PricePerSquareMeter = newFlooringPrice.PricePerSquareMeter
                },
                FacadeMaterialPrice newFacadePrice => new FacadeMaterialPrice
                {
                    MaterialType = newFacadePrice.MaterialType,
                    MaterialCategory = newFacadePrice.MaterialCategory,
                    PriceWithoutTax = newFacadePrice.PriceWithoutTax,
                    Thickness = newFacadePrice.Thickness,
                    PricePerSquareMeter = newFacadePrice.PricePerSquareMeter,
                    PricePerSquareMeterFinish = newFacadePrice.PricePerSquareMeterFinish
                },
                DoorsMaterialPrice newDoorsPrice => new DoorsMaterialPrice
                {
                    MaterialType = newDoorsPrice.MaterialType,
                    MaterialCategory = newDoorsPrice.MaterialCategory,
                    PriceWithoutTax = newDoorsPrice.PriceWithoutTax,
                    Height = newDoorsPrice.Height,
                    Width = newDoorsPrice.Width,
                    PricePerDoor = newDoorsPrice.PricePerDoor
                },
                SuspendedCeilingMaterialPrice newCeilingPrice => new SuspendedCeilingMaterialPrice
                {
                    MaterialType = newCeilingPrice.MaterialType,
                    MaterialCategory = newCeilingPrice.MaterialCategory,
                    PriceWithoutTax = newCeilingPrice.PriceWithoutTax,
                    PricePerSquareMeter = newCeilingPrice.PricePerSquareMeter,
                    MaxHeight = newCeilingPrice.MaxHeight
                },
                BalconyMaterialPrice newBalconyPrice => new BalconyMaterialPrice
                {
                    MaterialType = newBalconyPrice.MaterialType,
                    MaterialCategory = newBalconyPrice.MaterialCategory,
                    PriceWithoutTax = newBalconyPrice.PriceWithoutTax,
                    Height = newBalconyPrice.Height,
                    PricePerMeter = newBalconyPrice.PricePerMeter
                },
                StaircaseMaterialPrice newStaircasePrice => new StaircaseMaterialPrice
                {
                    MaterialType = newStaircasePrice.MaterialType,
                    MaterialCategory = newStaircasePrice.MaterialCategory,
                    PriceWithoutTax = newStaircasePrice.PriceWithoutTax,
                    PricePerStep = newStaircasePrice.PricePerStep,
                    StandardStepHeight = newStaircasePrice.StandardStepHeight,
                    StandardStepWidth = newStaircasePrice.StandardStepWidth
                },
                WindowsMaterialPrice newWindowsPrice => new WindowsMaterialPrice
                {
                    MaterialType = newWindowsPrice.MaterialType,
                    MaterialCategory = newWindowsPrice.MaterialCategory,
                    PriceWithoutTax = newWindowsPrice.PriceWithoutTax,
                    PricePerSquareMeter = newWindowsPrice.PricePerSquareMeter,
                    StandardHeight = newWindowsPrice.StandardHeight,
                    StandardWidth = newWindowsPrice.StandardWidth
                },
                _ => throw new Exception($"Unsupported material price type: {materialPrice.GetType()}")
            };
        }
    }
}
