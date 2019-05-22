namespace budgettracker.data.Converters
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
        /// data representation that can matches the schema of the
        /// model in the database exactly.
        /// </p>
        /// </summary>
        D ToDataModel(B businessObject);

        /// <summary>
        /// <p>
        /// Converts a data representation of an object to its
        /// domain representation as a POCO.
        /// </p>
        /// </summary>
        B ToBusinessModel(D dataModel);
    }
}