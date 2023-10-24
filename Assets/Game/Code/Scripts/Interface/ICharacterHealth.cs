namespace ProjectTD
{
    public interface ICharacterHealth
    {
        public float HealthPoints { get; set; }
        public float MaxHealthPoints { get; }
        public void DecreaseHealth(float amount);
    }
}
