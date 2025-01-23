namespace Backend.Data.Models.Orders
{
    public enum OrderStatus
    {
        New,                     // Nowe zlecenie dostępne dla Workerów
        NegotiationInProgress,   // W trakcie negocjacji
        Accepted,                // Zlecenie zaakceptowane przez obie strony
        Rejected,                // Całkowicie odrzucone, wraca do statusu New
        Completed                // Zakończone przez Workera
    }
}
