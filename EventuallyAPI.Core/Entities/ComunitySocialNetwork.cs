namespace EventuallyAPI.Core.Entities
{
    public class ComunitySocialNetwork
    {
        public int SocialNetworkId { get; set; }
        public SocialNetwork SocialNetwork { get; set; }
        public Comunity 
            Comunity{ get; set; }
    public int ComunityId{ get; set; }
        public string Value { get; set; }
    }
}
