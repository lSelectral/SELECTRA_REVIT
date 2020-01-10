namespace CommandTab
{
    #region Namespaces

    using System;
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.UI.Selection;
    using Autodesk.Revit.DB.Structure;

    #endregion

    /// <summary>
    /// Graphic override external command
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class OverrideCommand : IExternalCommand
    {
        #region Public Methods

        /// <summary>
        /// Implement this method as an external command for Revit.
        /// </summary>
        /// <param name="commandData">An object that is passed to the external application 
        /// which contains data related to the command, 
        /// such as the application object and active view.</param>
        /// <param name="message">A message that can be set by the external application 
        /// which will be displayed if a failure or cancellation is returned by 
        /// the external command.</param>
        /// <param name="elements">A set of elements to which the external application 
        /// can add elements that are to be highlighted in case of failure or cancellation.</param>
        /// <returns>Return the status of the external command. 
        /// A result of Succeeded means that the API external method functioned as expected. 
        /// Cancelled can be used to signify that the user cancelled the external operation 
        /// at some point. Failure should be returned if the application is unable to proceed with 
        /// the operation.</returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // This commands provide interact with document and application interface
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Autodesk.Revit.DB.Document doc = uidoc.Document;

            // Select the elements which will be override
            IList<Reference> elementIds = uidoc.Selection.PickObjects(ObjectType.Element);

            // An instance of override graphic settings class
            OverrideGraphicSettings overrideGraphicSettings = new OverrideGraphicSettings();

            #region Info about graphic override settings

            // Visible option control complete visibilty of object(s)
            // Halftone, if set to true blend the color with background colour

            // Both 2D and 3D
            //Projection lines in 2D represent corners, in 3D represents the edges for projected elements

            // Only in 3D

            // Surface patterns represent visiblity in 3D work like cut patterns
            // Surface transparency control complete transparency in 3D.


            // Only in 2D

            // Cut line represent edge of object in 2D
            // Cut line pattern represents the style of line in 2D
            // Cut line weight value range is 1-16

            // Cut foreground pattern represent inside of object  from foreground in 2D
            // Cut pattern has to has a cut pattern and set to visible to see in the active view (Not mandatory but can't see either) 2D
            // Same things apply for background too. If foreground's patterns isn't solid, we can see background too. 2D

            #endregion

            // Colors
            Color red = new Color(255, 0, 0); // Solid red
            Color lightBlue = new Color(125,154,218); // Blue for inside of selection
            Color blue = new Color(0,59,189);  // Blue for outside of selection

            // Solid fill isn't include in Revit API
            // We should direct access via Element ID from LookUp
            ElementId solidFillId = new ElementId(3);


            // Access to document changing via transaction
            using (Transaction tx = new Transaction(doc))
            {
                // Start the transaction
                tx.Start("Override Graphic");

                try
                {

                    // Override graphic setttings of elements
                    overrideGraphicSettings.SetCutForegroundPatternId(solidFillId);
                    overrideGraphicSettings.SetCutForegroundPatternColor(red);
                    overrideGraphicSettings.SetCutLineColor(lightBlue);
                    overrideGraphicSettings.SetCutForegroundPatternVisible(true);

                    foreach (var i in elementIds)
                    {
                        Element e = doc.GetElement(i);
                        // Apply the override
                        doc.ActiveView.SetElementOverrides(e.Id, overrideGraphicSettings);
                    }
                }
                catch (Exception e)
                {
                    TaskDialog.Show("ERROR", e.Message);
                }
                tx.Commit();
            }
            return Result.Succeeded;
        }

        /// <summary>
        /// Gets the full namespace path to this command
        /// </summary>
        /// <returns></returns>
        public static string GetPath()
        {
            // Return constructed namespace path 
            return typeof(OverrideCommand).Namespace + "." + nameof(OverrideCommand);
        }

        #endregion

    }
}
