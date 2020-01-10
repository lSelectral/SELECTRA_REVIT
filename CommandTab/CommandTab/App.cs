#region Namespaces
using System.Collections;
using System.Reflection;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using Autodesk.Revit.DB.Structure;
#endregion

namespace CommandTab
{
    /// <summary>
    /// Implement Revit External Application Interface
    /// </summary>
    public class App : IExternalApplication
    {
        #region Class Instances

        /// <summary>
        /// Class instance
        /// </summary>
        internal static App thisApp = null;

        /// <summary>
        /// Door Control Modeless Form Instance
        /// </summary>
        private ModelessForm m_DoorControlForm;

        /// <summary>
        /// Rotate Multiple Elements Modeless Form Instance
        /// </summary>
        private Rotating_Form m_rotatingForm;

        /// <summary>
        ///Beam Symbol Selection Modeless Form Instance
        /// </summary>
        private SymbolSelectionForm m_SymbolSelection;

        /// <summary>
        /// Structural Connection Type Selector Modeless Form Instance
        /// </summary>
        private StructuralConnectionSelector m_structuralConnectionSelector;

        #endregion

        #region Public Methods

        /// <summary>
        /// Execute the command when REVIT starts
        /// </summary>
        /// <param name="a">Access the control of user interface application</param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication a)
        {
            m_DoorControlForm = null;   // no dialog needed yet; the command will bring it
            m_rotatingForm = null;
            m_SymbolSelection = null;
            m_structuralConnectionSelector = null;

            thisApp = this;  // static access to this application instance

            #region Revit ribbon and tab creation

            // An instance of push button data model class
            PushButtonDataModel p = new PushButtonDataModel();

            // Get executing assembly for creating button data
            PushButtonDataModel.GetAssemblyLocation = Assembly.GetExecutingAssembly().Location;

            // Create Ribbon Tab
            string tabName = "Selectra Main";
            a.CreateRibbonTab(tabName);

            // Create the ribbon panels
            RibbonPanel arhcitecturalCommandsPanel = a.CreateRibbonPanel(tabName, "Architectural Commands");
            RibbonPanel structuralCommandsPanel = a.CreateRibbonPanel(tabName, "Structural Commads");

            TextBoxData textBoxData = new TextBoxData("Cope Distance");
            TextBox copeDistanceTextBox = structuralCommandsPanel.AddItem(textBoxData) as TextBox;
            copeDistanceTextBox.Width /= 3;
            copeDistanceTextBox.PromptText = "Cope Distance";
            copeDistanceTextBox.Value = "0.0";
            copeDistanceTextBox.ToolTip = "Sets the cope distance of cutted framing elements";

            SplitButtonData splitButtonData = new SplitButtonData("Multi Cope Split", "Multi Cope");
            SplitButton splitMultiCope = structuralCommandsPanel.AddItem(splitButtonData) as SplitButton;
            splitMultiCope.ItemText = "Multiple Cope Options";

            SplitButtonData splitDatumButtonData = new SplitButtonData("Datum Util Split", "Datum Utils");
            SplitButton splitButtonDatum = arhcitecturalCommandsPanel.AddItem(splitDatumButtonData) as SplitButton;
            splitButtonDatum.ItemText = "Datum Utils";

            #region Create Beam between Columns Button

            // Populate button data model
            PushButtonDataModel BeamBtwColButtonData = new PushButtonDataModel()
            {
                Label = "CreateBeam\nBetweenColumn",
                Panel = structuralCommandsPanel,
                CommandNamespacePath = CreateBeamCommand.GetPath(),
                Tooltip = "Create Beam between columns from selected beam symbol",
                IconImageName = p.Icon_Beam,
                TooltipImageName = p.tooltip_Beam
            };

            // Create Button from provided data
            PushButton beamButton = Button.Create(BeamBtwColButtonData);

            #endregion

            #region Create Structural Connection Button

            // Populate push button data model
            PushButtonDataModel createStructuralConnectionData = new PushButtonDataModel()
            {
                Label = "Create Structural\nConnection",
                Panel = structuralCommandsPanel,
                CommandNamespacePath = CreateStructuralConnectionCommand.GetPath(),
                Tooltip = "Create Multiple Structural Connection between selected elements",
                IconImageName = p.Icon_PlateConnection,
                TooltipImageName = p.tooltip_PlateConnection
            };

            // Create push button from populated button data
            PushButton createStructuralConnectionButton = Button.Create(createStructuralConnectionData);

            #endregion

            #region Rotate Multiple Elements Button

            // Populate button data model
            PushButtonDataModel rotateButtonData = new PushButtonDataModel()
            {
                Label = "Rotate Multiple\nElements",
                Panel = arhcitecturalCommandsPanel,
                CommandNamespacePath = RotateMultiCommand.GetPath(),
                Tooltip = "Create Beam between columns from selected beam symbol",
                IconImageName = p.Icon_Rotate,
                TooltipImageName = p.tooltip_3DRotate
            };

            // Create Button from provided data
            PushButton rotateButton = Button.Create(rotateButtonData);

            #endregion

            #region Door Control Button

            // Populate button data model
            PushButtonDataModel doorControlData = new PushButtonDataModel()
            {
                Label = "Door Control",
                Panel = arhcitecturalCommandsPanel,
                CommandNamespacePath = DoorControlCommand.GetPath(),
                Tooltip = "Control the door behaviour due to user input",
                IconImageName = p.Icon_DoorOpening,
                TooltipImageName = p.tooltip_DoorOpening
            };

            // Create button from provided data
            PushButton doorControlButton = Button.Create(doorControlData);

            #endregion

            #region Multiple Coping Button Data

            // Populate button data model
            PushButtonDataModel multiCopeData = new PushButtonDataModel()
            {
                Label = "Multiple\nCope",
                Panel = structuralCommandsPanel,
                CommandNamespacePath = CopeIntersected.GetPath(),
                Tooltip = "Cope multiple beams and columns",
                IconImageName = p.Icon_B,
                TooltipImageName = p.tooltip_Beam,
            };

            #endregion

            #region Cope Intersected Beams Button Data

            // Populate button data model
            PushButtonDataModel copeIntersectsData = new PushButtonDataModel()
            {
                Label = "Cope Intersected Beams",
                Panel = structuralCommandsPanel,
                CommandNamespacePath = CopeIntersectedBeams.GetPath(),
                Tooltip = "Cope the beams that intersects with selected beam",
                IconImageName = p.Icon_Beam,
                TooltipImageName = p.tooltip_Beam
            };

            #endregion

            #region Cope Beam With Intersects Button Data

            PushButtonDataModel copeSelectedData = new PushButtonDataModel()
            {
                Label = "Cope Selected Beam\n with Intersects",
                Panel = structuralCommandsPanel,
                CommandNamespacePath = CopeSelectedWIntersects.GetPath(),
                Tooltip = "Cope selected beam through intersects",
                IconImageName = p.Icon_B,
                TooltipImageName = p.tooltip_Beam
            };

            #endregion

            #region Cope Connected Beams

            // Populate button data model
            PushButtonDataModel copeConnectedsData = new PushButtonDataModel()
            {
                Label = "Cope connected beams",
                Panel = structuralCommandsPanel,
                CommandNamespacePath = CopeConnectedBeams.GetPath(),
                Tooltip = "Cope the beams that connected to selected element",
                IconImageName = p.Icon_Beam,
                TooltipImageName = p.tooltip_Beam,
            };

            #endregion

            #region Cope Beams Connected To Structural Column Button Data

            PushButtonDataModel copeConnectedToColumnData = new PushButtonDataModel()
            {
                Label = "Cope Beams Connected To Structural Column",
                Panel = structuralCommandsPanel,
                CommandNamespacePath = CopeBeamConnectedToStrColumn.GetPath(),
                Tooltip = "Cope beams connected to structural column",
                IconImageName = p.Icon_Beam,
                TooltipImageName = p.tooltip_Beam
            };

            #endregion

            #region Remove copings Button Data

            // Populate button data model
            PushButtonDataModel removeCopeButtonData = new PushButtonDataModel()
            {
                Label = "Remove Copings",
                Panel = structuralCommandsPanel,
                CommandNamespacePath = RemoveAllCopings.GetPath(),
                Tooltip = "Remove all copings from selected elements",
                IconImageName = p.Icon_Beam,
                TooltipImageName = p.tooltip_DoorOpening
            };

            #endregion

            #region Override Graphic Settings Button

            // Populate button data model
            PushButtonDataModel overrideButtonData = new PushButtonDataModel()
            {
                Label = "Override\nGraphics",
                Panel = arhcitecturalCommandsPanel,
                CommandNamespacePath = OverrideCommand.GetPath(),
                Tooltip = "Override graphic settings of selected element",
                IconImageName = p.Icon_Override,
                TooltipImageName = p.tooltip_Override
            };

            // Create push button from provided data
            PushButton overrideButton = Button.Create(overrideButtonData);

            #endregion

            #region Create Wall Grid Button Data

            // Populate the button data model
            PushButtonDataModel createGridButtonData = new PushButtonDataModel()
            {
                Label = "Create Wall\nGrid",
                Panel = arhcitecturalCommandsPanel,
                CommandNamespacePath = CreateWallGrid.GetPath(),
                Tooltip = "Create Grid from wall center line",
                IconImageName = p.Icon_Grid,
                TooltipImageName = p.tooltip_Grid
            };

            #endregion

            #region Plate Param Changer Button

            // Populate button data mode
            PushButtonDataModel plateParamData = new PushButtonDataModel()
            {
                Label = "Plate Parametre\nController",
                Panel = structuralCommandsPanel,
                CommandNamespacePath = SteelElementProperty.GetPath(),
                Tooltip = "Set the parameter of plates (Volume and Mass)",
                IconImageName = p.Icon_PlateConnection,
                TooltipImageName = p.tooltip_PlateConnection
            };

            PushButton plateParamButton = Button.Create(plateParamData);

            #endregion

            #region Schema Build Button

            // Populate button data model
            PushButtonDataModel schemaBuildButtonData = new PushButtonDataModel()
            {
                Label = "Add Data",
                Panel = structuralCommandsPanel,
                CommandNamespacePath = AddField.GetPath(),
                Tooltip = "Add Data to extensible storage",
                IconImageName = p.Icon_B,
                TooltipImageName = p.Icon_DoorOpening
            };

            PushButton schemaBuildButton = Button.Create(schemaBuildButtonData);

            #endregion

            #region Tag Wall Layers Button

            PushButtonDataModel tagWallLayerData = new PushButtonDataModel()
            {
                Label = "Tag Wall\nLayers",
                Panel = arhcitecturalCommandsPanel,
                CommandNamespacePath = TagWallLayers.GetPath(),
                Tooltip = "Create a textnote element at given point that includes the wall layers information",
                IconImageName = p.Icon_B,
                TooltipImageName = p.tooltip_DoorOpening
            };

            PushButton tagWallButton = Button.Create(tagWallLayerData);

            #endregion

            #region Create Level Button Data

            // Populate button data model with given input
            PushButtonDataModel createLevelData = new PushButtonDataModel()
            {
                Label = "Create Multiple\nLevel",
                Panel = arhcitecturalCommandsPanel,
                CommandNamespacePath = CreateLevel.GetPath(),
                Tooltip = "Create Multiple Level with given user input",
                IconImageName = p.Icon_B,
                TooltipImageName = p.tooltip_Grid
            };

            #endregion

            #region Set Elevation Button Data

            // Populate Button Data model
            PushButtonDataModel setElevationData = new PushButtonDataModel()
            {
                Label = "Set Elevation",
                Panel = arhcitecturalCommandsPanel,
                CommandNamespacePath = SetELevation.GetPath(),
                Tooltip = "Set elevation of level(s) with given input",
                IconImageName = p.Icon_S,
                TooltipImageName = p.Icon_S
            };

            #endregion

            #region Sample Button

            // Populate button data model
            PushButtonDataModel sampleData = new PushButtonDataModel()
            {
                Label = "SAMPLE",
                Panel = arhcitecturalCommandsPanel,
                CommandNamespacePath = Sample.GetPath(),
                Tooltip = "This is just a sample for test purpose, content change continiously",
                IconImageName = p.Icon_S,
                TooltipImageName = p.tooltip_Override
            };

            // Create push button from provided data
            PushButton sampleButton = Button.Create(sampleData);

            #endregion

            // Add push button to multiple cope split button
            splitMultiCope.AddPushButton(Button.CreateButtonData(multiCopeData));
            splitMultiCope.AddPushButton(Button.CreateButtonData(copeIntersectsData));
            splitMultiCope.AddPushButton(Button.CreateButtonData(copeSelectedData));
            splitMultiCope.AddPushButton(Button.CreateButtonData(copeConnectedToColumnData));
            splitMultiCope.AddPushButton(Button.CreateButtonData(removeCopeButtonData));
            splitMultiCope.AddPushButton(Button.CreateButtonData(copeConnectedsData));

            // Add push button to datum utils split button
            splitButtonDatum.AddPushButton(Button.CreateButtonData(createLevelData));
            splitButtonDatum.AddPushButton(Button.CreateButtonData(setElevationData));
            splitButtonDatum.AddPushButton(Button.CreateButtonData(createGridButtonData));

            #endregion

            return Result.Succeeded;
        }

