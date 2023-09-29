# popcron/com.popcron.base-game-extras
This was an attempt at making something like a "lib" for my game projects, code that I'd want to use and extend outwards from.
Archived because this is very easy to do, and is something that should happen internally in projects.

[Successor this repo](https://github.com/popcron-games/com.popcron-games.lib)

## Platformer movement ability
For making your player move like in a platformer game
## Jumping ability
Requires a `GroundedMovement<>` component to be on the player, or anything in the inventory that implements `IGroundState`.
### Health ability
## Looking around abilities
`LookingAround2D` will make the player look towards the mouse position, and `FirstPersonLookingAround` will make the player look around like in an FPS game.
## `GunShooting` ability
By default, it will try to shoot the first gun it can find in the inventory assuming it inherits from `ProjectileGun`
## `CollisionEventListener` component
Makes it easy to listen for collision events as long as a reference to the game object exists
```cs
private void Enable()
{
    gameObject.GetOrAddComponent<CollisionEventListener>().onCollisionEnter += OnCollisionEnter;
}

private void Disable()
{
    gameObject.GetComponent<CollisionEventListener>().onCollisionEnter -= OnCollisionEnter;
}

private void OnCollisionEnter(Collision collision)
{
    
}
```
