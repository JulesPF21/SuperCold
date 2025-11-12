using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [Tooltip("Image component dispplaying current health")]
        public Image HealthFillImage;
        
        Health m_PlayerHealth;
        public PlayerCharacterController playerCharacterController;
        void Start()
        {
            if (playerCharacterController == null)
            {
                Debug.LogError("Pas de joueur assigné à la PlayerHealthBar !");
                return;
            }

            m_PlayerHealth = playerCharacterController.GetComponent<Health>();
            if (m_PlayerHealth == null)
            {
                Debug.LogError("Pas de composant Health sur le joueur assigné !");
                return;
            }
        }

        void Update()
        {
            // update health bar value
            HealthFillImage.fillAmount = m_PlayerHealth.CurrentHealth / m_PlayerHealth.MaxHealth;
        }
    }
}