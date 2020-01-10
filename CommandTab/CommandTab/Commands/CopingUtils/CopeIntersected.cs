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
    using System.Linq;
    #endregion

    /// <summary>
    /// Multiple framing coping external command class
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CopeIntersected : IExternalCommand
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
            // This commands provide interact with active document
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Autodesk.Revit.DB.Document doc = uidoc.Document;
            View activeView = doc.ActiveView;

            // Apply a filter to selection for selecting only valid elements (Structural Framing Elements)
            ISelectionFilter structuralFramingFilter = new StructuralFramingSelectionFilter();

            


            using (StructuralConnectionControl thisForm = new StructuralConnectionControl())
            {
                // Show the form
                // Use ShowDialog instead Show for safe using.
                thisForm.ShowDialog();

                if (thisForm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    return Result.Cancelled;
                }

                // Access to document changing via transaction
                using (Transaction tx = new Transaction(doc))
                {
                    // Start the transaction and give a unique name which will shown in undo/redo menu
                    tx.Start("Multiple Cope");

                    try
                    {
                        // Select the Cutter object with framing selection filter
                        Element e1 = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element, structuralFramingFilter, "Please select structural framing"));
                        FamilyInstance cutter = e1 as FamilyInstance;

                        // Get bounding box of cutter element
                        BoundingBoxXYZ beamBounding = cutter.get_BoundingBox(activeView);
                        // Get the outline from bouinding box
                        Outline beamOutline = new Outline(beamBounding.Min, beamBounding.Max);

                        // Get bouinding box outline as filter
                        BoundingBoxIntersectsFilter BoundingFilter = new BoundingBoxIntersectsFilter(beamOutline);

                        // Apply bounding box filter to the Filtered Element Collector
                        // This filter collect structural framings which pass the filter and convert to the element
                        IList<Element> cutteds = new FilteredElementCollector(doc)
                            .OfCategory(BuiltInCategory.OST_StructuralFraming).WherePasses(BoundingFilter).ToElements();

                        if (null != e1)
                        {
                            foreach (Element i in cutteds)
                            {
                                //  Family instance of cutted elements
                                FamilyInstance familyInstance = i as FamilyInstance;

                                // Get the location curve of intersects
                                LocationCurve locationCurve = i.Location as LocationCurve;
                                Curve curve = locationCurve.Curve; 

                                XYZ startPoint = curve.GetEndPoint(0);
                                XYZ endPoint = curve.GetEndPoint(1);

                                // Parameters of for getting property of intersected frames
                                Parameter ParamstartJoinCutback = i.get_Parameter(BuiltInParameter.START_JOIN_CUTBACK);
                                Parameter ParamendJoinCutback = i.get_Parameter(BuiltInParameter.END_JOIN_CUTBACK);
                                Parameter ParamcutLength = i.get_Parameter(BuiltInParameter.STRUCTURAL_FRAME_CUT_LENGTH);

                                double length = (i.Location as LocationCurve).Curve.Length;
                                double cutBackLength = ParamcutLength.AsDouble();

                                double extendDistance = ((cutBackLength - length) / 2) + GeometryHelper.FeetToMilimeter(12.7);

                                // Set the cope distance of cutted elements from given value
                                Parameter q = i.get_Parameter(BuiltInParameter.STRUCTURAL_COPING_DISTANCE);
                                q.Set(thisForm.GetCopeDistance());

                                // Locate the which end point of intersects with given tolerance and extend this point 
                                // If cutback/ length rate so angle is to big don't include it
                                if (beamOutline.Contains(startPoint, 0.1) || (cutBackLength / length < 0.84))
                                {
                                    ParamstartJoinCutback.Set(extendDistance);
                                }
                                if (beamOutline.Contains(endPoint, 0.1) || (cutBackLength / length < 0.84))
                                {
                                    ParamendJoinCutback.Set(extendDistance);
                                }

                                // Selected elements shouldn't cope itself otherwise it will completly be inside itself and will thrown an error
                                // If cutback/ length rate angle is too big, don't include it
                                if (familyInstance.Id != e1.Id && (cutBackLength / length < 0.84))
                                {
                                    // Cope the framings
                                    familyInstance.AddCoping(cutter);
                                }
                            }
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
            return typeof(CopeIntersected).Namespace + "." + nameof(CopeIntersected);
        }

        #endregion
    }
}
