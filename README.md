# Bazaar

## Table of content:

* [Project description](#project-description)
* [Technologies](#technologies)
* [Setup](#setup)
* [Features](#features)
* [Architecture](#architecture)
* [Contribution](#contribution)


## Project description

Bazaar is a new version of Marketplace that I have in repository. The idea is almost the same. Bazaar aggregates small businesses that are not big enough to have their own ecommerce sites but they want to expand themselves for broader group of clients. Bazaar allows a customer to make an order from several different shops and pay once for everything. Crucial idea is that customer waits for only one delivery with all ordered products. 

From customers perspective ordering is pretty simple. He doesnt need to walk around whole city to get products he wants. That simplify whole process. There is no need to place many orders, then payments and after all customers doesnt need to wait whole day (or more - depending on delivery company) for a delivery. 

From enterpreneur perspective it reduces cost of creating and maintaining ecommerce service/website. Enterpreneur just upload products he has in stock and updates its status if stock is empty. He gets split invoice and what was ordered specifically from his shop. Ideally he doesnt take care of delivery cause at the start and end of working day delivery company arrive to shop and take products that were ordered. 

Delivery company take care of completing orders, pack them together and bring to customers. This part can be split also to warehouse and shipping. Products are collected from shop and brought to warehouse where are packed and send by company. Separation can increase costs of whole process so Its not the best idea at this time and the best way is to let shipping company take care of package until it arrives to customer.

## Technologies

- C# 11
- .Net 7
- EntityFrameworkCore 7.0.4
- MSQLServer

## Setup

#### Clone to repository
```
$ git clone https://github.com/mateuszbabski/Bazaar
```

#### Go to the folder you cloned
```
$ cd Bazaar
```

#### Install dependencies
```
$ dotnet restore
```

#### Update appsettings.json 

#### Create empty database, create migration for each module and update database

#### Set Bootstrapper Layer as startup project

#### Run application
```
$ dotnet run
```

## Features

### Customers module:
- Creating an account
- Sign in
- Emitting domain events that account was created

### Shops module:
- Creating a shop account
- Sign in
- Emitting domain events that account was created/updated
- Update shop details
- Querying shops by localization and its name

### Products module:
- Add product to store (only for shop account)
- Update / edit product details (price, weight, availability etc.)
- Emitting domain events that product was created/updated
- Emitting integration event that product was added to cart ("add to cart" button clicked - customers only feature)
- Querying products by name, price, shop, category etc.

### Baskets module:
- Creating basket while adding first product
- Clearing basket from products (delete)
- Removing item from basket
- Checkout basket
- Change basket currency

### Shippings module:
- Adding, updating, deleting shipping methods
- Querying for available shipping methods
- Create shipping from order
- Querying shippings by orderId, trackingNumber
- Listing shipping history for customer

### Shared layer:
- Auth helpers - hashing service, token manager, jwt settings
- Email uniqueness checker (awaiting)
- Domain event disptacher
- Integration event disptacher 
- Error handling middleware
- User fetching service
- Unit of work pattern
- Module scanner/loader
- Currency converter
- Business rules checker
- Query Processor

### To implement:
- Autoupdate prices while checking out cart/placing order basis on current rates
- Notify all shops owners about products to prepare
- Sending invoices
- Sending email notifications about orders
- Confirm account, forgot/reset password features
- Unit tests

## Architecture

Projects is built using modular monolith architecture with strict separation of modules. Each module is based on clean architecture (if its needed of course), following DDD approach. For clearness and much better separation I used CQRS pattern and MediatR that handle application features and simplify controllers. I found the best way of communication between modules is by publishing events. As its not microservice architecture every module use the same database. 

To implement: diagrams and structures

## Contribution

Feel free to fork project and work on it with me. I am open to any suggestions, pull requests just to make project better.
