using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3;
using VRC.Udon.Common.Interfaces;
using VRC.SDK3.UdonNetworkCalling;

namespace ragecraft.UtilsScript
{
    public class InterectSendCustomEvent : UdonSharpBehaviour
    {
        [SerializeField] private UdonBehaviour targetUdon;
        [SerializeField] private string customEventName;

        public override void Interact()
        {
            // Debug.Log(customEventName);
            targetUdon.SendCustomEvent(customEventName);//こっちはコンパイル通るし動作もする。
        }
    }
}