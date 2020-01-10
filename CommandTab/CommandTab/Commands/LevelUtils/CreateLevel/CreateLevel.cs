namespace CommandTab
{
    #region Namespaces
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.Attributes;
    using System;
    #endregion

    /// <summary>
    /// Create Multiple Levels with given elevation
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CreateLevel : IExternalCommand
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
            Document doc = uidoc.Document;
            View activeView = doc.ActiveView;

            using (CreateLevelForm thisForm = new CreateLevelForm())
            {
                // Show the form
                // Use ShowDialog instead Show for safe using.
                thisForm.ShowDialog();

                if (thisForm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    return Result.Cancelled;
                }

                using (Transaction t = new Transaction(doc))
                {
                    // Start the transaction
                    t.Start("Sample Command");

                    try
                    {
                        double levelCount = thisForm.getLevelCount();
                        double offsetDistance = thisForm.getLevelOffset();

                        if (thisForm.getCheckBox() == true)
                        {
                            Level firstlevel = Level.Create(doc, 0);

                        }

                        for (int i = 1; i < levelCount+1; i++)
                        {
                            Level levels =  Level.Create(doc, offsetDistance * i / 304.80);

                            if (levels.IsBubbleVisibleInView(DatumEnds.End1,activeView))
                                levels.ShowBubbleInView(DatumEnds.End1, activeView);
                        }

                        /*
                        List<Reference> levels = uidoc.Selection.PickObjects(ObjectType.Element, "Please select levels").ToList();

                        foreach (var reference in levels)
                        {
                            Element levelEl = doc.GetElement(reference);

                            if (levelEl as Level != null)
                            {
                                Level level = levelEl as Level;
                                string lvlName = level.Name;

                                lvlName.Remove(0, 6);

                                double.TryParse(lvlName, out double q);

                                level.Elevation = (q - 1) * 600;
                            }
                            else
                            {
                                TaskDialog.Show("Error", "Please select only level");
                            }
                        }*/
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
            return typeof(CreateLevel).Namespace + "." + nameof(CreateLevel);
        }
    }
}