        /// <summary>
        /// Execute the command when REVIT shut down
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication a)
        {
            if (m_DoorControlForm != null && m_DoorControlForm.Visible)
            {
                m_DoorControlForm.Close();
            }

            if (m_rotatingForm != null && m_rotatingForm.Visible)
            {
                m_rotatingForm.Close();
            }

            if (m_SymbolSelection != null && m_SymbolSelection.Visible)
            {
                m_SymbolSelection.Close();
            }

            if (m_structuralConnectionSelector != null && m_structuralConnectionSelector.Visible)
            {
                m_structuralConnectionSelector.Close();
            }

            return Result.Succeeded;
        }

        #region Modeless Form Public Methods

        /// <summary>
        ///   This method creates and shows a modeless dialog, unless it already exists.
        /// </summary>
        /// <remarks>
        ///   The external command invokes this on the end-user's request
        /// </remarks>
        /// 
        public void ShowForm_DoorControlForm(UIApplication uiapp)
        {
            // If we do not have a dialog yet, create and show it
            if (m_DoorControlForm == null || m_DoorControlForm.IsDisposed)
            {
                // A new handler to handle request posting by the dialog
                RequestHandler handler = new RequestHandler();

                // External Event for the dialog to use (to post requests)
                ExternalEvent exEvent = ExternalEvent.Create(handler);

                // We give the objects to the new dialog;
                // The dialog becomes the owner responsible fore disposing them, eventually.
                m_DoorControlForm = new ModelessForm(exEvent, handler);
                m_DoorControlForm.Show();
            }
        }

