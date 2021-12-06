using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager {
    public delegate void ScreenInputEvent(Vector2 pos);
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// �����¼���
        /// </summary>

        public ScreenInputEvent singleTouch;
        /// <summary>
        /// ˫���¼���(δʵ��
        /// </summary>
        
        public ScreenInputEvent doubleTouch;
        /// <summary>
        /// �϶�����(δʵ��
        /// </summary>

        public ScreenInputEvent drag;

        private void Awake() {
            Input.multiTouchEnabled = false;
            InstanceManager.InputInstance = this;
        }

        private void Update() {
            if (Input.touchCount > 0) {
                if (Input.touches[0].phase == TouchPhase.Began) {
                    if(singleTouch!=null)
                        singleTouch(Input.touches[0].position);
                }
            }
        }
    }
}


