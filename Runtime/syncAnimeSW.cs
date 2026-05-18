
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
    }
}