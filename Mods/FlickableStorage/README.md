# [Flickable Storage](https://steamcommunity.com/sharedfiles/filedetails/?id=2497907804)

![Image](https://img.litet.net/logos/Self-Info.png)

Storage-buildings and stockpiles can be set to one of four settings



-  Storage on - Works as normal
-  Storage off - Will not accept or release any items
-  Accept only - Pawns can only store items, not retrieve them
-  Retrieve only - Pawns can only retrieve items, not store them



## Examples

Your prison cells have a storage unit for food, but when there is no prisoners. You want to stop hauling food for it without changing storage filters or priorities. You turn the shelf to retrieve only when there are no prisoners.

You want to have a stack of steel left at all times for emergency trap-building. You set one shelf to store only with highest priority. 

## Technical

The blocking of storing new items is done by modifying the Accept-check for the storage.
The blocking of retrieving items is done by modifying the Forbidden check for the items in the storage. Pawns with mental break ignores the forbidden check so they will still be able to fetch items.

Should work with all mods that add storage buildings using the IHaulDestination class. Tested for example with [LWM's Deep Storage](https://steamcommunity.com/sharedfiles/filedetails/?id=1617282896)
Will also change flick-setting in all linked storages.

NOTE: Chunks cannot be forbidden so are ignored by this mods settings. You can fix this by adding [[XND] Forbiddable Debris]( https://steamcommunity.com/sharedfiles/filedetails/?id=2054653797)

## Credits

notfood: Support for [Multiplayer](https://steamcommunity.com/sharedfiles/filedetails/?id=1752864297), optimization and generalization
Waveshaper: Debugging
HawnHan: Chinese translation
Velcroboy333: Original idea
	
![Image](https://img.litet.net/logos/Self-SupportAndDonations.png)

Since modding is just a hobby for me I expect no donations to keep modding. If you still want to show your support you can gift me anything from my [Wishlist](https://store.steampowered.com/wishlist/id/Mlie) or buy me a cup of tea.

[![Image](https://img.litet.net/logos/Kofi-Small.png)](https://ko-fi.com/G2G55DDYD)

![Image](https://img.litet.net/logos/Self-ReportingIssues.png)



-  See if the the error persists if you just have this mod and its requirements active.
-  If not, try adding your other mods until it happens again.
-  Always post your log using the [Log Uploader](https://steamcommunity.com/sharedfiles/filedetails/?id=2873415404)
-  For best support, please use the Discord-channel for error-reporting.
-  Do not report errors by making a discussion-thread, I get no notification of that.
-  If you have the solution for a problem, please post it to the GitHub repository.
-  Use [RimSort](https://github.com/RimSort/RimSort/releases/latest) to sort your mods

 

[![Image](https://img.shields.io/github/v/release/emipa606/FlickableStorage?label=latest%20version&style=plastic&labelColor=0070cd&color=white)](https://steamcommunity.com/sharedfiles/filedetails/changelog/2497907804) | tags: storage,  settings
