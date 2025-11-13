using UnityEngine;

namespace Interface
{
    public interface IFreezable
    {
        Rigidbody RigidBody { get; }
        bool CanFreeze { get; }
        void Freeze();
        
        void UnFreeze();
    }
}

