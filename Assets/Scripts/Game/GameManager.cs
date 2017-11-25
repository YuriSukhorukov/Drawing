using UnityEngine.SceneManagement;
using Utils;

namespace Game
{
    public class GameManager : AbstractMonoSingleton<GameManager>
    {
        public string gameSceneName = "coloring_scene_reskin";
        public string menuSceneName = "menu_reskin";
        public int selectedPageId = -1;
        
        public void GoToGameScene()
        {
            SceneManager.LoadScene(gameSceneName);
        }

        public void GoToMenuScene()
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }
}
