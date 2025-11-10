using UnityEngine;

public class MultiDisplayManager : MonoBehaviour
{
   public Camera cameraPlayer1;
   public Camera cameraPlayer2;
      void Start()
      {
         int count = Display.displays.Length;
         Debug.Log("Nombre d’écrans détectés : " + count);
         
         if (count > 1)
         {
            Display.displays[1].Activate();
            cameraPlayer1.targetDisplay = 0;
            cameraPlayer2.targetDisplay = 1;
            Debug.Log("Display 2 activé !");
         }
         else
         {
            cameraPlayer1.rect = new Rect(0f, 0f, 0.5f, 1f);
            cameraPlayer2.rect = new Rect(0.5f, 0f, 0.5f, 1f);
            Debug.LogWarning("Un seul écran détecté — split-screen activé à la place.");
         }
      }
   }


