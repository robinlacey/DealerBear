# 🎰:bear:



Like any card dealer, **Dealer Bear** (🎰:bear:) takes requests from players, draws new cards and reports the status of the current game. Dealer Bear communicates with [Pack Bear]() (:black_joker::bear:) to draw cards from the card pack and [Game Bear]()  (:floppy_disk::bear:)  save current game status.

The game rules are similar to [Reigns](https://reignsgame.com/). Players are dealt a card. They can choose from a number of options. Choosing an option with modify player stats and can add/remove cards from the players deck. The game is over when any of the player stats goes over or under a threshold. 

The implementation of the game rules (stats, thresholds, cards etc) are data driven and should be of no concern to Dealer Bear. Dealer Bear responds to two simple message commands `Get Current Game` and `Play Card` with a unique `SessionID` string.  

Any front end (web, mobile, console etc) should render current card data and provide a method of picking an option. 

 🎰:bear: is still work in progress and is missing some key functionality. Feel free to drop me a line if you have any questions.



To run:  

Dealer Bear uses `MassTransit` with `RabbitMQ` by default. This could be easily changed by creating a different `IPublishMessageAdaptor `

- Set the `RABBITMQ_HOST` environment variable along with `RABBITMQ_USERNAME` and `RABBITMQ_PASSWORD` 
- Build and run the `Dockerfile`



**Note**: to avoid coupling the :bear:'s do not share a library. If you're adding new messages to the services make certain the recipient :bear: has the corresponding message data structure.  
