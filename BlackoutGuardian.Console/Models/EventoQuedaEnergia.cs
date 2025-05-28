namespace BlackoutGuardian.Console.Models
{
    public class EventoQuedaEnergia : BaseEntity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        /// <summary>APP, BLE ou MEDIAPIPE</summary>
        public string Source { get; set; } = string.Empty;

        /// <summary>NOVO, CONFIRMADO ou ENCERRADO</summary>
        public string Status { get; set; } = "NOVO";
    }
}
