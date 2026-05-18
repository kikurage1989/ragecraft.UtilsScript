
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace ragecraft.UtilsScript
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class syncAnimeSwMomentary : syncSW_Base
    {
        protected override void Start()
        {
            if(useAnimator)
            {
                if (setParameterName == null || setParameterName == "")
                {
                    Debug.LogError("syncAnimeSwMomentary:setParameterName:null");
                    return;
                }
                if (setParameterAnimator == null)
                {
                    Debug.LogError("syncAnimeSwMomentary:setParameterAnimator:null");
                    return;
                }
                setParameterNameID = Animator.StringToHash(setParameterName);
            }
            base.Start();
        }

        //Useした瞬間と離した瞬間のみ発火
        public override void InputUse(bool value, VRC.Udon.Common.UdonInputEventArgs args)
        {
            if (!isInit || !Networking.IsOwner(this.gameObject)) return;
            // Debug.Log("testMomentary:" + value);
            // inputUseState = value;

            if (udonSyncedBool[0] && !value)
            {
                udonSyncedBool[0] = false;
                #if UNITY_EDITOR
                State = udonSyncedBool[0];
                #endif
                RequestSerialization();
                SomeUpdate();
            }
        }

        protected override void SomeUpdate()
        {
            //なにかUdonSynced変数更新に付随した処理
            base.SomeUpdate();
            if(useAnimator)
            {
                int i;
                for (i = 0; i < setParameterAnimator.Length; i++)
                {
                    setParameterAnimator[i].SetBool(setParameterNameID, udonSyncedBool[0]);
                }
            }
        }

        protected override void ChangeUdonSyncedValue()
        {
            udonSyncedBool[0] = true;
            #if UNITY_EDITOR
            State = udonSyncedBool[0];
            #endif
        }  
    }
}