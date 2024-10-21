using UnityEngine;
using UnityEngine.EventSystems;

namespace Master.Scripts.Menu
{
    public class NavigationMenu : MonoBehaviour
    {
        public EventSystem EventSystem;
        public GameObject settingPanel;
        public GameObject BackButton;

        public void startGame()
        {
            // Load the first scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        public void openSetting()
        {
            settingPanel.SetActive(true);
            EventSystem.SetSelectedGameObject(BackButton);
        }
    
        public void closeSetting()
        {
            settingPanel.SetActive(false);
        }

        public void quit()
        {
            Application.Quit();
        }
    }
}