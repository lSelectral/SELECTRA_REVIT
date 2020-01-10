namespace CommandTab
{
    using Autodesk.Revit.UI;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI.Selection;
    using System.Collections.Generic;

    /// <summary>
    /// Command code to be executed when button clicked
    /// </summary>
    /// <seealso cref="Autodesk.Revit.UI.IExternalCommand" />
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class TagWallLayers : IExternalCommand
    {
        #region public methods

        /// <summary>
        /// Tag wall layers by creating text note element on user 
        /// specified point and populate it with layer information
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Application context
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Get access the current view
            var activeView = uidoc.ActiveView;

            // Check if Text note can be created in currently active view
            bool canCreateTextNoteInView = false;
            switch (activeView.ViewType)
            {
                case ViewType.FloorPlan:
                    canCreateTextNoteInView = true;
                    break;
                case ViewType.CeilingPlan:
                    canCreateTextNoteInView = true;
                    break;
                case ViewType.Detail:
                    canCreateTextNoteInView = true;
                    break;
            }

            if (!canCreateTextNoteInView)
            {
                TaskDialog.Show("ERROR" ,"Text note can't be created in the current active view");
                return Result.Cancelled;
            }

            // Ask user to pick location point for Text Note
            var pt = uidoc.Selection.PickPoint("Pick text note location point");

            // Ask user the select one basic wall
            Reference selectionReference = uidoc.Selection.PickObject(ObjectType.Element, "Select one wall");
            Element selectionElement = doc.GetElement(selectionReference);

            // Cast generic element type to more specific wall type
            Wall wall = selectionElement as Wall;

            // Check if wall is type of basic wall
            if (wall.IsStackedWall)
            {
                TaskDialog.Show("Warning" ,"Wall you selected is category of the  Stacked Wall.\nIt's not supported by this command");
                return Result.Cancelled;
            }

            // Access list of wall layers
            IList<CompoundStructureLayer> layers = wall.WallType.GetCompoundStructure().GetLayers();

            // Get layer information in structured string format for Text note
            string a = "";
            a += "Layer Function     ||    " + "Material Name     ||    " + "Layer Width" + "\n";
            foreach (var l in layers)
            {
                var material = doc.GetElement(l.MaterialId) as Material;
                if (material != null)
                {
                    a += l.Function.ToString() + "        " + material.Name + "        " + FeetTomm(l.Width).ToString() + "mm" + "\n";
                }
            }

            // Create text note options
            var textNoteOptions = new TextNoteOptions
            {
                VerticalAlignment = VerticalTextAlignment.Top,
                HorizontalAlignment = HorizontalTextAlignment.Left,
                TypeId = doc.GetDefaultElementTypeId(ElementTypeGroup.TextNoteType)
            };

            // Open transaction for access to revit UI level document changing
            using (Transaction t = new Transaction(doc))
            {
                t.Start("Tag Wall Layers");

                // Create text note with wall layers information on user specified point in the current active view
                var textNote = TextNote.Create(doc, activeView.Id, pt, a, textNoteOptions);

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
            return typeof(TagWallLayers).Namespace + "." + nameof(TagWallLayers);
        }

        /// <summary>
        /// Convert feet to mm
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public double FeetTomm(double number)
        {
            double temp = number * 304.8;
            return temp;
        }

        #endregion
    }
}
