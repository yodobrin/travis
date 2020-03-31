# travis 
A simple demo backend app used as the underline interface for a bot

## The Architecture

![High Level View](https://user-images.githubusercontent.com/37622785/77843549-e5ac3700-71a6-11ea-9bd3-a330e7c9a831.png)

### Azure Functions
Azure functions were selected to align with serverless compute paradigm. All functions must return valid JSON.

#### GetCities
Modify the hard coded return value to specific query.

#### GetRoutes
Modify the hard coded return value to specific query, use the passed values for better filtering.

#### UpdateDetails
Provided JSON object with the gathered information by the bot, currently saves the entry as a single blob. Modify the implementation to a selected DB.

### Storage : Either Blob or any other storage solution
Currently using only blob as a simple reference impelmentation  

### Bot Flow Overview
Bot accepts user data in a set of 4 questions, showcasing how to call an api, how to travers through the data and call back once data has been collected. it works with the Microsoft Health Bot framework.

![Bot Flow](https://user-images.githubusercontent.com/37622785/77684647-ed02f300-6fa2-11ea-8d19-579bd7de199a.png)

[Security](https://docs.microsoft.com/en-us/healthbot/handoff-teams)

[Variables](https://docs.microsoft.com/en-us/healthbot/scenario-authoring/instance-variables)

#### Import travis
Create your health bot from the Azure Market Place.
import tavis.json to have a quick start

#### Deploy functions to Azure
using visual studio code to create and deploy a function app, exposing the 3 required APIs for this bot.