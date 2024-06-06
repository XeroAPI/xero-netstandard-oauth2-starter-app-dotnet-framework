# Xero NetStandard OAuth 2.0 Starter App with .NET Framework v4.6.1

This is a starter app build with .NET Framework v4.6.1 MVC to demonstrate Xero OAuth 2.0 Client Authentication & OAuth 2.0 APIs. 

![XeroNetStandard-dotnet-framework](https://user-images.githubusercontent.com/41350731/76382748-d149fb00-63ad-11ea-83eb-79f814d49878.gif)


Its functions include:

- connect & reconnect to xero
- storing Xero token in a .json file
- refresh Xero access token on expiry
- read organisation information from /organisation endpoint
- read contacts information from /contacts endpoint
- create a new contact in Xero

You can connect this starter app to an actual Xero organisation and make real API calls. Please use your Demo Company organisation for your testing. [Here](https://central.xero.com/s/article/Use-the-demo-company) is how to turn it on. 

### Create a Xero app
You will need your Xero app credentials created to run this demo app. 
To obtain your API keys, follow these steps:

* Create a [free Xero user account](https://www.xero.com/us/signup/api/) (if you don't have one)
* Login to [Xero developer center](https://developer.xero.com/myapps)
* Click "New App" link
* Enter your App name, company url, privacy policy url.
* Enter the redirect URI (your callback url - i.e. `https://localhost:5001/Authorization/Callback`)
* Agree to terms and condition and click "Create App".
* Click "Generate a secret" button.
* Copy your client id and client secret and save for use later.
* Click the "Save" button. You secret is now hidden.

### Download the code
Clone this repo to your local drive or open with GitHub desktop client.

### Configure your API Keys
In /NetStandardApp_net461/Web.config, you should populate your Xero app credentials as such: 

```
    <add key="XeroClientId" value="YOUR_XERO_APP_CLIENT_ID" />
    <add key="XeroClientSecret" value="YOUR_XERO_APP_CLIENT_SECRET" />
    <add key="XeroCallbackUri" value="https://localhost:44300/Authorization/Callback" />
    <add key="XeroScope" value="openid profile email files accounting.transactions accounting.transactions.read accounting.reports.read accounting.journals.read accounting.settings accounting.settings.read accounting.contacts accounting.contacts.read accounting.attachments accounting.attachments.read offline_access" />
    <add key="XeroState" value="my_net461_state" />
```

Note that you will have to have a state. The XeroCallbackUri has to be exactly the same as redirect URI you put in Xero developer portal letter by letter. 

## Getting started with Visual Studio 2019 (windows version)
You can run it using [Visual Studio 2019 Community Edition](https://visualstudio.microsoft.com/vs/) on a windows PC or virtual machine . 
 
### Install Visual Studio 2019
[Download](https://visualstudio.microsoft.com/vs/) an install VS 2019 Community edition and open the solution file in the project root directory _NetStandardApp_net461.sln_. 

![image](https://user-images.githubusercontent.com/41350731/76382787-f6d70480-63ad-11ea-8763-bfaf34663d44.png)


### Build the project 
Clean and Rebuild the solution before running the project. (This is to prevent a bug with the Roslyn compiler which can happen when cloning the project from GitHub.)
Then go back to Explorer and press F5 (or go to _Debug_ > _Start Debugging_). You might need to choose a default ISS Express browser that you would like to use to test. I am using MS Edge. 

![image](https://user-images.githubusercontent.com/41350731/76382816-0bb39800-63ae-11ea-990f-ccaca3563b78.png)

You should be directed to your default browser with https://localhost:44300/ already open. 

![image](https://user-images.githubusercontent.com/41350731/76382872-2be35700-63ae-11ea-97d1-51cf41d67c93.png)

Start your Testing. 

## Some explanation of the code

![image](https://user-images.githubusercontent.com/41350731/76382899-40275400-63ae-11ea-88a3-c906841b5f50.png)

There are three controllers in this MVC app:

**HomeController**
- checks if there is a xerotoken.json, and 
- passes a boolean firstTimeConnection to view to control the display of buttons. 

**AuthorizationController**
- reads Web.config and instantiates XeroConfiguration called XeroConfig
- instantiates httpClientFactory via the Microsoft.Extensions.DependencyInjection package
- on /Authorization/, redirects user to Xero OAuth for authentication & authorization
- receives callback on /Authorization/Callback request Xero token
- get connected tenants (organisations) 
- store token via a static method TokenUtilities.StoreToken(xeroToken);

**OrganisationInfoController**
- gets or refreshes stored token
- make api call to organisation endpoint 
- displays in view

**ContactInfoController** 
- gets or refreshes stored token 
- make api call to contacts endpoint
- displays in view
- static view Create.cshtml creates a webform and POST contact info to Create() action, and
- makes an create operation to contacts endpoint 

Xero token is stored in a JSON file in the root of the project "xerotoken.json". The app serialise and deserialise with the static class functions in /Utilities/TokenUtilities.cs. 

## License

This software is published under the [MIT License](http://en.wikipedia.org/wiki/MIT_License).

	Copyright (c) 2020 Xero Limited

	Permission is hereby granted, free of charge, to any person
	obtaining a copy of this software and associated documentation
	files (the "Software"), to deal in the Software without
	restriction, including without limitation the rights to use,
	copy, modify, merge, publish, distribute, sublicense, and/or sell
	copies of the Software, and to permit persons to whom the
	Software is furnished to do so, subject to the following
	conditions:

	The above copyright notice and this permission notice shall be
	included in all copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
	EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
	OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
	NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
	HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
	WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
	FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
	OTHER DEALINGS IN THE SOFTWARE.


