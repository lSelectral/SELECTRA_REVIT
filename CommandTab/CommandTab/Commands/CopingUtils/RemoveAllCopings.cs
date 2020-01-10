namespace CommandTab
{
    #region Namespaces

    using System;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.UI.Selection;
    using System.Collections.Generic;
    using Autodesk.Revit.DB.Structure;
    using Autodesk.Revit.DB.IFC;
    using System.Linq;
    #endregion

    /// <summary>
    /// Remove all structural copings from selected elements
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class RemoveAllCopings : IExternalCommand
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

            Element cutter = GeometryHelper.GetFramingElement(doc, uidoc);

            IList<Element> intersects = GeometryHelper.GetSolidIntersects(doc, activeView, cutter);

            using (Transaction tx = new Transaction(doc))
            {
                // Start the transaction and give a unique name
                tx.Start("Remove Coping");

                try
                {
                    foreach (Element e in intersects)
                    {
                        FamilyInstance familyInstance = e as FamilyInstance;

                        (cutter as FamilyInstance).RemoveCoping(familyInstance);
                    }
                }
                catch (OperationCanceledException)
                {
                    return Result.Failed;
                }
                catch (Exception e)
                {
                    TaskDialog.Show("ERROR", e.Message);
                }
                // End the transaction
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
            return typeof(RemoveAllCopings).Namespace + "." + nameof(RemoveAllCopings);
        }
    }
}
