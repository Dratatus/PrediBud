namespace Backend.Data.Models.Notifications
{
    public enum NotificationStatus
    {
        OrderAccepted,                // Zlecenie zaakceptowane przez Workera
        NegotiationStarted,           // Rozpoczęcie negocjacji przez jedną ze stron
        NegotiationRejected,          // Negocjacja odrzucona przez jedną ze stron
        ContinuedNegotiation,         // Dalsza negocjacja przez którąkolwiek ze stron
        NewOrderCreated,              // Nowe zlecenie utworzone przez Klienta
        OrderCompleted                // Zlecenie zakończone przez Workera
    }
}
