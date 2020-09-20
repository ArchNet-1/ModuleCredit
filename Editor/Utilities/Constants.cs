namespace ArchNet.Module.Credit
{
    public static class Constants
    {
        #region Globals

        public const string CREDIT_LOADSCENE = "[ARCH NET] - [CREDIT MODULE] : Load Async scene ";

        #endregion

        #region Errors

        public const string ERROR_409 = "[ARCH NET] - [CREDIT MODULE] [ERROR-409] : Failed to load scene";
        public const string ERROR_410 = "[ARCH NET] - [CREDIT MODULE] [ERROR-410] : Field must not be empty";
        public const string ERROR_411 = "[ARCH NET] - [CREDIT MODULE] [ERROR-411] : Missing GameObject";
        public const string ERROR_412 = "[ARCH NET] - [CREDIT MODULE] [ERROR-412] : Cannot load file";

        #endregion

        #region GameObjects

        public const string GB_Text = "TextToDisplay";
        public const string GB_BackGround = "BackGroundImg";
        public const string GB_Content = "Content";
        public const string GB_CustomImg = "CustomImg";

        #endregion
    }
}
