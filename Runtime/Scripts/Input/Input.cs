using System;
using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialSolution.Inputs
{
    public interface IOutputable<T> where T : class
    {
        void SetValue(T value);
        T GetValue();
    }

    public abstract class Input : ScriptableObject
    {
        /// <summary>
        /// Ψһ������ID��������ϵͳ��Ψһ��ʶ����  
        /// </summary>
        [SerializeField] private string inputId = Guid.NewGuid().ToString();
        [SerializeField] private string inputName = "New Input";

        public Guid InputId => new(inputId);
        public string InputName => inputName;
    }

    public abstract class Input<T> : Input, IDisposable, IOutputable<T> where T : class
    {
        private static readonly Dictionary<Guid, object> inputValues = new();

        public virtual void SetValue(T value)
        {
            if (inputValues.ContainsKey(InputId)) inputValues[InputId] = value;
            else inputValues.Add(InputId, value);
        }

        public virtual T GetValue()
        {
            inputValues.TryGetValue(InputId, out object value);
            if (value is T val) return val;

            return default;
        }

        public virtual void Dispose()
        {
            inputValues.Remove(InputId);

            GC.SuppressFinalize(this);
        }

        ~Input()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif

            Dispose();
        }
    }
}