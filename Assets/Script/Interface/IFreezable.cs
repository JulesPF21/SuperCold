namespace Interface
{
    public interface IFreezable
    {
        bool CanFreeze { get; }
        void Freeze();
        
        void UnFreeze();
    }
}

