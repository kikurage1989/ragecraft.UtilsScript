内容

syncAnimeSW：
Interectすると指定のAnimatorへ指定のパラメータ名(bool)を切り替えて送信します。
グローバル動作です。LateJoinにも対応しています。
IsTriggerが付いたコライダーへAddCompornentし、Layerを確認して使用してください。

複数のAnimatorへも送信できますが、パラメータ名は共通です。
他SWとの同一パラメータ指定はNGです。
他SWでも同一パラメータを制御したい場合はChildSWを使用してください。

(注)同期を扱うオブジェクトを増減させた際はNetworkIDの重複によるトラブルが発生します。
NetworkIDの振りなおし等で解消してください。

syncAnimeSWFloat：
syncAnimeSWのパラメータをFloat(0.0、1.0）で送信するものです。
BlendTreeでOn/Offアニメーションをまとめた時などに使用します。

syncAnimeSwMomentary：
syncAnimeSWの動作をInterectし続けている間のみTrue、離すとFalseを送信するようにしたものです。
ブザーなどに使用します。

childSW(Float,Momentary)：
それぞれに対応するsyncAnimeSWの子スイッチです。
同一パラメータを複数のSWで制御したいときに使用します。

Teleporter：
ただのテレポーターです。
Interectすると指定のGameObjectの場所にテレポートします。

CustomOwnerLinker：
RBURに付属するOwnerLinkerへEnabledパラメータを追加し、アニメーター等で外部から停止・作動を制御できるようにしたものです。
設定されたAnimatorにて(float) isOwnerLinkerEnabled を0.0にすることにより動作を停止できます。
機関車重連時のオーナー取り合い対策です。
floatのパラメータなのでAnimator内で制御が可能です。

InterectSendCustomEvent：
Interectすると対象のUdonへCustomSendEventを実行します。

OwnerDisplay：
対象GameObjectのOwnerをTextMeshProへ送信するスクリプトです。
TextMeshProUIへは送信できないので注意してください。

ライセンス表記
CustomOwnerLinker：
MIT License
Copyright (c) 2025 frou01
https://github.com/frou01/GrabAnimatorController/tree/main?tab=MIT-1-ov-file
Mod by kikurage1989


その他スクリプト：
The MIT License (MIT)

Copyright (c) 2026 kikurage1989

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
