
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace ragecraft.UtilsScript
{
    //Manualに固定する
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    //複数のコントローラーに一斉に同名boolパラメータの切替を行なうオルタネイトスイッチ
    //boolでなくfloatの0-1を切り替える
    //ReJoinで同期不良おこすので他SWでの同パラメータ指定NG 
    public class syncAnimeSWFloat : UdonSharpBehaviour
    {
        [UdonSynced(UdonSyncMode.None)]private float udonSyncedFloat;
        [SerializeField] private string setParameterName;
        [SerializeField] private Animator[] setParameterAnimator;
        private int setParameterNameID;
        private bool isInit = false;

        void Start()
        {
            if (setParameterName == null || setParameterName == "")
            {
                Debug.Log("syncAnimeSW:setParameterName:null");
                return;
            }
            if (setParameterAnimator == null)
            {
                Debug.Log("syncAnimeSW:setParameterAnimator:null");
                return;
            }
            setParameterNameID = Animator.StringToHash(setParameterName);
            // Debug.Log("syncAnimeSW:Init");
            isInit = true;
        }

        public override void Interact()
        {
            //childSWFloatで発火できるよう処理を別関数に移す。
            InteractSW();
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
                setParameterAnimator[i].SetFloat(setParameterNameID, udonSyncedFloat);
            }
            return;
        }

        public void InteractSW()
        {
            if (!isInit) return;
            //オーナ権限を委譲
            if (!Networking.IsOwner(this.gameObject)) Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
            
            //UdonSynced変数を更新
            if(setParameterAnimator[0].GetFloat(setParameterNameID) == 0f) udonSyncedFloat = 1.0f;
            else udonSyncedFloat = 0f;

            //同期をリクエスト
            RequestSerialization();

            //付随するオーナ側の更新
            SomeUpdate();
        }
    }
}