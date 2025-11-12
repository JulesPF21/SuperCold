using UnityEngine;
using Unity.FPS.Game; 


namespace Unity.FPS.Gameplay
{
    public class Projectile : MonoBehaviour
    {
        
        [Header("Paramètres généraux")]
        public float life = 3f;
        public float speed = 20f;
        public float damage = 25f;
        
        
        public GameObject impactVFX; 
        public float impactVFXLifetime = 2f;
    
        private Vector3 m_Velocity;
    
        private void Awake()
        {
            // Détruire la balle après un certain temps
            Destroy(gameObject, life);
        }
    
        private void OnEnable()
        {
            // Calculer la direction initiale
            m_Velocity = transform.forward * speed;
        }
    
        void Update()
        {
            // Déplacement linéaire de la balle
            transform.position += m_Velocity * Time.deltaTime;
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            Damageable target = collision.collider.GetComponent<Damageable>();
            if (target != null)
            {
                target.InflictDamage(damage, false, gameObject);
            }
    
            // Effet visuel d’impact (optionnel)
            if (impactVFX != null)
            {
                GameObject vfx = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
                Destroy(vfx, impactVFXLifetime);
            }
    
            // Détruire la balle après impact
            Destroy(gameObject);
        }
}


}