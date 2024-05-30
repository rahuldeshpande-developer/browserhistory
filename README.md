## BrowserHistory API Project

This project contains the REST API implementation of a Web API that would return
browser history information to any service invoking it.

### How to use the API

Here are the steps:

1. Open the Solution in Visual Studio
2. Set `BrowserNavigationHistory` project as your Start up project
3. Now click Run on Visual Studio. Visual Studio will now open a new tab on a new browser window.


### API Calls


#### To get all navigation history items
`api/browserhistory`


### To get only a navigation history item of a specific id:

`api/browserhistory/id/45`

### To get navigation history items to a total fixed size of 1000:

`api/browserhistory/batch/`

### To get navigation histories of a certain number, say 5

`api/browserhistory/batch?size=5`

### To paginate, and get the navigation histories of the 4th page, wherein each page has a size of 6

`api/browserhistory/batch?size=6&page=4`
