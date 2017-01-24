/*============================================================================== 
 Copyright (c) 2016 PTC Inc. All Rights Reserved.
 
 Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

public class UDTEventHandler : MonoBehaviour, IUserDefinedTargetEventHandler
{
    #region PUBLIC_MEMBERS
    /// <summary>
    /// Can be set in the Unity inspector to reference a ImageTargetBehaviour that is instanciated for augmentations of new user defined targets.
    /// Unity inspector에서 User defined Target을 보완하기 위해 인스턴스화 된 ImageTargetBehaviour를 참조하도록 설정할 수 있습니다.
    /// </summary>
    public ImageTargetBehaviour ImageTargetTemplate;

    public int LastTargetIndex
    {
        get { return (mTargetCounter - 1) % MAX_TARGETS; }
    }
    #endregion PUBLIC_MEMBERS

     
    #region PRIVATE_MEMBERS
    private const int MAX_TARGETS = 5;
    private UserDefinedTargetBuildingBehaviour mTargetBuildingBehaviour;
    private QualityDialog mQualityDialog;
    private ObjectTracker mObjectTracker;

    // DataSet that newly defined targets are added to
    // 새로 정의된 타겟 정보를 추가하기 위한 DataSet 선언
    private DataSet mBuiltDataSet;

    // Currently observed frame quality
    // 현재 관찰된 프레임 품질
    private ImageTargetBuilder.FrameQuality mFrameQuality = ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE;

    // Counter used to name newly created targets
    // 새로 생성된 대상의 이름을 지정하는 데 사용되는 카운터
    private int mTargetCounter;

    private TrackableSettings mTrackableSettings;
    #endregion //PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    public void Start()
    {
        mTargetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>();
        if (mTargetBuildingBehaviour)
        {
            mTargetBuildingBehaviour.RegisterEventHandler(this);
            Debug.Log("Registering User Defined Target event handler.");
        }

        mTrackableSettings = FindObjectOfType<TrackableSettings>();
        mQualityDialog = FindObjectOfType<QualityDialog>();
        if (mQualityDialog)
        {
            mQualityDialog.gameObject.SetActive(false);
        }

    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region IUserDefinedTargetEventHandler implementation
    /// <summary>
    /// Called when UserDefinedTargetBuildingBehaviour has been initialized successfully
    /// (한글)UserDefinedTargetBuildingBehaviour가 성공적으로 초기화 될 때 호출됩니다.
    /// </summary>
    public void OnInitialized()
    {
        mObjectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (mObjectTracker != null)
        {
            // Create a new dataset
            mBuiltDataSet = mObjectTracker.CreateDataSet();
            mObjectTracker.ActivateDataSet(mBuiltDataSet);
        }
    }

    /// <summary>
    /// Updates the current frame quality
    /// 현재 프레임 품질을 업데이트합니다.
    /// </summary>
    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        Debug.Log("Frame quality changed: " + frameQuality.ToString());
        mFrameQuality = frameQuality;
        if (mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_LOW)
        {
            Debug.Log("Low camera image quality");
        }
    }

    /// <summary>
    /// Takes a new trackable source and adds it to the dataset
    /// This gets called automatically as soon as you 'BuildNewTarget with UserDefinedTargetBuildingBehaviour
    /// 새로운 추적 가능 소스를 가져 와서 데이터 세트에 추가합니다.
    /// UserDefinedTargetBuildingBehaviour를 사용하여 BuildNewTarget을 호출하면 자동으로 호출됩니다.
    /// </summary>
    public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        mTargetCounter++;

        // Deactivates the dataset first
        //먼저 데이터 집합을 비활성화합니다.
        mObjectTracker.DeactivateDataSet(mBuiltDataSet);

        // Destroy the oldest target if the dataset is full or the dataset 
        // already contains five user-defined targets.

        //데이터 세트가 꽉 찼거나 데이터 세트에 이미 5개의 사용자 정의 타겟이 포함되어있는 경우
        //가장 오래된 타겟을 삭제합니다.
        if (mBuiltDataSet.HasReachedTrackableLimit() || mBuiltDataSet.GetTrackables().Count() >= MAX_TARGETS)
        {
            IEnumerable<Trackable> trackables = mBuiltDataSet.GetTrackables();
            Trackable oldest = null;
            foreach (Trackable trackable in trackables)
            {
                if (oldest == null || trackable.ID < oldest.ID)
                    oldest = trackable;
            }

            if (oldest != null)
            {
                Debug.Log("Destroying oldest trackable in UDT dataset: " + oldest.Name);
                mBuiltDataSet.Destroy(oldest, true);
            }
        }

        // Get predefined trackable and instantiate it
        //미리 정의 된 추적 가능 객체를 가져와서 인스턴스화
        //Scene 하이어라키에 올린 imageTarget(UserDefinedTarget)을 복사한다.
        ImageTargetBehaviour imageTargetCopy = (ImageTargetBehaviour)Instantiate(ImageTargetTemplate);
        imageTargetCopy.gameObject.name = "UserDefinedTarget-" + mTargetCounter;

        // Add the duplicated trackable to the data set and activate it
        // 카피된 추적 가능한 데이타를 데이터 세트에 추가하고 활성화합니다.
        // DataSet에 새로운 TrackableSource와 타켓의 게임오브젝트를 등록한다.
        mBuiltDataSet.CreateTrackable(trackableSource, imageTargetCopy.gameObject);

        // Activate the dataset again
        // DataSet를 ObjectTracker에 활성화합니다.
        mObjectTracker.ActivateDataSet(mBuiltDataSet);

        // Extended Tracking with user defined targets only works with the most recently defined target.
        // If tracking is enabled on previous target, it will not work on newly defined target.
        // Don't need to call this if you don't care about extended tracking.
        // 사용자 정의 된 대상을 사용하는 확장 된 추적은 가장 최근에 정의 된 대상에서만 작동합니다.
        // 이전 타겟에서 트래킹이 활성화 된 경우 새로 정의 된 타겟에서 작동하지 않습니다.
        // 확장 된 추적을 고려하지 않는다면 이것을 호출 할 필요가 없습니다.
        StopExtendedTracking();
        mObjectTracker.Stop();
        mObjectTracker.ResetExtendedTracking();
        mObjectTracker.Start();

        // Make sure TargetBuildingBehaviour keeps scanning...
        //TargetBuildingBehaviour의 타겟스캔 시작
        mTargetBuildingBehaviour.StartScanning();
    }
    #endregion IUserDefinedTargetEventHandler implementation


    #region PUBLIC_METHODS
    /// <summary>
    /// Instantiates a new user-defined target and is also responsible for dispatching callback to 
    /// IUserDefinedTargetEventHandler::OnNewTrackableSource
    /// User-Defined Target을 인스턴스화하며 IUserDefinedTargetEventHandler :: OnNewTrackableSource에 대한 콜백 디스패치도 담당합니다.
    /// </summary>
    public void BuildNewTarget()
    {
        if (mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_MEDIUM ||
            mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH)
        {
            // create the name of the next target.
            // 다음 타겟의 이름을 생성합니다.
            // the TrackableName of the original, linked ImageTargetBehaviour is extended with a continuous number to ensure unique names
            // 고유한 이름을 보장하기 위해 원본 ImageTargetBehaviour의 TrackableName이 연속된 번호로 확장됩니다.
            string targetName = string.Format("{0}-{1}", ImageTargetTemplate.TrackableName, mTargetCounter);

            // generate a new target:
            // 새로운 타겟을 생성합니다.
            mTargetBuildingBehaviour.BuildNewTarget(targetName, ImageTargetTemplate.GetSize().x);
        }
        else
        {
            Debug.Log("Cannot build new target, due to poor camera image quality");
            if (mQualityDialog)
            {
                mQualityDialog.gameObject.SetActive(true);
            }
        }
    }

    public void CloseQualityDialog()
    {
        if (mQualityDialog)
            mQualityDialog.gameObject.SetActive(false);
    }
    #endregion //PUBLIC_METHODS


    #region PRIVATE_METHODS
    /// <summary>
    /// This method only demonstrates how to handle extended tracking feature when you have multiple targets in the scene
    /// So, this method could be removed otherwise
    /// 
    /// 이 함수는 장면에 여러 대상이있을 때 확장 된 추적 기능을 처리하는 방법만 보여줍니다. 따라서 이 메서드는 그렇지 않으면 제거 될 수 있습니다
    /// </summary>
    private void StopExtendedTracking()
    {
        // If Extended Tracking is enabled, we first disable it for all the trackables
        // and then enable it only for the newly created target
        bool extTrackingEnabled = mTrackableSettings && mTrackableSettings.IsExtendedTrackingEnabled();
        if (extTrackingEnabled)
        {
            StateManager stateManager = TrackerManager.Instance.GetStateManager();

            // 1. Stop extended tracking on all the trackables
            foreach (var tb in stateManager.GetTrackableBehaviours())
            {
                var itb = tb as ImageTargetBehaviour;
                if (itb != null)
                {
                    itb.ImageTarget.StopExtendedTracking();
                }
            }

            // 2. Start Extended Tracking on the most recently added target
            List<TrackableBehaviour> trackableList = stateManager.GetTrackableBehaviours().ToList();
            ImageTargetBehaviour lastItb = trackableList[LastTargetIndex] as ImageTargetBehaviour;
            if (lastItb != null)
            {
                if (lastItb.ImageTarget.StartExtendedTracking())
                    Debug.Log("Extended Tracking successfully enabled for " + lastItb.name);
            }
        }
    }
    #endregion //PRIVATE_METHODS
}


