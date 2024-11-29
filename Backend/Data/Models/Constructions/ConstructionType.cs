namespace Backend.Data.Models.Constructions
{
    public enum ConstructionType
    {
        PartitionWall,          // Ściana działowa (prosta do wyceny na podstawie wymiarów i zdjęcia)
        Foundation,             // Fundamenty (podstawowa konstrukcja, prosta do wyceny)
        Windows,                // Montaż okien
        Doors,                  // Montaż drzwi
        Facade,                 // Elewacja (docieplenie i wykończenie zewnętrzne)
        Flooring,               // Podłoga (np. panele, parkiet – łatwo policzyć na podstawie powierzchni)
        SuspendedCeiling,       // Sufit podwieszany
        InsulationOfAttic,      // Ocieplenie poddasza
        Plastering,             // Tynkowanie ścian
        Painting,               // Malowanie ścian i sufitów
        Staircase,              // Konstrukcja schodów
        Balcony,                 // Konstrukcja balkonu
        ShellOpen
    }
}
