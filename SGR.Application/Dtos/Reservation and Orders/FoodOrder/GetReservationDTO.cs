namespace SGR.Application.Dtos.Reservation_and_Orders
{
    public record GetReservationDTO
    {
        public int IdReservation { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public GetReservationDTO() { }

        public GetReservationDTO(int idReservation, int customerId, int restaurantId, DateTime reservationDate, string status, DateTime createdAt)
        {
            IdReservation = idReservation;
            CustomerId = customerId;
            RestaurantId = restaurantId;
            ReservationDate = reservationDate;
            Status = status;
            CreatedAt = createdAt;
        }
    }
}
