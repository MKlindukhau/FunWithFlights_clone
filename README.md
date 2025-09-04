# Requirements
- The switch between the existing system and the new one should be as incremental and as seamless as possible. This means that traffic would be incrementally switched from the old to the new system while data migration would be also required.
- As new data providers are always emerging on-boarding of new data sources should be automated as much as possible (same flight / seat offering can come from multiple sources with different prices we should always show the best for the customer)
- The profit margin should be configured real time on flight / airline / country level
- All site traffic data should be collected and analysed for insight regarding user behaviour popular flights and opportunities (e.g: adjusting margins) in addition to audit and legal purposes.
- As they are building a platform FunWithFlights would like to expose their API for re-sellers
- The goal is to also sell directed advertisement on the site
- Their loyalty program is key to their success where the returning users can earn reward points based on their spending, in addition they should receive relevant deals based on their booking history (frequency, area, price range and interests) which should be also represented in their personal shopping assistant / chatbot.
- Recently they faced a DDOS attack so the system robustness (availability, scalability) is crucial for them along with observability and alerting to be able to instantly pinpoint and address such issues.
- The site should be able to handle automatic money exchange to give the users an ability to pay in their local currencies
- The expected number of concurrent users are in the tens of thousands with big surges around the major holidays.
- On top of accepting the major form of payments (VISA, MasterCard, AMEX, Apple Pay, GooglePay, PayPal...) they are exploring BitCoin too.
- If the user can't travel for some reason there should be a price controlled platform for them to re-sell their already purchased tickets which they can share on the major social media platforms.
- User notifications and communications can be configured and should range from email through phone to messaging applications. 

# Additional context
- This is a strategic direction;
- The company is expanding aggressively by merging with smaller competitors - think about REST API for mergers;
- The company has plans to switch over only for a few selected airlines to the new platform and if the switch is successful they will move ahead
- The company just exited a lawsuit where they settled a suit alleging fraud.

# Technical considerations
- The customer has plans to create a cloud-native solution. They are looking for well-reasoned guidance regarding which cloud provider would fit them best. They expect to have all required environments, CI/CD pipelines, databases in the cloud of the one of top cloud vendors;
- Preferable platform is technology of your choice for both backend and frontend;
- The customer expects our solution to be same tech quality as the code that you write in your day-to-day job;
- The customer expects our solution will have no 3rd-party components; however, we are free to use open source libs (we do not expect to write csv parsing on our own);

# Expectations
- to create a solution design with necessary diagrams and documentation. The solutions should satisfy the provided requirements;
- a solution skeleton to work with multiple data sources for flight route information, they shared the following APIs: [Provider1 endpoint](https://4r5rvu2fcydfzr5gymlhcsnfem0lyxoe.lambda-url.eu-central-1.on.aws/provider/flights1),  [Provider2 endpoint](https://4r5rvu2fcydfzr5gymlhcsnfem0lyxoe.lambda-url.eu-central-1.on.aws/provider/flights2)
- in the skeleton should be a possibility to read data from both endpoints and expose the aggregated dataset at GET /routes -> Model[]