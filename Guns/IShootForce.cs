#nullable enable
using System.Collections.Generic;

namespace BaseGame
{
    public interface IShootForce : IVariables
    {
        Variable<float> Value { get; }

        IEnumerable<(string, BaseVariable)> IVariables.GetVariables()
        {
            yield return ("shootForce", Value);
        }
    }
}