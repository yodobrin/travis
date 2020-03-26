# travis 
A simple demo backend app used as the underline interface for a bot

## The Architecture

### Bot Flow Overview
Bot accepts user data in a set of 4 questions, showcasing how to call an api, how to travers through the data and call back once data has been collected. it works with the Microsoft Health Bot framework.

![Bot Flow](https://user-images.githubusercontent.com/37622785/77684647-ed02f300-6fa2-11ea-8d19-579bd7de199a.png)

#### Import travis
Create your helath bot from the Azure Market Place.
import tavis.json to have a quick start

#### Deploy functions to Azure
using visual studio code to create and deploy a function app, exposing the 3 required APIs for this bot.