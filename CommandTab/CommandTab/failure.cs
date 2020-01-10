using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Collections.Generic;

namespace CommandTab
{
    public class failure : IFailuresPreprocessor
    {
        public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        {
            // Inside Event Handler Get all warnings
            IList<FailureMessageAccessor> failList = failuresAccessor.GetFailureMessages();
            foreach (FailureMessageAccessor failure in failList)
            {
                // check FailureDefinitionIds against ones that you want to dismiss, 
                FailureDefinitionId failID = failure.GetFailureDefinitionId();
                // prevent Revit from showing Unenclosed room warnings
                if (failID == BuiltInFailures.JoinElementsFailures.CannotKeepJoined)
                {
                    TaskDialog.Show("Failure", "8");
                    failuresAccessor.DeleteWarning(failure);
                }
                if (failID == BuiltInFailures.JoinElementsFailures.JoiningDisjointWarn) 
                {
                    TaskDialog.Show("Failure", "Highlighted elements are joined but do not intersect error. ");
                    failuresAccessor.DeleteWarning(failure);
                }
                if (failID == BuiltInFailures.JoinElementsFailures.CannotJoinElements) 
                {
                    TaskDialog.Show("Failure", "Can't Keep Elements joined error");
                    failuresAccessor.DeleteWarning(failure);
                }
                if (failID == BuiltInFailures.JoinElementsFailures.CannotJoinElementsError)
                {
                    failuresAccessor.DeleteWarning(failure);
                    TaskDialog.Show("q", "1");
                }
                if (failID == BuiltInFailures.JoinElementsFailures.CannotJoinElementsMultiPlaneError)
                {
                    failuresAccessor.DeleteWarning(failure);
                    TaskDialog.Show("q", "2");

                }
                if (failID == BuiltInFailures.JoinElementsFailures.CannotJoinElementsStructuralError)
                {
                    failuresAccessor.DeleteWarning(failure);
                    TaskDialog.Show("q", "3");

                }
                if (failID == BuiltInFailures.JoinElementsFailures.CannotJoinElementsWarn)
                {
                    failuresAccessor.DeleteWarning(failure);
                    TaskDialog.Show("q", "4");
                }
                if (failID == BuiltInFailures.WallJoinFailures.FailedToChangeWallJointsTypeFailure)
                {
                    failuresAccessor.DeleteWarning(failure);
                    TaskDialog.Show("q", "5");
                }
                if (failID == BuiltInFailures.ColumnInsideWallFailures.CopingWarningExtension)
                {
                    failuresAccessor.DeleteWarning(failure);
                    TaskDialog.Show("q", "6");
                }
                if (failID == BuiltInFailures.ColumnInsideWallFailures.CopingWarningOffset)
                {
                    failuresAccessor.DeleteWarning(failure);
                    TaskDialog.Show("q", "7");
                }
                if (failID == BuiltInFailures.JoinElementsFailures.CannotCutJoinedGeometry)
                {
                    failuresAccessor.DeleteWarning(failure);
                    TaskDialog.Show("q", "9");
                }
                if (failID == BuiltInFailures.JoinElementsFailures.CannotJoinElementsStructural)
                {
                    failuresAccessor.DeleteWarning(failure);
                    TaskDialog.Show("q", "9");
                }
            }

            return FailureProcessingResult.Continue;
        }

    }
}
