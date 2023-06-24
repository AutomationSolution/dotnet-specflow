using System.Diagnostics.CodeAnalysis;

namespace AutomationFramework.Utilities.Attributes;

/// <summary>
///     Specifies a locator for a enum.
/// </summary>
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class LocatorAttribute : Attribute
{
    /// <summary>
    ///     Specifies the default value for the <see cref='LocatorAttribute' />,
    ///     which is an empty string (""). This <see langword='static' /> field is read-only.
    /// </summary>
    public static readonly LocatorAttribute Default = new();

    public LocatorAttribute() : this(string.Empty)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref='LocatorAttribute' />
    ///     class.
    /// </summary>
    public LocatorAttribute(string locator, string? key = null)
    {
        LocatorValue = locator;
        LocatorKey = key;
    }

    public string Locator => LocatorValue;
    public string? Key => LocatorKey;

    protected string? LocatorKey { get; }
    protected string LocatorValue { get; }

    public override object TypeId => this;

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is LocatorAttribute other && other.Locator == Locator;

    public override int GetHashCode() => Locator.GetHashCode() ^ Key?.GetHashCode() ?? 0;

    public override bool IsDefaultAttribute() => Equals(Default);
}
