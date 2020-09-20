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
        public string SceneToLoad;
        // Name of the text file with all infos
        public string TextFileName;

        [Header("Custom Images")]
        // Background image of the credit
        public Sprite BackgroundImg;
        // Game logo to display
        public Sprite GameLogoImg;
        // Entreprise or corporation image to display
        public Sprite CorporationImg;


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
                Console.WriteLine(Constants.CREDIT_LOADSCENE + SceneToLoad);

                // Load async next scene 
                SceneManager.LoadSceneAsync(SceneToLoad);
            }
            catch (Exception)
            {
                // Trigger Error 409
                Debug.LogError(Constants.ERROR_409 + " (" + SceneToLoad + ").");
            }
        }

        /// <summary>
        /// Description : Check if the module is correctly set
        /// </summary>
        private void ModuleAvailable()
        {
            if (BackgroundImg == null
               || GameLogoImg == null
               || CorporationImg == null
               || TextFileName == ""
               || SceneToLoad == "")
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
            // Set all Game object from components
            Text = GameObject.Find(Constants.GB_Text);
            Content = GameObject.Find(Constants.GB_Content);
            Backgounrd = GameObject.Find(Constants.GB_BackGround);

            // Get File text ressource
            CustomText = Resources.Load(TextFileName) as TextAsset;
        }

        /// <summary>
        /// Description : Generate Credit
        /// </summary>
        private void GenerateCredit()
        {
            GameObject lCustomImg = Resources.Load(Constants.GB_CustomImg) as GameObject;

            // Instantiate GameLogo
            GameObject lGameLogo = Instantiate(lCustomImg, Content.transform);
            lGameLogo.GetComponent<Image>().sprite = GameLogoImg;
            lGameLogo.transform.SetSiblingIndex(1);

            // Load Text
            Text.GetComponent<TextMeshProUGUI>().text = CustomText.text;
            Text.transform.SetSiblingIndex(2);

            // Instantiate Corporation
            GameObject lCorporation = Instantiate(lCustomImg, Content.transform);
            lCorporation.GetComponent<Image>().sprite = CorporationImg;
            lCorporation.transform.SetSiblingIndex(3);
        }

        #endregion
    }
}

