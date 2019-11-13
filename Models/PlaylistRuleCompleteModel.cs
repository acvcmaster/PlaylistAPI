namespace PlaylistAPI.Models
{
    public class PlaylistRuleCompleteModel : BaseModel
    {
        public string Property { get; set; }
        public string PropertyDescription { get; set; }
        public string PropertyType { get; set; }
        public string Operator { get; set; }
        public string OperatorDescription { get; set; }
        public string Data { get; set; }
    }
}