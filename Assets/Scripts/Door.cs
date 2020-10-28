using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class Door : MonoBehaviour
    {
        [SerializeField] private Sprite[] doorOpen;
        private bool isOpened = false;
        internal void OpenDoor()
        {
            GetComponent<SpriteRenderer>().sprite = doorOpen[0];
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = doorOpen[1];
            isOpened = true;
        }
        internal void TryEnterToDoor(SceneObserver observer)
        {
            if (isOpened)
                observer.Invoke();
        }
        
    }
}
