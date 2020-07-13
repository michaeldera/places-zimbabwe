# Places Zimbabwe

A duct tape api that returns the list of suburbs of a city in Zimbabwe (Bulawayo only at the moment)

Endpoint: `https://placeszw.azurewebsites.net/api/suburbs`

## How to use: 

Query the api with either a `GET` or a `POST` request. 
If you prefer using url parameters, a `GET` request on `https://placeszw.azurewebsites.net/api/surburbs?city=YOUR_CITY_HERE` will return the list of surburbs in your city (Again, at the moment, you city can only be in Bulawayo). 
Alternatively, if you prefer to send JSON objects, `{ city: "YOUR_CITY_HERE" }`, will give the same result. 

If you plan to use this in production please fork it and do a separate deployment. It may change.

## Contributions 

Contribution in terms of issues, code contribution, feature requests are all welcome. 
