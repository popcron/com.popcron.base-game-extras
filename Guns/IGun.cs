#nullable enable

namespace BaseGame
{
    public interface IGun
    {
        bool TryToShoot(ref float cooldownTimer);
    }
}