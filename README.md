PocketWP
========

A helper class for apps wanting to create a Pocket client, or wanting to send stuff to a pocket client on Windows Phone. For apps wanting to send items to Pocket, this provides methods to ease launching any Pocket clients that the user has installed. For Pocket client developers, this allows you to easily tap into the shared URI scheme used by other apps. 

## Installation ##
This is available through nuget 

```
Install-Package PocketWP
```

## Sending to Pocket Clients ##
Sending to a Pocket client in its simplest form is as easy as doing:

```c#
PocketHelper.AddItemToPocket("http://www.getpocket.com");
```

You can send multiple items to a Pocket client, each with it's own tags, optional title and tweet ID. 

The helper also allows your app to send a callback URI (`callbackUri` param) to the Pocket client too, so that when it's finished adding the item(s), it can relaunch your app again, keeping the user's flow in tact.

## Use in Pocket Clients ##
This helper will allow you to easily check a url that is passed to your app and detect if it's a Pocket request, and if it is, it will give you an object that you can then use in your own app.

Because this all takes advantage of custom URI schemes, you need to make sure you've registered the common scheme in your app's manifest file.

After the <Token> tag, and if you haven't already got the Extensions tag, add that, then include the following:
```xml
<Protocol Name="pocket" NavUriFragment="encodedLaunchUri=%s" TaskID="_default" />
```

In your UriMapper, you will have code similar to this:

```c#
if (PocketHelper.HasPocketData(uri))
{
    var item = PocketHelper.RetrievePocketData(uri);
    
    // Save the item somewhere for use within your app
    return new Uri("AddItemPage.xaml", UriKind.Relative);
    ...
}
```
