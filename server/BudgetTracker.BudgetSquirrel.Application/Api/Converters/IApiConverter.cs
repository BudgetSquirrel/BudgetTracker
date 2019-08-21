using BudgetTracker.Business.Api.Messages;
using BudgetTracker.Business.Auth;

namespace BudgetTracker.Business.Api.Converters
{
    /// <summary>
    /// <para>
    /// Converts models to their corresponding <see cref="IApiMessage" />
    /// implementation or vice versa. This provides methods to perform this
    /// conversion.
    /// </para>
    /// <para>
    /// This converter assumes that a domain model is associated with both
    /// a request and response contract class. Each may be slightly different.
    /// for example, a UserResponseApiMessage may not contain the password
    /// whereas the UserRequestApiMessage would.
    /// </para>
    /// <para>
    /// M: The model class for this converter (ex. User).
    /// </para>
    /// <para>
    /// Q: The <see cref="IApiMessage" /> implementation for this converter (ex. UserApiMessage)
    /// that represents a request.
    /// </para>
    /// <para>
    /// R: The <see cref="IApiMessage" /> implementation for this converter (ex. UserApiMessage)
    /// that represents a response.
    /// </para>
    /// </summary>
    public interface IApiConverter<M, Q, R>
        where Q : IApiMessage
        where R : IApiMessage
    {
        /// <summary>
        /// <para>
        /// Converts and returns the main model object to represent the contract.
        /// For example, if contract represents a User, this will return a direct
        /// representation of that user contract as a User model. This new instance
        /// will reflect the state of the user that the contract represents.
        /// </para>
        /// <para>
        /// The contract from which to convert must be a request contract.
        /// </para>
        /// </summary>
        M ToModel(Q requestMessage);

        /// <summary>
        /// <para>
        /// Converts and returns the main model object to represent the contract.
        /// For example, if contract represents a User, this will return a direct
        /// representation of that user contract as a User model. This new instance
        /// will reflect the state of the user that the contract represents.
        /// </para>
        /// <para>
        /// The contract from which to convert must be a response contract.
        /// </para>
        /// </summary>
        M ToModel(R responseMessage);

        /// <summary>
        /// <para>
        /// Converts and returns the contract to represent the model instance.
        /// For example, if model represents a User, this will return a direct
        /// representation of that user model as a User contract. This new instance
        /// will reflect the state of the user that the model represents.
        /// </para>
        /// <para>
        /// The contract which will be returned will be a request contract.
        /// </para>
        /// </summary>
        Q ToRequestMessage(M model);

        /// <summary>
        /// <para>
        /// Converts and returns the contract to represent the model instance.
        /// For example, if model represents a User, this will return a direct
        /// representation of that user model as a User contract. This new instance
        /// will reflect the state of the user that the model represents.
        /// </para>
        /// <para>
        /// The contract which will be returned will be a response contract.
        /// </para>
        /// </summary>
        R ToResponseMessage(M model);
    }
}
