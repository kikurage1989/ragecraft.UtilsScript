
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace ragecraft.UtilsScript
{
    //Manualに固定する
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    //複数のコントローラーに一斉に同名boolパラメータの切替を行なうオルタネイトスイッチ
    //ReJoinで同期不良おこすので他SWでの同パラメータ指定NG 
    public class syncAnimeSW : syncSW_Base
    {
        // [UdonSynced(UdonSyncMode.None)]private bool udonSyncedBool;
        // [SerializeField] private string setParameterName;
        // [SerializeField] private Animator[] setParameterAnimator;
        // private int setParameterNameID;
        // private bool isInit = false;

        protected override void Start()
        {
            if(useAnimator)
            {
                if (setParameterName == null || setParameterName == "")
                {
                    Debug.LogError("syncAnimeSW:setParameterName:null");
                    return;
                }
                if (setParameterAnimator == null)
                {
                    Debug.LogError("syncAnimeSW:setParameterAnimator:null");
                    return;
                }
                setParameterNameID = Animator.StringToHash(setParameterName);
            }
            base.Start();
        }

        // public override void Interact()
        // {
        //     //childSWで発火できるよう処理を別関数に移す。Interact直発火はできなかった。
        //     InteractSW();
        // }

        // public override void OnDeserialization()
        // {
        //     //同期変数を受信した側でも更新する
        //     SomeUpdate();
        // }

        // public override void OnPlayerJoined(VRCPlayerApi player)
        // {
        //     //プレイヤーがjoinするとこのメソッドが全プレイヤーで実行される
        //     RequestSerialization(); //これが通るのはオーナのプレイヤーだけ
        //     //新規joinプレイヤーにも同期変数が受信される
        // }


        protected override void SomeUpdate()
        {
            //なにかUdonSynced変数更新に付随した処理
            base.SomeUpdate();
            int i;
            for (i = 0; i < setParameterAnimator.Length; i++)
            {
                setParameterAnimator[i].SetBool(setParameterNameID, udonSyncedBool[0]);
            }
        }
    
        // public void InteractSW()
        // {
        //     if (!isInit) return;
        //     // Debug.Log(this.gameObject.name + ":Interact");
        //     //オーナ権限を委譲
        //     if (!Networking.IsOwner(this.gameObject)) Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
            
        //     //UdonSynced変数を更新
        //     ChangeUdonSyncedValue();

        //     //同期をリクエスト
        //     RequestSerialization();

        //     //付随するオーナ側の更新
        //     SomeUpdate();
        // }
    }
}