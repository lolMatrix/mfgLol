using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class SceneObserver
    {

        public delegate void onDie();
        onDie dieScript;

        public void Subscribe(onDie script)
        {
            dieScript += script;
        }

        public void Invoke()
        {
            dieScript?.Invoke();
        }

    }
}
