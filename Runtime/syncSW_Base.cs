
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3;
using VRC.SDK3.Components;
using VRC.Udon.Common.Interfaces;
using VRC.SDK3.UdonNetworkCalling;

namespace ragecraft.UtilsScript
{//Manualに固定する
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class syncSW_Base : UdonSharpBehaviour
    {
        [UdonSynced(UdonSyncMode.None)] public bool[] udonSyncedBool = new bool[1];
        [SerializeField] protected bool useTransform;
        [SerializeField] protected Vector3 SwMove;
        [SerializeField] protected Vector3 SwRotation;
        protected Vector3 initialPosition;
        protected Vector3 initialRotation;
        protected Vector3 changeSwPotison;
        protected Vector3 changeSwRotation;

        [SerializeField] protected bool useAnimator;
        [SerializeField] protected string setParameterName;
        [SerializeField] protected Animator[] setParameterAnimator;
        protected int setParameterNameID;
        protected bool isInit = false;

        protected virtual void Start()
        {
            if(useTransform)
            {
                initialPosition = this.transform.localPosition;
                initialRotation = this.transform.localEulerAngles;
                changeSwPotison = initialPosition + SwMove;
                changeSwRotation = initialRotation + SwRotation;
            }

            // if(useAnimator)
            // {
            //     if (setParameterName == null || setParameterName == "")
            //     {
            //         Debug.LogError("syncAnimeSW:setParameterName:null");
            //         return;
            //     }
            //     if (setParameterAnimator == null)
            //     {
            //         Debug.LogError("syncAnimeSW:setParameterAnimator:null");
            //         return;
            //     }
            //     setParameterNameID = Animator.StringToHash(setParameterName);
            // }
            // Debug.Log("syncAnimeSW:Init");
            isInit = true;
        }

        public override void Interact()
        {
            //childSWで発火できるよう処理を別関数に移す。Interact直発火はできなかった。
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


        protected virtual void SomeUpdate()
        {
            //なにかUdonSynced変数更新に付随した処理
            // int i;
            // for (i = 0; i < setParameterAnimator.Length; i++)
            // {
            //     setParameterAnimator[i].SetBool(setParameterNameID, udonSyncedBool[0]);
            // }
            if(useTransform)
            {
                this.transform.localPosition = udonSyncedBool[0] ? changeSwPotison : initialPosition;
                this.transform.localEulerAngles = udonSyncedBool[0] ? changeSwRotation : initialRotation;
            }
        }
    
        [NetworkCallable]
        public void InteractSW()
        {
            if (!isInit) return;
            // Debug.Log(this.gameObject.name + ":Interact");
            //オーナ権限を委譲
            if (!Networking.IsOwner(this.gameObject)) Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
            
            //UdonSynced変数を更新
            ChangeUdonSyncedValue();

            //同期をリクエスト
            RequestSerialization();

            //付随するオーナ側の更新
            SomeUpdate();
        }

        protected virtual void ChangeUdonSyncedValue()
        {
            udonSyncedBool[0] = !udonSyncedBool[0];
        }   
    }
}