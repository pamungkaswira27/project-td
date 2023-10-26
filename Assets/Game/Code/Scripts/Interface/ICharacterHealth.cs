namespace ProjectTD
{
    public interface ICharacterHealth
    {
        public float HealthPoints { get; }
        public float MaxHealthPoints { get; }
        public void DecreaseHealth(float amount);
    }
}
