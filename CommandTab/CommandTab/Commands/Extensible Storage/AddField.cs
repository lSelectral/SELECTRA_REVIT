namespace CommandTab
{
    #region Namespaces
    using System;
    using System.Linq;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.UI.Selection;
    using Autodesk.Revit.DB.ExtensibleStorage;
    using RVTDoc = Autodesk.Revit.DB.Document;
    using RVTransaction = Autodesk.Revit.DB.Transaction;
    #endregion

    /// <summary>
    /// Sample Class
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class AddField : IExternalCommand
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

            using (RVTransaction t = new RVTransaction(doc))
            {
                t.Start("Schme Build");

                try
                {
                    string schemaName = "PurchasingInfo";

                    // Check to see if schema exist
                    Schema mySchema = Schema.ListSchemas().FirstOrDefault(q => q.SchemaName == schemaName);

                    if (mySchema == null)
                    {
                        Guid guid = new Guid("95E4FC70-5B90-49D5-9672-A049BE3A0257");
                        SchemaBuilder sb = new SchemaBuilder(guid);
                        sb.SetSchemaName(schemaName);

                        // Add fields (properties) to schema, defining the name and data type for each field
                        FieldBuilder fbCost = sb.AddSimpleField("Cost", typeof(Int32));
                        FieldBuilder fbShippingWeight = sb.AddSimpleField("ShippingWeight", typeof(double));

                        // Set unit type of shipping weight
                        fbShippingWeight.SetUnitType(UnitType.UT_Mass);

                        sb.SetReadAccessLevel(AccessLevel.Public);
                        sb.SetWriteAccessLevel(AccessLevel.Public);
                        sb.SetVendorId("Selectra");

                        // Conclude the creation of schema
                        mySchema = sb.Finish();
                    }

                    // Create entity of schema (like instance of class)
                    Entity myEntity = new Entity(mySchema);

                    // Get fields from the schema
                    Field myCostField = mySchema.GetField("Cost");
                    Field myShippingWeightField = mySchema.GetField("ShippingWeight");

                    // Set value for this entity for each field
                    myEntity.Set<Int32>(myCostField, 125);
                    myEntity.Set<double>(myShippingWeightField, 1, DisplayUnitType.DUT_KILOGRAMS_MASS);

                    // Prompt user to select an element 
                    Element element = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element));

                    // Store the data in element ( Need transaction )
                    element.SetEntity(myEntity);

                    int cost = myEntity.Get<Int32>("Cost");
                    double weight = myEntity.Get<double>("ShippingWeight", DisplayUnitType.DUT_KILOGRAMS_MASS);

                    TaskDialog.Show(schemaName, "Cost: " + cost.ToString() + "\n" + "Weight: " + weight.ToString());
                }
                catch (Exception e)
                {
                    TaskDialog.Show("ERROR", e.Message);
                }

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
            return typeof(AddField).Namespace + "." + nameof(AddField);
        }
    }
}
