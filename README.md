# TRXpense

Application developed within my graduate thesis (Development of the travel expenses system using web technologies).

App administrator creates employees and assigns them roles (admin, employee, superior, manager). The administrator can also create, update or delete roles, vehicles, cost centers, expense categories etc.
All other roles only have the options associated with the travel order itself (creating, updating, generating PDF reports, sending for approval, closing etc.). 
Closing travel order means sending it to the base for processing. Only the administrator can see the base table with all arrived orders and can also delete them (employees can only see their own travel orders and delete them before they are closed). It is foreseen that the administrator is a person who works in a finance department and is in charge of processing travel orders.

The app contains various validation checks and messages, automatic calculation of daily costs (based on the dates of departure and return, destination state and the number of meals) and transportation expenses (based on selected vehicle type and initial and final mileage), automatic exchange rate calculation (based on destination state or chosen expense currency, date and amount)...
