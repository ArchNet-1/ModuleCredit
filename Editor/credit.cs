using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace ArchNet.Module.Credit
{
    /// <summary>
    /// [MODULE] - [ARCH NET] - [CREDIT]
    /// </summary>
    public class credit : MonoBehaviour
    {
        #region Private Properties

        private GameObject Text;
        private GameObject Content;

        private GameObject Backgounrd;

        private TextAsset CustomText;
        #endregion

        #region Public Properties

        [Header("Custom Setup")]
        // Scene to load after the credit
        [SerializeField, Tooltip("Next scene to load")]
        private string _nextScene;

        // Name of the text file with all infos
        [SerializeField, Tooltip("Text File to load")]
        private string _text;




        [Header("Custom Images")]
        // Background image of the credit
        [SerializeField, Tooltip("BackGround Image")]
        private Sprite _backgroundImg;

        // Game logo to display
        [SerializeField, Tooltip("Game Logo")]
        private Sprite _gameLogoImg;

        // Entreprise or corporation image to display
        [SerializeField, Tooltip("Corporation / Entreprise logo")]
        private Sprite _corporationImg;



        [Header("Animation")]
        [SerializeField, Tooltip("Time before auto loadScene")]
        // Current time before loading next scene
        private int _time;

        [SerializeField, Tooltip("Credit Animation Clip")]
        // Animation Clip
        private AnimationClip _animationClip;

        // Animator
        [SerializeField,Tooltip("Credit Animator")]
        private Animator _animator;

        // Animator
        [SerializeField, Tooltip("Animation Speed")]
        private float _animationSpeed;

        #endregion

        #region Private Methods

        private void Start()
        {
            Init();

            ModuleAvailable();

            GenerateCredit();
        }

        private void Update()
        {
            // If any key is pressed, cut the credit and load screen
            if (Input.anyKeyDown)
            {
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
               || _text == ""
               || _nextScene == "")
            {
                // Trigger Error 410
                Debug.LogError(Constants.ERROR_410);
            }

            if (Text == null
                || Content == null
                || Backgounrd == null
                )
            {
                // Trigger Error 411
                Debug.LogError(Constants.ERROR_411);
            }

            if (CustomText == null)
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
            _animator.speed = _animationSpeed;

            AnimationEvent lAnimationEvent = new AnimationEvent
            {
                functionName = "LoadScene",
                time = _time
            };


            _animationClip.AddEvent(lAnimationEvent);

            // Set all Game object from components
            Text = GameObject.Find(Constants.GB_Text);
            Content = GameObject.Find(Constants.GB_Content);
            Backgounrd = GameObject.Find(Constants.GB_BackGround);

            // Get File text ressource
            CustomText = Resources.Load(_text) as TextAsset;
        }

        /// <summary>
        /// Description : Generate Credit
        /// </summary>
        private void GenerateCredit()
        {
            GameObject lCustomImg = Resources.Load(Constants.GB_CustomImg) as GameObject;

            // Load BackGround Image
            Backgounrd.GetComponent<Image>().sprite = _backgroundImg;

            // Instantiate GameLogo
            GameObject lGameLogo = Instantiate(lCustomImg, Content.transform);
            lGameLogo.GetComponent<Image>().sprite = _gameLogoImg;
            lGameLogo.transform.SetSiblingIndex(1);

            // Load Text
            Text.GetComponent<TextMeshProUGUI>().text = CustomText.text;
            Text.transform.SetSiblingIndex(2);

            // Instantiate Corporation
            GameObject lCorporation = Instantiate(lCustomImg, Content.transform);
            lCorporation.GetComponent<Image>().sprite = _corporationImg;
            lCorporation.transform.SetSiblingIndex(3);
        }

        #endregion
    }
}

