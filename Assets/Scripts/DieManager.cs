using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class DieManager
    {
        // класс для открытия меню смерти. Здесь идет обработка после смерти персонажа       
        private SceneObserver exiterScene;
        private GameObject dieUI;

        public DieManager(SceneObserver observer, GameObject dieUI)
        {
            this.dieUI = dieUI;
            exiterScene = observer;
            exiterScene.Subscribe(onSceneExit);
        }

        private void onSceneExit()
        {
            dieUI.SetActive(true);
            Time.timeScale = 0f;
        }

    }
}
