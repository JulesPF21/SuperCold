namespace Interface
{
    public interface IFlammable
    {
        bool canBurn { get; }
        int TimeToBurn { get; }
        
        void StartBurning();
        void StopBurning();
        
        void SetBurnedProgress(float progress);
    }
}
