
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace ragecraft.UtilsScript
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class syncAnimeSwMomentary : UdonSharpBehaviour
    {
        [UdonSynced(UdonSyncMode.None)] private bool udonSyncedBool = false;
        // [UdonSynced(UdonSyncMode.None)] private bool inputUseState = false;
        [SerializeField] private string setParameterName;
        [SerializeField] private Animator[] setParameterAnimator;
        private int setParameterNameID;
        private bool isInit = false;

        void Start()
        {
            if (setParameterName == null || setParameterName == "") Debug.Log("syncAnimeSwMomentary:setParameterName:null");
            if (setParameterAnimator == null) Debug.Log("syncAnimeSwMomentary:setParameterAnimator:null");
            setParameterNameID = Animator.StringToHash(setParameterName);
            isInit = true;
        }

        public override void Interact()
        {
            InteractSW();
        }
        public void InteractSW()
        {
            if (!isInit) return;
            //オーナ権限を委譲
            if (!Networking.IsOwner(this.gameObject)) Networking.SetOwner(Networking.LocalPlayer, this.gameObject);

            //UdonSynced変数を更新
            udonSyncedBool = true;

            //同期をリクエスト
            RequestSerialization();

            //付随するオーナ側の更新
            SomeUpdate();
        }

        //Useした瞬間と離した瞬間のみ発火
        public override void InputUse(bool value, VRC.Udon.Common.UdonInputEventArgs args)
        {
            if (!isInit || !Networking.IsOwner(this.gameObject)) return;
            // Debug.Log("testMomentary:" + value);
            // inputUseState = value;

            if (udonSyncedBool && !value) udonSyncedBool = false;
            //同期をリクエスト
            RequestSerialization();
            SomeUpdate();
        }

        public override void OnDeserialization()
        {
            //同期変数を受信した側でも更新する
            SomeUpdate();
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            //プレイヤーがjoinするとこのメソッドが全プレイヤーで実行される
            RequestSerialization(); //これが通るのはオーナのプレイヤーだけ
            //新規joinプレイヤーにも同期変数が受信される
        }

        private void SomeUpdate()
        {
            //なにかUdonSynced変数更新に付随した処理
            int i;
            for (i = 0; i < setParameterAnimator.Length; i++)
            {
                setParameterAnimator[i].SetBool(setParameterNameID, udonSyncedBool);
            }
            return;
        }
    }
}