        /// <summary>
        ///   This method creates and shows a modeless dialog, unless it already exists.
        /// </summary>
        /// <remarks>
        ///   The external command invokes this on the end-user's request
        /// </remarks>
        /// 
        public void ShowForm_RotatingForm(UIApplication uiapp)
        {
            // If we don't have dialog yet, create and show it
            if (m_rotatingForm == null || m_rotatingForm.IsDisposed)
            {
                // A new handler handle request posting by the dialog
                RotateMultiExternalEvent handler = new RotateMultiExternalEvent();

                // External event for the dialog to use (to post request)
                ExternalEvent exEvent = ExternalEvent.Create(handler);

                // We give the objects to the new dialog;
                // The dialog becomes the owner responsible fore disposing them, eventually.
                m_rotatingForm = new Rotating_Form(exEvent, handler);
                m_rotatingForm.Show();
            }
        }

        /// <summary>
        ///   This method creates and shows a modeless dialog, unless it already exists.
        /// </summary>
        /// <remarks>
        ///   The external command invokes this on the end-user's request
        /// </remarks>
        /// 
        public void ShowForm_SymbolSelection(UIApplication uiapp)
        {
            // If we don't have dialog yet, create and show it
            if (m_SymbolSelection == null || m_SymbolSelection.IsDisposed)
            {
                // A new handler handle request posting by the dialog
                CreateBeamExternalEventHandler handler = new CreateBeamExternalEventHandler();

                // External event for the dialog to use (to post request)
                ExternalEvent exEvent = ExternalEvent.Create(handler);

                // We give the objects to the new dialog;
                // The dialog becomes the owner responsible fore disposing them, eventually.
                m_SymbolSelection = new SymbolSelectionForm(exEvent, handler, uiapp.ActiveUIDocument.Document);
                m_SymbolSelection.Show();
            }
        }

