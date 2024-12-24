namespace API.Models.DTO.ShiftDTO
{
    public class ShiftDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeOnly StartHour { get; set; }
        public TimeOnly EndHour { get; set; }
        public bool Status { get; set; }
    }
}
