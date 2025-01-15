using Backend.Data.Models.Constructions.Specyfication.Ceiling;
using Backend.Data.Models.Constructions.Specyfication.Chimney;
using Backend.Data.Models.Constructions.Specyfication.Foundation;
using Backend.Data.Models.Constructions.Specyfication.Roof;
using Backend.Data.Models.Constructions.Specyfication.Ventilation;
using Backend.Data.Models.Constructions.Specyfication.Walls;

namespace Backend.Data.Models.Constructions.Specyfication.ShellOpen
{
    public class ShellOpenSpecification: ConstructionSpecification
    {
        public ShellOpenSpecification()
        {
            Type = ConstructionType.ShellOpen;
        }

        public FoundationSpecification FoundationSpecification { get; set; }
        public LoadBearingWallSpecification LoadBearingWallMaterial { get; set; }
        public PartitionWallSpecification PartitionWall { get; set; }
        public ChimneySpecification Chimney { get; set; }
        public VentilationSystemSpecification Ventilation { get; set; }
        public CeilingSpecification Celling { get; set; } 
        public RoofSpecification Roof { get; set; } 
        public string[] ImagesUrl { get; set; }  
    }
}
