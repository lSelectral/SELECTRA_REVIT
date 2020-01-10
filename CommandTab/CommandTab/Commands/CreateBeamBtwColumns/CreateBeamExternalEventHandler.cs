namespace CommandTab
{
    #region Namespaces
    using Autodesk.Revit.DB;
    using Autodesk.Revit.DB.Structure;
    using Autodesk.Revit.UI;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    #endregion

    /// <summary>
    /// Create Beam Between Columns External Event Handler
    /// </summary>
    public class CreateBeamExternalEventHandler : IExternalEventHandler
    {
        #region Public External Event Handler Methods

        /// <summary>
        ///   The top method of the event handler.
        /// </summary>
        /// <remarks>
        ///   This is called by Revit after the corresponding
        ///   external event was raised (by the modeless form)
        ///   and Revit reached the time at which it could call
        ///   the event's handler (i.e. this object)
        /// </remarks>
        /// <param name="app">Revit User Interface Application</param>
        public void Execute(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = uidoc.Document;
            View view = doc.ActiveView;

            // External Event Handler Instance
            CreateBeamExternalEventHandler createBeamExternalEventHandler = new CreateBeamExternalEventHandler();
            // Create a external event inherited from handler.
            ExternalEvent externalEvent = ExternalEvent.Create(createBeamExternalEventHandler);

            // Modeless Form Instance
            SymbolSelectionForm symbolSelectionForm = new SymbolSelectionForm(externalEvent, createBeamExternalEventHandler, doc);

            try
            {
                using (Transaction t = new Transaction(doc))
                {
                    //Provide access to make change in document
                    t.Start("Create Beam W Columns");

                    // Collect selected objects element id
                    ICollection<ElementId> selElements = uidoc.Selection.GetElementIds();

                    ICollection<ElementId> structuralColumns = 
                        new FilteredElementCollector(uidoc.Document,selElements).OfCategory(BuiltInCategory.OST_StructuralColumns).ToElementIds();

                    CreateBeamMethod(doc, view, structuralColumns, symbolSelectionForm);

                    t.Commit();
                    App.thisApp.WakeFormUp_SymbolSelectionForm();
                }
            }
            catch (Exception e)
            {
                TaskDialog.Show("INFO", e.Message);
            }
            finally
            {
                App.thisApp.WakeFormUp_SymbolSelectionForm();
            }
            return;
        }

        /// <summary>
        ///   A method to identify this External Event Handler
        /// </summary>
        public string GetName()
        {
            return "Create Beam Between Columns External Event Handler";
        }

        #endregion

        #region Private and Public Methods

        /// <summary>
        /// Create Beam(s) Between The Selected Structural Columns
        /// </summary>
        /// <param name="doc">Active Revit Document</param>
        /// <param name="refList">Selected Elements Id list</param>
        /// <param name="symbolSelectionForm">Symbol Selection Form</param>
        /// <param name="view">Active Revit View</param>
        private void CreateBeamMethod(Document doc, View view ,ICollection<ElementId> refList, SymbolSelectionForm symbolSelectionForm)
        {
            // Collect column location point with offset as point list
            List<XYZ> points = new List<XYZ>();

            // Need at least two columns
            if (refList.Count < 2)
            {
                TaskDialog.Show("ERROR", "Please select at least two columns");
            }

            // Iterate through the list
            foreach (ElementId r in refList)
            {
                Element column = doc.GetElement(r);

                // Get element as family instance
                FamilyInstance familyInstance = column as FamilyInstance;
                FamilySymbol columnSymbol = familyInstance.Symbol;

                // If selected elements aren't structural column cancel the operation
                if (familyInstance.StructuralType == StructuralType.Column)
                {
                    // Get the bounding box of element
                    BoundingBoxXYZ boxXYZ = familyInstance.get_BoundingBox(view);

                    // Column bounding box reference height
                    double column_height = boxXYZ.Max.Z - boxXYZ.Min.Z;

                    // Offset point
                    XYZ offset = new XYZ(0, 0, column_height);

                    //// Get the column location point
                    Location loc = familyInstance.Location;
                    LocationPoint locationPoint = loc as LocationPoint;

                    // Add offset to the column location point to get the height of column
                    points.Add(locationPoint.Point.Add(offset));
                }

                else
                {
                    TaskDialog.Show("ERROR", "Please select at least two columns");
                }
            }

            // Create beam location line from column location points
            List<Line> lines = CreateLine(refList.Count, points);

            // get the active level from document
            Level level = doc.GetElement(doc.ActiveView.LevelId) as Level;

            if (App.beamTypeSelection != null)
            {
                FamilySymbol beamSymbol = App.beamTypeSelection as FamilySymbol;

                // For using in the document, symbol has to be activate.
                if (!beamSymbol.IsActive)
                    beamSymbol.Activate();

                if (lines != null)
                {
                    foreach (Line line in lines)
                    {
                        FamilyInstance beam = doc.Create.NewFamilyInstance(line, beamSymbol, level, StructuralType.Beam);
                    }
                }
            }
            else
            {
                TaskDialog.Show("INFO", "Combo box selection is null");
            }
        }

        /// <summary>
        /// Beam Location Line Creation Method
        /// </summary>
        /// <param name="range">Selected Column Number</param>
        /// <param name="m_points">Collection of structural column location points</param>
        /// <returns>Collection of beam location line</returns>
        private List<Line> CreateLine(int range, IList<XYZ> m_points)
        {
            List<Line> lineList = new List<Line>();
            IEnumerable<int> startPoint = Enumerable.Range(0, range - 1);
            IEnumerable<int> endPoint = Enumerable.Range(1, range -1 );

            foreach (int i in startPoint)
            {
                foreach (int t in endPoint)
                {   
                    if (t - i == 1)
                    {
                        Line line = Line.CreateBound(m_points[i], m_points[t]);
                        lineList.Add(line);
                    }
                }
            }
            return lineList;
        }

        /// <summary>
        /// Add beam family symbols to the array list for initialize to the form
        /// </summary>
        /// <param name="doc">Active Revit Document</param>
        /// <param name="beamMap">Want to initalize the beam symbol array list</param>
        public static void beamSymbols(Document doc, ArrayList beamMap)
        {
            // Filtered Element iterator for add structural framing types to the windows form
            FilteredElementIterator i = new FilteredElementCollector(doc).OfClass(typeof(Family)).GetElementIterator();
            while (i.MoveNext())
            {
                Family f = i.Current as Family;
                if (f != null)
                {
                    foreach (ElementId elementId in f.GetFamilySymbolIds())
                    {
                        FamilySymbol familyType = doc.GetElement(elementId) as FamilySymbol;
                        if (null == familyType)
                            continue;
                        if (null == familyType.Category)
                            continue;
                        //add symbols of beams and braces to lists 
                        if ("Structural Framing" == familyType.Category.Name)
                        {
                            beamMap.Add(new SymbolMap(familyType));
                        }
                    }
                }
            }
        }
        #endregion

        #region Symbol Map Class
        /// <summary>
        /// assistant class contains the symbol and its name.
        /// </summary>
        public class SymbolMap
        {
            string m_symbolName = "";
            FamilySymbol m_symbol = null;

            /// <summary>
            /// constructor without parameter is forbidden
            /// </summary>
            private SymbolMap()
            {
            }

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="symbol">family symbol</param>
            public SymbolMap(FamilySymbol symbol)
            {
                m_symbol = symbol;
                string familyName = "";
                if (null != symbol.Family)
                {
                    familyName = symbol.Family.Name;
                }
                m_symbolName = familyName + " : " + symbol.Name;
            }

            /// <summary>
            /// SymbolName property
            /// </summary>
            public string SymbolName
            {
                get
                {
                    return m_symbolName;
                }
            }
            /// <summary>
            /// ElementType property
            /// </summary>
            public FamilySymbol ElementType
            {
                get
                {
                    return m_symbol;
                }
            }
        }
        #endregion
    }
}