        /// <summary>
        ///   This method creates and shows a modeless dialog, unless it already exists.
        /// </summary>
        /// <remarks>
        ///   The external command invokes this on the end-user's request
        /// </remarks>
        /// 
        public void ShowForm_StructuralConnectionSelector(UIApplication uiapp)
        {
            // If we don't have dialog yet, create and show it
            if (m_structuralConnectionSelector == null || m_structuralConnectionSelector.IsDisposed)
            {
                // A new handler handle request posting by the dialog
                CreateStructuralConnectionEvent handler = new CreateStructuralConnectionEvent();

                // External event for the dialog to use (to post request)
                ExternalEvent exEvent = ExternalEvent.Create(handler);

                // We give the objects to the new dialog;
                // The dialog becomes the owner responsible fore disposing them, eventually.
                m_structuralConnectionSelector = new StructuralConnectionSelector(exEvent, handler, uiapp.ActiveUIDocument.Document);
                m_structuralConnectionSelector.Show();
            }
        }

        /// <summary>
        ///   Waking up the dialog from its waiting state.
        /// </summary>
        public void WakeFormUp_DoorControlForm()
        {
            if (m_DoorControlForm != null)
            {
                m_DoorControlForm.WakeUp();
            }
        }

        /// <summary>
        ///   Waking up the dialog from its waiting state.
        /// </summary>
        public void WakeFormUp_RotatingForm()
        {
            if (m_rotatingForm != null)
            {
                m_rotatingForm.WakeUp();
            }
        }

