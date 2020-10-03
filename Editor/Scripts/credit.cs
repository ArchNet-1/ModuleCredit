using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ArchNet.Module.Credit
{
    /// <summary>
    /// [MODULE] - [ARCH NET] - [CREDIT]
    /// author : LOUIS PAKEL
    /// </summary>
    public class credit : MonoBehaviour
    {
        #region Private Properties
        
        // Module Credit
        private Transform _module = null;

        // Scroll view content of the credit
        private Transform _contentView = null;

        // Text Area to load with credit text
        private Transform _creditContentTxt = null;

        // Global Background image of the credit
        private Transform _globalBkgImg = null;

        // Animator
        private Animator _contentViewAnimator = null;

        #endregion

        #region Serialize Fields
        [Header("Custom Setup")]
        // Scene to load after the credit
        [SerializeField, Tooltip("Next scene to load")]
        private string _nextScene = "MainMenu";

        // Text File to Load (Credit content)
        [SerializeField, Tooltip("Text File to load")]
        private TextAsset _textFile = null;

        [Header("Custom Images")]
        // Background image of the credit
        [SerializeField, Tooltip("Global BackGround Sprite")]
        private Sprite _backgroundImg = null;

        // Game logo to display
        [SerializeField, Tooltip("Game Logo Sprite")]
        private Sprite _gameLogoImg = null;

        // Entreprise or corporation image to display
        [SerializeField, Tooltip("Corporation / Entreprise Sprite")]
        private Sprite _corporationImg = null;



        [Header("Animation")]
        [SerializeField, Tooltip("Time before auto loadScene")]
        // Current time before loading next scene
        private int _animationDuration = 60;

        [SerializeField, Tooltip("Credit Animation Clip")]
        // Animation Clip
        private AnimationClip _animationClip = null;



        // Animator
        [SerializeField, Tooltip("Animation Speed")]
        private float _animationSpeed = 2;

        #endregion

        #region Private Methods

        private void Start()
        {

            // Init Credit Scene
            Init();

            // Check if the module is ready
            ModuleAvailable();

            // Generate credit ( text + animation + image)
            GenerateCredit();
        }

        private void Update()
        {
            // If any key is pressed, cut the credit and load screen
            if (Input.anyKeyDown)
            {
                // Load next scene
                LoadScene();
            }
        }

        #endregion

        #region Publics methods

        /// <summary>
        /// Description : Load async a specific scene ( set in sceneToLoad )
        /// </summary>
        public void LoadScene()
        {
            try
            {
                Console.WriteLine(Constants.CREDIT_LOADSCENE + _nextScene);

                // Load async next scene 
                SceneManager.LoadSceneAsync(_nextScene);
            }
            catch (Exception)
            {
                // Trigger Error 409
                Debug.LogError(Constants.ERROR_409 + " (" + _nextScene + ").");
            }
        }

        /// <summary>
        /// Description : Check if the module is correctly set
        /// </summary>
        private void ModuleAvailable()
        {
            if (_backgroundImg == null
               || _gameLogoImg == null
               || _corporationImg == null
               || _nextScene == "")
            {
                // Trigger Error 410
                Debug.LogError(Constants.ERROR_410);
            }

            if (_contentView == null
                || _globalBkgImg == null
                )
            {
                // Trigger Error 411
                Debug.LogError(Constants.ERROR_411);
            }

            if (_textFile == null
                || _animationClip == null
                || _animationSpeed == 0
                || _contentViewAnimator == null
                || _textFile == null)
            {
                // Trigger Error 412
                Debug.LogError(Constants.ERROR_412);
            }
        }

        /// <summary>
        /// Description : Init all gameobjects and load credit text file
        /// </summary>
        private void Init()
        {
            // Set Module Credit
            _module = gameObject.transform;

            // find module credit children by name
            foreach (Transform child in _module)
            {
                if (child.name == Constants.ContentView)
                {
                    // set Content view
                    _contentView = child;
                }
                else if (child.name == Constants.Global_Bkg_Img)
                {
                    // set Global Background
                    _globalBkgImg = child;
                }
            }

            // find content view children by name
            foreach (Transform child in _contentView)
            {
                if (child.name == Constants.Credit_Content_Txt)
                {
                    // set credit content txt
                    _creditContentTxt = child;
                }
            }

            // Set animator
            _contentViewAnimator = this.GetComponent<Animator>();

            // Set speed of credit scroll animation
            _contentViewAnimator.speed = _animationSpeed;

            // Create new animation Event with custom time
            AnimationEvent lAnimationEvent = new AnimationEvent
            {
                functionName = "LoadScene",
                time = _animationDuration
            };

            // Set Event in credit animationClip
            _animationClip.AddEvent(lAnimationEvent);
        }

        /// <summary>
        /// Description : Generate Credit
        /// </summary>
        private void GenerateCredit()
        {
            // Load CustomImg
            GameObject lCustomImg = Resources.Load(Constants.GB_CustomImg) as GameObject;

            // Load BackGround Image
            _globalBkgImg.gameObject.GetComponent<Image>().sprite = _backgroundImg;

            // Instantiate GameLogo
            GameObject lGameLogo = Instantiate(lCustomImg, _contentView);
            lGameLogo.GetComponent<Image>().sprite = _gameLogoImg;
            lGameLogo.transform.SetSiblingIndex(1);

            // Load Text
            _creditContentTxt.gameObject.GetComponent<TextMeshProUGUI>().text = _textFile.text;
            _creditContentTxt.SetSiblingIndex(2);

            // Instantiate Corporation
            GameObject lCorporation = Instantiate(lCustomImg, _contentView);
            lCorporation.GetComponent<Image>().sprite = _corporationImg;
            lCorporation.transform.SetSiblingIndex(3);
        }

        #endregion
    }
}

