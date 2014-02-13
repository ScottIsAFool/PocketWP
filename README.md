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

## Used in the following Pocket clients

[![Squirrel](http://cdn.marketplaceimages.windowsphone.com/v8/images/32f117a4-6b57-4a92-ab42-71aa0fa8503f?imageType=ws_icon_tiny)](http://www.windowsphone.com/s?appid=67821d47-e348-45d5-90d7-0930bea50bee) | [![Pouch](http://cdn.marketplaceimages.windowsphone.com/v8/images/84b7b6d4-3827-4cd8-aead-12e3896b64a0?imageType=ws_icon_tiny)](http://www.windowsphone.com/s?appid=b648e223-119c-4632-a03c-de4f26d4b0cb) | [![Poki](http://cdn.marketplaceimages.windowsphone.com/v8/images/7f549ebf-f83b-48b1-a781-839efe3aa640?imageType=ws_icon_tiny)](http://www.windowsphone.com/s?appid=2532ff45-aa3f-4aba-a266-ed7ec71d47bd) | [![OwlReader](http://cdn.marketplaceimages.windowsphone.com/v8/images/92d70b06-5386-4ffd-99bd-b47aea17aefe?imageType=ws_icon_tiny)](http://www.windowsphone.com/s?appid=3042945a-f1ce-4a0f-99db-800cd8b9169d) | [![Pock8](http://cdn.marketplaceimages.windowsphone.com/v8/images/985d2286-f7d1-40b1-b3e9-f6e72a28f737?imageType=ws_icon_tiny)](http://www.windowsphone.com/s?appid=c00715a7-d2d4-48c1-94e2-2ecc7c1b798b) |
|---|---|---|---|---|
| [Squirrel](http://www.windowsphone.com/s?appid=67821d47-e348-45d5-90d7-0930bea50bee) | [Pouch](http://www.windowsphone.com/s?appid=b648e223-119c-4632-a03c-de4f26d4b0cb) | [Poki](http://www.windowsphone.com/s?appid=2532ff45-aa3f-4aba-a266-ed7ec71d47bd) | [OwlReader](http://www.windowsphone.com/s?appid=3042945a-f1ce-4a0f-99db-800cd8b9169d) | [Pock8](http://www.windowsphone.com/s?appid=c00715a7-d2d4-48c1-94e2-2ecc7c1b798b) |

_and many others (we can't track anything ...)_
