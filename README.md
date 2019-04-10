# BudgetTracker

Authors: Ian Kirkpatrick, Benjamin Groseclose

## Problem addressed
We wanted to budget finances digitally but could not find a product available that fit our needs that was free. We wanted one that would allow the user to easily plan their budget and record and track spending.

## Solution
Create a system that provides the user with easy financial management and planning.

## High level architectural approach
A client server system. There will be multiple client types such as a mobile app, a thin JavaScript web app and a desktop app. Each client will interact with an API provided by the server. The heavy lifting logic will all be done on the server. The clients will simply request actions and display results to the user.
