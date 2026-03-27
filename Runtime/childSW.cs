
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace ragecraft.UtilsScript
{
    //Manualに固定する
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    //親に指定したスイッチのインタラクトを叩くだけ
    public class childSW : UdonSharpBehaviour
    {
        [SerializeField] private syncAnimeSW parentSW;
        void Start()
        {
            if(parentSW == null) Debug.Log(this.gameObject.name + ":parentSW:null");
        }
        public override void Interact()
        {
            // Debug.Log(this.gameObject.name + ":Interact");
            parentSW.SendCustomEvent(nameof(parentSW.InteractSW));
        }
    }
}