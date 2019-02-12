using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Na
{
    public class UIManager : MonoBehaviour
    {
        [Serializable]
        public class MoveEvent : UnityEvent<DIRECTION> { }

        public MoveEvent moveEvent;
        public UnityEvent gotoMenuEvent;
        public UnityEvent gotoPlayEvent;

        public void OnR_Button()
        {
            moveEvent?.Invoke(DIRECTION.RIGHT);
        }

        public void OnL_Button()
        {
            moveEvent?.Invoke(DIRECTION.LEFT);
        }

        public void OnU_Button()
        {
            moveEvent?.Invoke(DIRECTION.UP);
        }

        public void OnD_Button()
        {
            moveEvent?.Invoke(DIRECTION.DOWN);
        }

        public void OnGotoMenu_Button()
        {
            gotoMenuEvent?.Invoke();
        }

        public void OnPlay_Button()
        {
            gotoPlayEvent?.Invoke();
        }
    }
}