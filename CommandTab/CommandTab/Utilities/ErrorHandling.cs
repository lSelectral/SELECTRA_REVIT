namespace CommandTab
{
    using Autodesk.Revit.DB;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Public Error Handlic Helper Class
    /// </summary>
    public class ErrorHandling : IFailuresProcessor
    {
        /// <summary>
        /// This method is being called in case of exception or document destruction to dismiss any possible pending failure UI that may have left on the screen 
        /// </summary>
        /// <param name="document">Document for which pending failures processing UI should be dismissed </param>
        public void Dismiss(Document document) { }

        /// <summary>
        /// Method that Revit will invoke to process failures at the end of transaction. 
        /// </summary>
        /// <param name="failuresAccessor">Provides all necessary data to perform the resolution of failures.</param>
        /// <returns></returns>
        public FailureProcessingResult ProcessFailures(FailuresAccessor failuresAccessor)
        {
            IList<FailureMessageAccessor> fmas = failuresAccessor.GetFailureMessages();
            if (fmas.Count == 0)
            {
                return FailureProcessingResult.Continue;
            }

            foreach (FailureMessageAccessor fma in fmas)
            {
                FailureDefinitionId id = fma.GetFailureDefinitionId();
                if ( id == BuiltInFailures.JoinElementsFailures.CannotKeepWallJoinToRoof )
                {
                    failuresAccessor.ResolveFailure(fma);
                }
            }
            return FailureProcessingResult.ProceedWithCommit;


            return FailureProcessingResult.Continue;
        }
    }
}
