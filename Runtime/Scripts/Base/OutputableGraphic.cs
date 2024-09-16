using UnityEngine;
using UnityEngine.UI;

namespace InitialSolution.Inputs
{
    [RequireComponent(typeof(CanvasRenderer))]
    public abstract class OutputableGraphic<TData> : MaskableGraphic, IOutputable<TData>
        where TData : class
    {
        public abstract void SetValue(TData value);
        public abstract TData GetValue();

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            material = null;
            color = Color.clear;
        }
#endif
    }

    [RequireComponent(typeof(CanvasRenderer))]
    public abstract class OutputableGraphic<TInput, TData> : OutputableGraphic<TData>
        where TInput : Input<TData>
        where TData : class
    {
        public TInput output;

        public override void SetValue(TData value) => output?.SetValue(value);
        public override TData GetValue() => output?.GetValue();
    }
}
