using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager {
    public delegate void ScreenInputEvent(Vector2 pos1,Vector2 pos2);
    public class InputManager : MonoBehaviour
    {
        public ScreenInputEvent singleTouch;
        public ScreenInputEvent doubleTouch;
        public ScreenInputEvent drag;


        //TODO �ȴ��߻������뷽ʽ
        private void Update() {
            if (Input.touchCount > 0) {

            }

        }
    }
}


