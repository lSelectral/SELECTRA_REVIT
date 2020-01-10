namespace CommandTab
{
    #region Namespaces
    using System;
    using System.Linq;
    using System.Text;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.DB.IFC;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB.Structure;
    using Autodesk.Revit.DB.Steel;
    using Autodesk.Revit.UI.Selection;
    using Autodesk.Revit.DB.ExtensibleStorage;
    using Autodesk.Revit.DB.DirectContext3D;
    using Autodesk.Revit.DB.Fabrication;
    using System.Collections.Generic;
    using Autodesk.Revit.Creation;
    using RvtDwgAddon;

    using RVTDoc = Autodesk.Revit.DB.Document;
    using RVTransaction = Autodesk.Revit.DB.Transaction;
    using ASDoc = Autodesk.AdvanceSteel.DocumentManagement.Document;

    using Autodesk.AdvanceSteel.DocumentManagement;
    using Autodesk.AdvanceSteel.Geometry;
    using Autodesk.AdvanceSteel.Modelling;
    using Autodesk.AdvanceSteel.CADAccess;
    using Wall = Autodesk.Revit.DB.Wall;
    using Grid = Autodesk.Revit.DB.Grid;
    using Plane = Autodesk.Revit.DB.Plane;
    #endregion

    /// <summary>
    /// Sample Class
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Sample : IExternalCommand
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
            RVTDoc doc = uidoc.Document;
            View activeView = doc.ActiveView;

            using (RVTransaction t = new RVTransaction(doc))
            {
                // Start the transaction
                t.Start("Sample Command");

                try
                {

                    //SteelConnectionOptions.MatchPropertiesDetailedStructuralConnection(uidoc);


                }
                catch (OperationCanceledException)
                {
                    return Result.Failed;
                }

                catch (Exception e)
                {
                    TaskDialog.Show("ERROR", e.Message);
                }
                // Commit the transaction
                t.Commit();
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
            return typeof(Sample).Namespace + "." + nameof(Sample);
        }
    }
}
