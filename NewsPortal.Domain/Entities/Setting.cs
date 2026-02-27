using System.ComponentModel.DataAnnotations;


namespace NewsPortal.Domain.Entities
{
    public class Setting
    {
        [Key]
        public int SettingId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
