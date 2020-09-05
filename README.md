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

[Subscribe(ThreadMode.MAIN)]
private void HandleEvent(ShootMessage message)
{
    /* do something when shooting on main thread */
}

[Subscribe]
private void HandleEventSync(ShootMessage message)
{
    /* do something when shooting in the same thread */
}

[Subscribe(ThreadMode.BACKGROUND)]
private void HandleEventAsync(ShootMessage message)
{
    /* do something when shooting on background thread */
}

public void Update()
{
    // Trigger event.
    EventBus.Default.Post(new ShootMessage(/*args*/));
}
```