        /// <summary>
        ///   Waking up the dialog from its waiting state.
        /// </summary>
        public void WakeFormUp_SymbolSelectionForm()
        {
            if (m_SymbolSelection != null)
            {
                m_SymbolSelection.WakeUp();
            }
        }

        /// <summary>
        ///   Waking up the dialog from its waiting state.
        /// </summary>
        public void WakeFormUp_StructuralConnectionSelector()
        {
            if (m_structuralConnectionSelector != null)
            {
                m_structuralConnectionSelector.WakeUp();
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Store the X angle value from modeless form
        /// </summary>
        public static double angleX;
        /// <summary>
        /// Store the X angle value from modeless form
        /// </summary>
        public static double angleY;
        /// <summary>
        /// Store the X angle value from modeless form
        /// </summary>
        public static double angleZ;

        /// <summary>
        /// Gets combobox selection from symbol selection form
        /// </summary>
        public static object beamTypeSelection { get; set; }

        /// <summary>
        /// Store the beam symbols in the document
        /// </summary>
        public static ArrayList beamMaps { get; set; }

        /// <summary>
        /// Store the connection types in the document
        /// </summary>
        public static ArrayList structuralConnectionMap { get; set; }

        /// <summary>
        /// Gets combobox selection from symbol selection form
        /// </summary>
        public static object connectionTypeSelection { get; set; }

        /// <summary>
        /// Store the connection types in the document
        /// </summary>
        public static ArrayList structuralConnectionMapSteel { get; set; }

        /// <summary>
        /// Gets combobox selection from symbol selection form
        /// </summary>
        public static object connectionTypeSelectionSteel { get; set; }

        #endregion

        #endregion
    }
}