namespace CommandTab
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.DB.Events;
    #endregion

    /// <summary>
    /// Rotate Multiple Element External Event Class
    /// </summary>
    public class RotateMultiExternalEvent : IExternalEventHandler
    {
        #region External Event Handler Interface

        /// <summary>
        ///   A method to identify this External Event Handler
        /// </summary>
        public string GetName()
        {
            return "Rotate Multiple Elements Event";
        }

        /// <summary>
        ///   The top method of the event handler.
        /// </summary>
        /// <remarks>
        ///   This is called by Revit after the corresponding
        ///   external event was raised (by the modeless form)
        ///   and Revit reached the time at which it could call
        ///   the event's handler (i.e. this object)
        /// </remarks>
        /// 
        public void Execute(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = uidoc.Document;

            RotateMultiExternalEvent rotateMultiExternalEvent = new RotateMultiExternalEvent();
            ExternalEvent exEvent = ExternalEvent.Create(rotateMultiExternalEvent);

            Rotating_Form rotating_Form = new Rotating_Form(exEvent, rotateMultiExternalEvent);

            try
            {
                using (Transaction t = new Transaction(doc))
                {
                    try
                    {
                        //Provide access to make change in document
                        t.Start("Rotate Elements");

                        // Collect selected elements
                        ICollection<ElementId> selectedElements = uidoc.Selection.GetElementIds();

                        RotateMethod(doc, selectedElements, App.angleX, App.angleY, App.angleZ);

                        t.Commit();
                        App.thisApp.WakeFormUp_RotatingForm();
                    }
                    catch (Exception e)
                    {
                        TaskDialog.Show("ERROR", e.Message + "\n" + e.Source);
                    }
                }
            }
            finally
            {
                App.thisApp.WakeFormUp_RotatingForm();
            }
            return;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Rotate Multiple Elements its own axis
        /// </summary>
        /// <param name="doc">Active Revit Document</param>
        /// <param name="ids">Collection of selected element IDs</param>
        /// <param name="angleX">Rotation angle of X axis</param>
        /// <param name="angleY">Rotation angle of Y axis</param>
        /// <param name="angleZ">Rotation angle of Z axis</param>
        public void RotateMethod(Document doc, ICollection<ElementId> ids, double angleX, double angleY, double angleZ)
        {
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    Element el = doc.GetElement(id);
                    Location loc = el.Location;

                    if ((el.Location as LocationCurve) != null)
                    {
                        LocationCurve locationCurve = loc as LocationCurve;
                        XYZ midPoint = (locationCurve.Curve.GetEndPoint(0) + locationCurve.Curve.GetEndPoint(1)) / 2;

                        XYZ directionX = midPoint.Add(XYZ.BasisX);
                        XYZ directionY = midPoint.Add(XYZ.BasisY);
                        XYZ directionZ = midPoint.Add(XYZ.BasisZ);

                        Line axisX = Line.CreateBound(midPoint, directionX);
                        Line axisY = Line.CreateBound(midPoint, directionY);
                        Line axisZ = Line.CreateBound(midPoint, directionZ);

                        if (locationCurve != null && angleX > 0)
                        {
                            locationCurve.Rotate(axisX, DegreeToRadian(angleX));
                        }

                        if (locationCurve != null && angleY > 0)
                        {
                            locationCurve.Rotate(axisY, DegreeToRadian(angleY));
                        }

                        if (locationCurve != null && angleZ > 0)
                        {
                            locationCurve.Rotate(axisZ, DegreeToRadian(angleZ));
                        }
                    }

                    if (el.Location as LocationPoint != null)
                    {
                        LocationPoint locationPoint = el.Location as LocationPoint;

                        XYZ m_directionX = (locationPoint.Point.Add(XYZ.BasisX));
                        XYZ m_directionY = (locationPoint.Point.Add(XYZ.BasisY));
                        XYZ m_directionZ = (locationPoint.Point.Add(XYZ.BasisZ));

                        Line m_axisX = Line.CreateBound(locationPoint.Point, m_directionX);
                        Line m_axisY = Line.CreateBound(locationPoint.Point, m_directionY);
                        Line m_axisZ = Line.CreateBound(locationPoint.Point, m_directionZ);

                        if (locationPoint != null && angleX > 0)
                        {
                            locationPoint.Rotate(m_axisX, DegreeToRadian(angleX));
                        }

                        if (locationPoint != null && angleY > 0)
                        {
                            locationPoint.Rotate(m_axisY, DegreeToRadian(angleY));
                        }

                        if (locationPoint != null && angleZ > 0)
                        {
                            locationPoint.Rotate(m_axisZ, DegreeToRadian(angleZ));
                        }
                    }
                }
            }
            else
            {
                TaskDialog.Show("ELEMENT SELECTION", "PLEASE SELECT AT LEAST 1 ELEMENT");
            }
        }

        /// <summary>
        /// Convert input degree to radian.
        /// </summary>
        /// <param name="angle">Value wants the convert to the radian</param>
        /// <returns>Return the angle degree as a radian</returns>
        public static double DegreeToRadian(double angle)
        {
            return angle * Math.PI / 180;
        }
    }

    #endregion
}
