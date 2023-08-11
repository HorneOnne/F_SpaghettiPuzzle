using UnityEngine;

namespace SpaghettiPuzzle
{
    public class UIGameplayManager : MonoBehaviour
    {
        public static UIGameplayManager Instance { get; private set; }

        public UIGameplay UIGameplay;
        public UIGameover UIGameover;
        public UIGuides UIGuides;


        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            CloseAll();
            DisplayGuidesMenu(true);
            DisplaGameplayMenu(true);
        }

        public void CloseAll()
        {
            DisplaGameplayMenu(false);
            DisplayGameoverMenu(false);
            DisplayGuidesMenu(false);
        }


        public void DisplaGameplayMenu(bool isActive)
        {
            UIGameplay.DisplayCanvas(isActive);
        }

        public void DisplayGameoverMenu(bool isActive)
        {
            UIGameover.DisplayCanvas(isActive);
        }

        public void DisplayGuidesMenu(bool isActive)
        {
            UIGuides.DisplayCanvas(isActive);
        }
    }
}
