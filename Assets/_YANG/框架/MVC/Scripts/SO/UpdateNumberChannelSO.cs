using System;
using UnityEngine;

namespace Yang.MVC
{
    [CreateAssetMenu(menuName = "MVC/Event/UpdateNumber Channel")]
    public class UpdateNumberChannelSO : ScriptableObject
    {
        public Action<MainModelSO> OnEventRaised;

        public void Raise(MainModelSO model)
        {
            OnEventRaised?.Invoke(model);
        }
    }
}