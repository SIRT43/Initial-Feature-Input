using FTGAMEStudio.InitialFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FTGAMEStudio.InitialSolution.Inputs
{
    [AddComponentMenu("Initial Solution/Inputs/Joystick"), RequireComponent(typeof(Image), typeof(CanvasRenderer))]
    public class Joystick : Outputable<JoystickInput, JoystickData>, IDragHandler, IEndDragHandler, IOutputable<JoystickData>
    {
        public RectTransform joystick;
        public RectTransform chassis;

        [Header("Smooth Damp")]
        public float smoothTime = 0.05f;

        protected JoystickData data = new();

        protected Vector3 target;
        protected Vector3 currentVelocity;

        public float Radius => Mathf.Min(chassis.rect.width, chassis.rect.height) / 2;

        protected override void Start() => SetValue(data);
        private void Update() => UpdatePosition();

        public void OnDrag(PointerEventData eventData) => UpdateJoystick(eventData.position);

        public void OnEndDrag(PointerEventData eventData)
        {
            target = Vector3.zero;
            data.Clear();
        }

        public void UpdateJoystick(Vector2 position)
        {
            Vector2 localPosition = ScreenUtils.ScreenToLocalPoi(position, chassis);

            Vector2 direction = localPosition.normalized;
            float distance = localPosition.magnitude;

            target = Vector2.ClampMagnitude(localPosition, Radius);

            data.inputPosition = position;
            data.localPosition = localPosition;
            data.direction = direction;
            data.offset = distance;
            data.normalOffset = data.offset / Radius;
        }

        public void UpdatePosition() => 
            joystick.localPosition = Vector3.SmoothDamp(joystick.localPosition, target, ref currentVelocity, smoothTime);

        protected override void OnDisable()
        {
            data.Clear();
            target = Vector3.zero;
        }

        protected override void OnDestroy() => data.Clear();
    }
}
