using JetBrains.Annotations;

namespace Recommerce.Infrastructure.Exceptions;

/// <summary>
/// Occurs when some input fields have validation errors
/// </summary>
[PublicAPI]
public class ValidationException : BaseException
{
    /// <summary>
    /// Occurs when some input fields have validation errors. Use this constructor when you want to add
    /// validation errors later
    /// </summary>
    public ValidationException()
    {
    }

    /// <summary>
    /// Occurs when some input fields have validation errors. Use this constructor when you want to add
    /// validation errors and throw it right now. 
    /// </summary>
    /// <param name="fieldName">The field that have error</param>
    /// <param name="errors">Error(s) of the field</param>
    public ValidationException(string fieldName, params string[] errors)
    {
        Add(fieldName, errors);
    }

    private Dictionary<string, List<string>> _validations = new();

    /// <summary>
    /// Adds error(s) of a field validation
    /// </summary>
    /// <param name="fieldName">The field that have error</param>
    /// <param name="errors">Error(s) of the field</param>
    public void Add(string fieldName, params string[] errors)
    {
        if (_validations.TryGetValue(fieldName, out var value))
            value.AddRange(errors);
        else
            _validations.Add(fieldName, errors.ToList());
    }

    /// <summary>
    /// Get all added errors
    /// </summary>
    public Dictionary<string, List<string>> GetErrors()
    {
        return _validations;
    }
}