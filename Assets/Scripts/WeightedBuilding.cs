public class WeightedBuilding : Weightable
{
    private void Start()
    {
        GetComponentInParent<BalancingContainer>().currentWeight += weight;
    }
}