# Extras
### Platformer movement ability
### Jumping ability
### Health ability
### Looking around abilities
`LookingAround2D` will make the player look towards the mouse position, and `FirstPersonLookingAround` will make the player look around like in an FPS game.
### `GunShooting` ability
By default, it will try to shoot the first gun it can find in the inventory assuming it inherits from `ProjectileGun`
### `CollisionEventListener` component
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