/*==============================================================================
Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// This Component can be used to create new ImageTargets at runtime. It can be configured to start scanning automatically
    /// or via a call from an external script.
    /// Registered event handlers will be informed of changes in the frame quality as well as new TrackableSources
    /// 이 컴포넌트는 런타임에 새 ImageTargets를 만드는 데 사용할 수 있습니다. 자동으로 또는 외부 스크립트의 호출을 통해 스캔을 시작하도록 구성 할 수 있습니다.
    /// 등록 된 이벤트 핸들러는 프레임 품질의 변경 사항과 새로운 TrackableSources에 대한 정보를받습니다.
    /// </summary>
    public class UserDefinedTargetBuildingBehaviour : UserDefinedTargetBuildingAbstractBehaviour
    {
    }
}
