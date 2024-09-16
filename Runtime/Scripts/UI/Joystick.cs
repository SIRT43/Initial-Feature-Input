using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InitialSolution.Inputs
{
    [AddComponentMenu("Initial Solution/Inputs/Joystick"), RequireComponent(typeof(Image), typeof(CanvasRenderer))]
    public class Joystick : Outputable<JoystickInput, JoystickData>, IDragHandler, IEndDragHandler, IOutputable<JoystickData>
    {
        public RectTransform joystick;
        public RectTransform chassis;

        [Space]
        [Min(0)] public float lerpT = 0.05f;



        protected JoystickData data = new();
        protected Vector2 target = Vector2.zero;

        public float Radius => Mathf.Min(chassis.rect.width, chassis.rect.height) / 2;



        protected override void Start() => SetValue(data);

        private void Update() => UpdatePosition();
        public void UpdatePosition() => joystick.localPosition = Vector2.Lerp(joystick.localPosition, target, lerpT * Time.deltaTime);


        public void OnDrag(PointerEventData eventData) => UpdateJoystick(eventData.position);

        public void OnEndDrag(PointerEventData eventData)
        {
            target = Vector3.zero;
            data.Clear();
        }

        public void UpdateJoystick(Vector2 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(chassis, position, null, out Vector2 localPosition);

            target = Vector2.ClampMagnitude(localPosition, Radius);

            data.inputPosition = position;
            data.localPosition = localPosition;
            data.direction = localPosition.normalized;
            data.offset = localPosition.magnitude;
            data.normalOffset = data.offset / Radius;
        }


        protected override void OnDisable()
        {
            data.Clear();
            target = Vector2.zero;
        }
        protected override void OnDestroy() => data.Clear();
    }
}
