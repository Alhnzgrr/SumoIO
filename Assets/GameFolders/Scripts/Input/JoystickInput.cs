using ConnectedFoods.Core;
using UnityEngine;

namespace Sumo.GamePlay
{
    public class JoystickInput : MonoSingleton<JoystickInput>
    {
        [SerializeField] private DynamicJoystick dynamicJoystick;

        public float GetHorizontal()
        {
            return dynamicJoystick.Horizontal;
        }

        public float GetVertical()
        {
            return dynamicJoystick.Vertical;
        }

        public Vector2 GetDirection()
        {
            return dynamicJoystick.Direction;
        }
    }
}
