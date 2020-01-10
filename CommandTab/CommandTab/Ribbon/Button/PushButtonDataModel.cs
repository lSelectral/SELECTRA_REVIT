namespace CommandTab
{
    using Autodesk.Revit.UI;

    /// <summary>
    /// Represent revit push button data model
    /// </summary>
    public class PushButtonDataModel
    {
        #region Public methods

        /// <summary>
        /// Gets or sets the label of button
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the panel on which button is hosted
        /// </summary>
        public RibbonPanel Panel { get; set; }

        /// <summary>
        /// Gets or sets the command namespace path
        /// </summary>
        public string CommandNamespacePath { get; set; }

        /// <summary>
        /// Gets or sets tooltip
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Gets or sets the icon image 
        /// </summary>
        public string IconImageName { get; set; }

        /// <summary>
        /// Gets or sets the Tooltip image 
        /// </summary>
        public string TooltipImageName { get; set; }

        /// <summary>
        /// Gets the core assembly location via "Assembly.GetExecutingAssembly().Location;"
        /// </summary>
        public static string GetAssemblyLocation { get; set; }

        #endregion

        #region Image Names

        /// <summary>
        /// 3D Rotate tooltip image
        /// </summary>
        public string tooltip_3DRotate = "3D Rotate 240x210.png";

        /// <summary>
        /// Orange B Icon
        /// </summary>
        public string Icon_B = "Icon_B.jpg";

        /// <summary>
        /// Orange S with Black background Icon
        /// </summary>
        public string Icon_S = "Icon-S 32x32.png";

        /// <summary>
        /// Rotate Icon
        /// </summary>
        public string Icon_Rotate = "Icon_Rotate 32x32.png";

        /// <summary>
        /// Beam Icon
        /// </summary>
        public string Icon_Beam = "Icon_Beam 32x32.jpg";

        /// <summary>
        /// Door opening Icon
        /// </summary>
        public string Icon_DoorOpening = "Door Opening 32x32.png";

        /// <summary>
        /// Override icon
        /// </summary>
        public string Icon_Override = "Override 32x32.png";

        /// <summary>
        /// Plate connection icon
        /// </summary>
        public string Icon_PlateConnection = "Plate Connection Icon.jpg";

        /// <summary>
        /// Grid Icon
        /// </summary>
        public string Icon_Grid = "Grid Icon.png";

        /// <summary>
        /// Grid tooltip image
        /// </summary>
        public string tooltip_Grid = "Grid Tooltip.png";

        /// <summary>
        /// Beam tooltip image
        /// </summary>
        public string tooltip_Beam = "BeamColumn 320x285.png";

        /// <summary>
        /// Door opening tooltip image
        /// </summary>
        public string tooltip_DoorOpening = "Door Opening 330x277.jpg";

        /// <summary>
        /// Override label tooltip image
        /// </summary>
        public string tooltip_Override = "Override 256x256 .jpg";

        /// <summary>
        /// Beam and plate connection tooltip image
        /// </summary>
        public string tooltip_PlateConnection = "Beam_PlateConnection 306x290.png";

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PushButtonDataModel() { }

        #endregion
    }
}
