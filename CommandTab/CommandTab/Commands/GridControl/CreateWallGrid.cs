namespace CommandTab
{
    #region Namespaces
    using System;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.UI.Selection;
    #endregion

    /// <summary>
    /// Create grid inside of wall location line
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CreateWallGrid : IExternalCommand
    {
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
            // This commands provide interact with active document
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Autodesk.Revit.DB.Document doc = uidoc.Document;
            View activeView = doc.ActiveView;

            var selectedIds = uidoc.Selection.GetElementIds();

            // Access to document changing via transaction
            using (Transaction tx = new Transaction(doc))
            {
                // Start the transaction
                tx.Start("Override Graphic");

                try
                {
                    var selections = uidoc.Selection.PickObjects(ObjectType.Element);

                    foreach (var reference in selections)
                    {
                        // Wall selection
                        Wall wall = (doc.GetElement(reference)) as Wall;
                        // Wall centerline curve
                        Curve locationCurve = (wall.Location as LocationCurve).Curve;

                        Arc gridArc = locationCurve as Arc;
                        Line gridLine = Line.CreateBound(locationCurve.GetEndPoint(0), locationCurve.GetEndPoint(1));

                        Grid grid = null;

                        // If wall is cyclic, create grid in arc shape.
                        if (locationCurve.IsCyclic && gridArc != null)
                        {
                            grid = Grid.Create(doc, gridArc);
                        }
                        if (gridLine != null)
                        {
                            grid = Grid.Create(doc, gridLine);
                        }

                        // If bubbles of grid isn't active, activates it.
                        grid.ShowBubbleInView(DatumEnds.End0, activeView);
                        grid.ShowBubbleInView(DatumEnds.End1, activeView);
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
            return typeof(CreateWallGrid).Namespace + "." + nameof(CreateWallGrid);
        }
    }
}
