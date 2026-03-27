
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

// MIT License
// Copyright (c) 2025 frou01
// https://github.com/frou01/GrabAnimatorController/tree/main?tab=MIT-1-ov-file
// Mod by kikurage1989

namespace ragecraft.UtilsScript
{
    //OwnerLinkerへEnabledパラメータを追加し、アニメーター等外部から停止・作動を制御できるようにしたもの
    public class CustomOwnerLinker : UdonSharpBehaviour
    {
        [Header("アタッチされたオブジェクトのオーナーが移った際、以下に設定されたオブジェクトのオーナーも同時に置き換えるUDONです")]
        [Header("アタッチされたアニメーターにて(float) isOwnerLinkerEnabled を0.0にすることにより動作を停止できます。")]
        [Header("複数人での重連協調運転時のオーナー取り合い対策")]
        public GameObject[] objects;
        public Animator _controllerAnimator;

        private int IsOwnerLinkerEnabledParameterID; //isOwnerLinkerEnabled
        private bool isInit = false;
        void Start()
        {
            if (_controllerAnimator == null) Debug.Log("_controllerAnimator:null");
            IsOwnerLinkerEnabledParameterID = Animator.StringToHash("isOwnerLinkerEnabled");
            isInit = true;
        }
        public override void OnOwnershipTransferred(VRC.SDKBase.VRCPlayerApi player)
        {
            if (!isInit) return;
            bool isEnabled = (_controllerAnimator.GetFloat(IsOwnerLinkerEnabledParameterID) != 0.0f);
            // Debug.Log("CustomOwnerLinkerEvent:isEnabled:" + isEnabled);
            if (Networking.LocalPlayer != player || !isEnabled)
            {
                if (!isEnabled) Debug.Log("Disable CustomOwnerLinker");
                return;
            }
            for (int i = 0; i < objects.Length; i++)
            {
                Networking.SetOwner(player, objects[i]);
            }
        }

        public override void OnPickup()
        {
            if (!isInit) return;

            bool isEnabled = (_controllerAnimator.GetFloat(IsOwnerLinkerEnabledParameterID) != 0.0f);
            if(!isEnabled) return;

            Debug.Log("CustomOwnerLinker:onPicked");
            Debug.Log("CustomOwnerLinker:Networking.GetOwner(objects[0]).playerId:" + Networking.GetOwner(objects[0]).playerId + " Networking.GetOwner(this.gameObject).playerId:" + Networking.GetOwner(this.gameObject).playerId);
            if (Networking.GetOwner(objects[0]).playerId != Networking.GetOwner(this.gameObject).playerId)
            {
                Networking.SetOwner(Networking.GetOwner(this.gameObject), objects[0]);
            }

        }
    }
}