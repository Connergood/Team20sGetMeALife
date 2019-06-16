# GetMeALife

Hello and welcome to our App. We hope you enjoy your new life!

HOW TO RUN:
There will be an .apk file uploaded to the github in the APKs folder. Download it to your phone, and "run" it from the downloads to install it to your phone.

WHAT IS THIS PIECE OF LOVELINESS CALLED GET ME A LIFE:
Get Me A Life is based around not only the prompt given at the start of the hackathon, but the idea of pulling users out to events near them by giving them multiple
choices related to their preferences. On first use, a user would simply get nearby events, and would choose the events that they want to go to by confirming them in the app.
As the user confirms more and more, their "preference" profile would be built up - and there would be a separate option for that user to search based more on their preferences,
the "Suggest Me A Life" option. The Another Life option is based around the idea that the user might want to go against their preferences - so that option would look at events
not within user preferences.

There were plans for a decision tree based option, where users would select various options like "Active/Spectator" or "Group/Solo" or "Family Oriented/Non Family Oriented",
and events would be filtered out until there was a curated list. However, we were unable to implement that at this time.

WHAT DID WE USE:
We used .NET framework and Xamarin forms as the platform for a Android Application. Using GraphQL, we build an API service to query our MYSQL database for information - we also 
integrated with Bored API to augment our group event data with more solo oriented events.