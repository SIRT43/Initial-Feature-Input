using UnityEngine;

namespace InitialSolution.Inputs
{
    public class JoystickData
    {
        public Vector2 inputPosition;
        public Vector2 localPosition;
        public Vector2 direction;
        public float offset;
        public float normalOffset;

        public void Clear()
        {
            inputPosition = Vector2.zero;
            localPosition = Vector2.zero;
            direction = Vector2.zero;
            offset = 0;
            normalOffset = 0;
        }
    }

    [CreateAssetMenu(fileName = "New Joystick Input", menuName = "Initial Solution/Inputs/Joystick Input")]
    public class JoystickInput : Input<JoystickData> { }
}
