namespace CommandTab
{
    #region Namespaces
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI.Selection;
    using Autodesk.Revit.DB.Structure;
    using Autodesk.Revit.UI;
    #endregion

    /// <summary>
    /// Geometry helper class for multiple coping external command
    /// </summary>
    public class GeometryHelper
    {
        static StructuralConnectionControl thisForm = new StructuralConnectionControl();

        #region Public Methods

        /// <summary>
        /// Gets the cope distance from windows user form input
        /// </summary>
        public static double getCopeDistance = (thisForm.GetCopeDistance());

        /// <summary>
        /// Select a structural framing element
        /// </summary>
        /// <param name="doc">Revit Document</param>
        /// <param name="uidoc">Active User Interface Document</param>
        /// <returns>Selected element</returns>
        public static Element GetFramingElement(Document doc, UIDocument uidoc)
        {
            // Apply a filter to selection for selecting only valid elements (Structural Framing Elements)
            ISelectionFilter structuralFramingFilter = new StructuralFramingSelectionFilter();

            // Select the Cutter object with framing selection filter
            Element element = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element, structuralFramingFilter, "Please select structural framing"));

            return element;
        }

        /// <summary>
        /// Select structural column
        /// </summary>
        /// <param name="doc">Revit Document</param>
        /// <param name="uidoc">Active User Interface Document</param>
        /// <returns>Selected element</returns>
        public static Element GetStructuralColumnElement(Document doc, UIDocument uidoc)
        {
            // Apply a filter to selection for selecting only valid elements (Structural Framing Elements)
            ISelectionFilter structuralFramingFilter = new StructuralColumnSelectionFilter();

            // Select the Cutter object with framing selection filter
            Element element = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element, structuralFramingFilter, "Please select structural column"));

            return element;
        }

        /// <summary>
        /// Gets the element which intersects with input element
        /// </summary>
        /// <param name="doc">Revit Document</param>
        /// <param name="activeView">Active View</param>
        /// <param name="element">Input element for solid intersect</param>
        /// <returns>List of elements that intersects with input element</returns>
        public static IList<Element> GetSolidIntersects(Document doc, View activeView, Element element)
        {
            Solid solid = GetGeometry(element.get_Geometry(new Options()));
            ElementIntersectsSolidFilter elementIntersectsSolidFilter = new ElementIntersectsSolidFilter(solid);

            // Apply bounding box filter to the Filtered Element Collector
            // This filter collect structural framings which pass the filter and convert to the element
            IList<Element> intersects = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_StructuralFraming).WherePasses(elementIntersectsSolidFilter).ToElements();

            return intersects;
        }

        /// <summary>
        /// Gets the elements which intersects with input element
        /// </summary>
        /// <param name="doc">Acitve Revit Document</param>
        /// <param name="activeView">Active View</param>
        /// <param name="element">Input element for bounding box</param>
        /// <returns>List of elements that intersects with input element</returns>
        public static IList<Element> GetBouindingBoxIntersect(Document doc, View activeView, Element element)
        {
            BoundingBoxXYZ boundingBox = element.get_BoundingBox(activeView);
            Outline outline = new Outline(boundingBox.Min, boundingBox.Max);

            BoundingBoxIntersectsFilter ıntersectsFilter = new BoundingBoxIntersectsFilter(outline);

            // Apply bounding box filter to the Filtered Element Collector
            // This filter collect structural framings which pass the filter and convert to the element
            IList<Element> intersects = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_StructuralFraming).WherePasses(ıntersectsFilter).ToElements();

            return intersects;
        }

        /// <summary>
        /// Gets the framing elements which intersects with input structural column
        /// </summary>
        /// <param name="doc">Acitve Revit Document</param>
        /// <param name="activeView">Active View</param>
        /// <param name="element">Input element for bounding box</param>
        /// <returns></returns>
        public static IList<Element> GetIntersectsStructuralColumn(Document doc, View activeView, Element element)
        {
            Solid solid = GetGeometry(element.get_Geometry(new Options()));
            ElementIntersectsSolidFilter elementIntersectsSolidFilter = new ElementIntersectsSolidFilter(solid);

            // Apply bounding box filter to the Filtered Element Collector
            // This filter collect structural framings which pass the filter and convert to the element
            IList<Element> intersects = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_StructuralFraming).WherePasses(elementIntersectsSolidFilter).ToElements();

            return intersects;
        }

        /// <summary>
        /// Cope the intersected elements that intersect with cutter element
        /// </summary>
        /// <param name="doc">Revit Document</param>
        /// <param name="intersects">Elements that intersects with input element(cutter)</param>
        /// <param name="cutter">Cutter, selected input element</param>
        /// <param name="reverse">Set true if wants to cut the intersects</param>
        public static void CopeIntersects(Document doc, IList<Element> intersects, Element cutter, bool reverse)
        {
            foreach (Element el in intersects)
            {
                FamilyInstance familyInstance = el as FamilyInstance;

                // Set the cope distance of cutted elements from given value
                Parameter copeDistanceParam = el.get_Parameter(BuiltInParameter.STRUCTURAL_COPING_DISTANCE);
                copeDistanceParam.Set(getCopeDistance);

                try
                {
                    if (familyInstance.Id != cutter.Id && reverse == true && !(cutter as FamilyInstance).GetCopingIds().Contains(familyInstance.Id))
                    {
                        var ty = (familyInstance.Location as LocationCurve).Curve ;
                        XYZ start = ty.GetEndPoint(0);
                        XYZ end = ty.GetEndPoint(1);

                        double angle = (end.Y - start.Y) / (end.X - start.X);

                        if (angle <80)
                            // Cope the intersects
                            familyInstance.AddCoping(cutter as FamilyInstance);
                    }

                    if (familyInstance.Id != cutter.Id && reverse == false && !(cutter as FamilyInstance).GetCopingIds().Contains(familyInstance.Id) )
                    {
                        var ty = (familyInstance.Location as LocationCurve).Curve;
                        XYZ start = ty.GetEndPoint(0);
                        XYZ end = ty.GetEndPoint(1);

                        double angle = (end.Y - start.Y) / (end.X - start.X);

                        if (angle < 80)
                            // Cope the selected element
                            (cutter as FamilyInstance).AddCoping(familyInstance);
                    }
                }
                catch (Exception e)
                {
                    TaskDialog.Show("ERROR", e.Message);
                }
            }
        }

        /// <summary>
        /// Return the solid geometry of an element.  
        /// </summary>
        /// <remarks>Makes an assumption that each element consists of only one 
        /// positive-volume solid, and returns the first one it finds.</remarks>
        public static Solid GetGeometry(GeometryElement geomElem)
        {
            foreach (GeometryObject geomObj in geomElem)
            {
                // Walls and some columns will have a solid directly in its geometry
                if (geomObj is Solid solid)
                {
                    if (solid.Volume > 0)
                        return solid;
                }

                // Some columns will have a instance pointing to symbol geometry
                if (geomObj is GeometryInstance geomInst)
                {
                    // Instance geometry is obtained so that the intersection works as
                    // expected without requiring transformation
                    GeometryElement instElem = geomInst.GetInstanceGeometry();

                    foreach (GeometryObject instObj in instElem)
                    {
                        if (instObj is Solid solidq)
                        {
                            if (solidq.Volume > 0)
                                return solidq;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Convert feet unit to milimetric unit
        /// </summary>
        /// <param name="input">Desired to be converted value</param>
        /// <returns></returns>
        public static double FeetToMilimeter(double input)
        {
            return input / 304.8;
        }

        #endregion
    }

    /// <summary>
    /// Selection filter for structural framing
    /// </summary>
    public class StructuralFramingSelectionFilter : ISelectionFilter
    {
        /// <summary>
        /// Allow selection of elements
        /// </summary>
        /// <param name="e">Selected Elements</param>
        /// <returns></returns>
        public bool AllowElement(Element e)
        {
            FamilyInstance fIns = e as FamilyInstance;
            if (fIns.StructuralType == StructuralType.Beam ||
                fIns.StructuralType == StructuralType.Brace)
                return true;
            return false;
        }
        /// <summary>
        /// Doesn't allow selection of references
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool AllowReference(Reference reference, XYZ position) => false;
    }

    /// <summary>
    /// Selection filter for structural columns
    /// </summary>
    public class StructuralColumnSelectionFilter : ISelectionFilter
    {
        /// <summary>
        /// Allow Selection of Elements
        /// </summary>
        /// <param name="e">Selected Elements</param>
        /// <returns></returns>
        public bool AllowElement(Element e)
        {
            FamilyInstance fIns = e as FamilyInstance;
            if (fIns.StructuralType == StructuralType.Column)
                return true;
            return false;
        }
        /// <summary>
        /// Allow Selection of References
        /// </summary>
        /// <param name="reference">Selected Reference</param>
        /// <param name="position">Selected Point</param>
        /// <returns></returns>
        public bool AllowReference(Reference reference, XYZ position) => false;
    }
}
