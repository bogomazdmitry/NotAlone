namespace NotAlone.Models
{
    public class QueueItemModel
    {
        public LoverPropertiesModel FirstPerson { get; set; }
        
        public LoverPropertiesModel SecondPerson { get; set; }
        
        public bool IsBlindDate { get; set; }
        
        public string LinkBlindDate { get; set; }
    }
}