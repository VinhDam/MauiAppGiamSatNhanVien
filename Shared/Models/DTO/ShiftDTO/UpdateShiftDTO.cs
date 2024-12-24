namespace Shared.Models.DTO.ShiftDTO
{
    public class UpdateShiftDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeOnly StartHour { get; set; }
        public TimeOnly EndHour { get; set; }
        public bool Status { get; set; }
    }
}
