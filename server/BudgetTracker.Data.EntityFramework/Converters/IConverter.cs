using System.Collections.Generic;

namespace BudgetTracker.Data.EntityFramework.Converters
{
    /// <summary>
    /// <p>
    /// Converts back and fourth between data models and domain models.
    /// </p>
    /// </summary>
    public interface IConverter<B, D>
        where B : new()
        where D : new()
    {
        /// <summary>
        /// <p>
        /// Converts a domain representation of an object to its
        /// data representation that matches the schema of the
        /// model in the database exactly.
        /// </p>
        /// </summary>
        D ToDataModel(B businessObject);

        /// <summary>
        /// <p>
        /// Converts the domain representations of the objects to their
        /// data representations that matche the schema of the
        /// model in the database exactly.
        /// </p>
        /// </summary>
        List<D> ToDataModels(List<B> businessModels);

        /// <summary>
        /// <p>
        /// Converts a data representation of an object to its
        /// domain representation as a POCO.
        /// </p>
        /// </summary>
        B ToBusinessModel(D dataModel);

        /// <summary>
        /// <p>
        /// Converts a data representations of the objects to their
        /// domain representations as POCOs.
        /// </p>
        /// </summary>
        List<B> ToBusinessModels(List<D> dataModels);
    }
}
