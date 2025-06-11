namespace Application.Features.UserAddress.Queries
{
    public class GetAddressByUserIdViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string AddressDetail { get; set; }
        public bool IsDefault { get; set; }
    }
}