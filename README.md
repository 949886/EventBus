# EventBus

Simple implementation just like java EventBus.

``` C#
private void Start()
{
    EventBus.Default.Register(this);
}

private void OnDestroy()
{
    EventBus.Default.Unregister(this);
}

[Subscribe]
private void HandleEvent(ShootMessage message)
{
    /* do something when shooting */
}
```

