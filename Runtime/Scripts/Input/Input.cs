using System;
using System.Collections.Generic;
using UnityEngine;

namespace InitialSolution.Inputs
{
    public interface IOutputable
    {
        void SetValue(object value);
        object GetValue();
    }
    
    public interface IOutputable<T>
    {
        void SetValue(T value);
        T GetValue();
    }

    public abstract class Input : ScriptableObject, IOutputable
    {
        private static readonly Dictionary<Guid, object> inputValues = new();

        public virtual void SetValue(object value)
        {
            if (inputValues.ContainsKey(InputId)) inputValues[InputId] = value;
            else inputValues.Add(InputId, value);
        }

        public virtual object GetValue()
        {
            inputValues.TryGetValue(InputId, out object value);
            return value;
        }



        [SerializeField] private string inputId = Guid.NewGuid().ToString();
        [SerializeField] private string inputName = "New Input";

        public Guid InputId => new(inputId);
        public string InputName => inputName;
    }

    public abstract class Input<T> : Input, IOutputable<T>
    {
        public virtual void SetValue(T value) => SetValue(value);

        public new virtual T GetValue()
        {
            if (base.GetValue() is T value) return value;
            else return default;
        }
    }
}