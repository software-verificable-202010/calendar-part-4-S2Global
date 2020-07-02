# CalendarApp
CalendarApp is a Windows Application, user based calendar with both month and week views.

## What's New
In this last update, a guest view functionality has been added to allow users a chance to open other users calendars in a read-only manner.

## Testing
Unit testing has been applied to a considerable portion of the code (20.97%). Proof can be found in the Evidence folder.
Tests have been run through the Visual Studio 2019 Enterprise Test Explorer tool, which measures Block Statements.
Blocks are Statement sequences that are treated as a single Statement. This makes it a type of Statement Coverage. 

Reference: https://www.froglogic.com/coco/statement-coverage/

## Static Analysis
Static analysis was run to keep the code up to a high level of scrutiny.
The rules applied and results can be found in the Evidence folder.

Error NU1701 was left untouched for both projects as it did not impact the code and no solution was found for it.

Error CA1822 was disabled for each case in which a method needed to be called from the Test Project.