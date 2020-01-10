namespace CommandTab
{
    using System;
    using Autodesk.Revit.UI;
    /// <summary>
    /// Revit push button methods
    /// </summary>
    public static class Button
    {
        #region Public methods

        /// <summary>
        /// Creates the push button data based on  data provided 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static PushButton Create(PushButtonDataModel data)
        {
            // The button name based on GUID (Globally unique identifier)
            string btnDataName = Guid.NewGuid().ToString();

            // Sets the button data
            PushButtonData btnData = new PushButtonData(btnDataName, data.Label, PushButtonDataModel.GetAssemblyLocation, data.CommandNamespacePath)
            {
                LargeImage = ResourceImage.GetIcon(data.IconImageName),
                ToolTipImage = ResourceImage.GetIcon(data.TooltipImageName)
            };

            // Return created button and host it on panel provided in required model
            return data.Panel.AddItem(btnData) as PushButton;
        }

        /// <summary>
        /// Create Button Data for split button creation
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static PushButtonData CreateButtonData(PushButtonDataModel data)
        {
            // The button name based on GUID (Globally unique identifier)
            string btnDataName = Guid.NewGuid().ToString();

            // Sets the button data
            PushButtonData btnData = new PushButtonData(btnDataName, data.Label, PushButtonDataModel.GetAssemblyLocation, data.CommandNamespacePath)
            {
                LargeImage = ResourceImage.GetIcon(data.IconImageName),
                ToolTipImage = ResourceImage.GetIcon(data.TooltipImageName)
            };

            return btnData;
        }

        #endregion
    }
}
